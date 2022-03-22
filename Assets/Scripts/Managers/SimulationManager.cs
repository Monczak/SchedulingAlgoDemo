using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance { get; private set; }

    public SimulationSettings simulationSettings;

    [Header("Simulation Timing")]
    public float simulationDuration;
    public float currentTime;
    [HideInInspector] public float previousTime;
    public float speed;

    [Header("Time Marker")]
    public GameObject arrow;
    public float arrowMovementSmoothing;
    private float velocity;
    public float unitsPerSecond = 1;

    [Header("Comfort Features")]
    public CameraMovement cameraMovement;

    private Dispatcher dispatcher;
    private ProcessSpawner processSpawner;

    public Dictionary<string, int> spawnTimes;
    public Dictionary<string, int> endTimes;
    public Dictionary<string, int> durations;

    public float MeanWaitingTime { get; private set; }

    private bool suppressTicks = false;

    private bool playing = false;
    private float playingSpeed;

    private Vector3 initialPos;

    public delegate void TickDelegate(int time);
    public event TickDelegate Tick, ReverseTick;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(gameObject);

        dispatcher = GetComponent<Dispatcher>();
        processSpawner = GetComponent<ProcessSpawner>();

        initialPos = arrow.transform.position;

        previousTime = currentTime;

        MeanWaitingTime = 0;

        SetDefaultSettings();
        GenerateNewSequence();
    }

    // Start is called before the first frame update
    void Start()
    {
        arrow.transform.position = initialPos;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        if (!suppressTicks) TickEveryUnit();
        CalculateMeanWaitingTime();

        MoveArrow();

        previousTime = currentTime;
        suppressTicks = false;
    }

    public void SetSimulationSettings(SimulationSettings settings)
    {
        simulationSettings = settings;
        dispatcher.SetAlgorithm(settings.algorithmType);
    }

    public void SetSimulationDuration(int duration)
    {
        simulationDuration = duration;
        BlockManager.Instance.processTimeline.GetComponentInChildren<ProcessTimeline>().width = duration;
    }

    private void TickEveryUnit()
    {
        if (currentTime < previousTime)
        {
            int lower = (int)Mathf.Floor(currentTime);
            if (Mathf.Approximately(currentTime, 0))
                lower--;

            int upper = (int)previousTime;
            if (upper == simulationDuration)
                upper--;

            for (int i = upper; i > lower; i--)
            {
                ReverseTick?.Invoke(i);
            }
        }
        else
        {
            int lower = (int)Mathf.Floor(previousTime);
            if (Mathf.Approximately(previousTime, 0) && (IsPlaying() || currentTime == simulationDuration))
                lower--;

            for (int i = lower; i < (int)currentTime; i++)
            {
                Tick?.Invoke(i + 1);
            }
        }
    }

    public bool IsPlaying()
    {
        return playing;
    }

    public float GetPlayingSpeed()
    {
        return playingSpeed;
    }

    public void PlayForward()
    {
        if (!playing || playingSpeed < 0)
        {
            playing = true;
            playingSpeed = speed;
        }
        else
        {
            Pause();
        }
    }

    public void PlayBackward()
    {
        if (!playing || playingSpeed > 0)
        {
            playing = true;
            playingSpeed = -speed;
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        playing = false;
        playingSpeed = 0;
    }

    public void Rewind()
    {
        playingSpeed = 0;
        currentTime = 0;
        playing = false;
    }

    public void Forward()
    {
        playingSpeed = 0;
        currentTime = simulationDuration;
        playing = false;
    }

    private void UpdateTime()
    {
        currentTime += playingSpeed * simulationSettings.simulationSpeed * Time.deltaTime;
        ClampTime();
    }

    private void ClampTime()
    {
        if (currentTime < 0)
        {
            Pause();
            currentTime = 0;
        }
        if (currentTime > simulationDuration)
        {
            Pause();
            currentTime = simulationDuration;
        }
    }

    private void MoveArrow()
    {
        arrow.transform.position = new Vector3(Mathf.SmoothDamp(arrow.transform.position.x, currentTime * unitsPerSecond, ref velocity, arrowMovementSmoothing), initialPos.y, initialPos.z);
    }

    public void ToggleCameraFollow()
    {
        cameraMovement.following ^= true;
    }

    public void GenerateNewSequence()
    {
        Debug.Log("New sequence!");
        (var sequence, int totalDuration) = ProcessSequenceGenerator.GenerateSequence(simulationSettings.processCount);
        processSpawner.sequence = sequence;
        SetSimulationDuration(totalDuration);

        spawnTimes = new Dictionary<string, int>();
        endTimes = new Dictionary<string, int>();
        durations = new Dictionary<string, int>();
        foreach (List<ProcessData> processes in sequence.Values)
        {
            foreach (ProcessData data in processes)
            {
                if (!spawnTimes.ContainsKey(data.Name))
                    spawnTimes.Add(data.Name, -1);
                if (!endTimes.ContainsKey(data.Name))
                    endTimes.Add(data.Name, -1);
                if (!durations.ContainsKey(data.Name))
                    durations.Add(data.Name, (int)data.Duration);
            }
        }

        ClearAndSuppress();
    }

    public void ClearAndSuppress()
    {
        dispatcher.ClearOperationHistory();
        BlockManager.Instance.processTimeline.Clear();
        BlockManager.Instance.queueTimeline.Clear();

        suppressTicks = true;
    }

    public void SetDefaultSettings()
    {
        simulationSettings = new SimulationSettings
        {
            algorithmType = AlgorithmType.FCFS,
            minProcessDuration = 1,
            maxProcessDuration = 5,
            processCount = 5,
            roundRobinInterval = 1,
            simulationSpeed = 1,
        };
    }

    public void CalculateMeanWaitingTime()
    {
        float sumOfWaitingTimes = 0;
        int processesEvaluated = 0;
        foreach (string processName in durations.Keys)
        {
            if (spawnTimes[processName] != -1 && endTimes[processName] != -1)
            {
                int turnaroundTime = endTimes[processName] - spawnTimes[processName];
                int waitingTime = turnaroundTime - durations[processName];
                sumOfWaitingTimes += waitingTime;
                processesEvaluated++;
            }

        }

        if (processesEvaluated == 0)
        {
            MeanWaitingTime = 0;
            return;
        }
        MeanWaitingTime = sumOfWaitingTimes / processesEvaluated;
    }
}
