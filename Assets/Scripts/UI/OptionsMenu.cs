using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public MultiSelector algorithmSelector;
    public TMP_InputField processCountInput;
    public TMP_InputField minProcessDurationInput, maxProcessDurationInput;
    public TMP_InputField roundRobinIntervalInput;
    public TMP_InputField simulationSpeedInput;

    public TogglableButtonText applyButton;

    public Color normalInputColor, invalidInputColor;
    public float colorChangeRate;

    private Dictionary<TMP_InputField, bool> inputValidity;

    private SimulationSettings currentSettings;

    public float minSimulationSpeed, maxSimulationSpeed;

    private bool valid;

    private void Awake()
    {
        inputValidity = new Dictionary<TMP_InputField, bool>
        {
            { processCountInput, true },
            { minProcessDurationInput, true },
            { maxProcessDurationInput, true },
            { roundRobinIntervalInput, true },
            { simulationSpeedInput, true },
        };

        algorithmSelector.OnChanged += OnAlgorithmSelectorChanged;
        processCountInput.onValueChanged.AddListener(s => OnProcessCountUpdate(s));
        minProcessDurationInput.onValueChanged.AddListener(s => OnMinProcessDurationUpdate(s));
        maxProcessDurationInput.onValueChanged.AddListener(s => OnMaxProcessDurationUpdate(s));
        roundRobinIntervalInput.onValueChanged.AddListener(s => OnRRIntervalUpdate(s));
        simulationSpeedInput.onValueChanged.AddListener(s => OnSimulationSpeedUpdate(s));
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateInputs();
    }

    // Update is called once per frame
    void Update()
    {
        CheckValidity();
        UpdateColors();
    }

    private void CheckValidity()
    {
        valid = AreInputsValid();
    }

    private void UpdateInputs()
    {
        currentSettings = SimulationManager.Instance.simulationSettings;

        algorithmSelector.Selected = (int)currentSettings.algorithmType;
        processCountInput.text = currentSettings.processCount.ToString();
        minProcessDurationInput.text = currentSettings.minProcessDuration.ToString();
        maxProcessDurationInput.text = currentSettings.maxProcessDuration.ToString();
        roundRobinIntervalInput.text = currentSettings.roundRobinInterval.ToString();
        simulationSpeedInput.text = currentSettings.simulationSpeed.ToString();
    }

    private void ApplySettings()
    {
        bool newSequence =
            currentSettings.processCount != SimulationManager.Instance.simulationSettings.processCount ||
            currentSettings.minProcessDuration != SimulationManager.Instance.simulationSettings.minProcessDuration ||
            currentSettings.maxProcessDuration != SimulationManager.Instance.simulationSettings.maxProcessDuration;

        bool clearAndSuppress =
            currentSettings.roundRobinInterval != SimulationManager.Instance.simulationSettings.roundRobinInterval;

        SimulationManager.Instance.SetSimulationSettings(currentSettings);
        if (newSequence)
            SimulationManager.Instance.GenerateNewSequence();
        if (clearAndSuppress)
            SimulationManager.Instance.ClearAndSuppress();
        SimulationManager.Instance.Rewind();
        Hide();
    }

    public void Show()
    {
        UpdateInputs();
    }

    public void Hide()
    {
        UIManager.Instance.showOptionsMenu = false;
    }

    private void UpdateColors()
    {
        foreach (KeyValuePair<TMP_InputField, bool> pair in inputValidity)
        {
            pair.Key.textComponent.color = Color.Lerp(pair.Key.textComponent.color, pair.Value ? normalInputColor : invalidInputColor, colorChangeRate * Time.deltaTime);
        }

        applyButton.SetActive(valid);
    }

    private bool AreInputsValid()
    {
        bool valid = true;
        foreach (bool v in inputValidity.Values)
            valid &= v;
        return valid;
    }

    private void OnAlgorithmSelectorChanged()
    {
        currentSettings.algorithmType = (AlgorithmType)algorithmSelector.Selected;
    }

    private void OnProcessCountUpdate(string input)
    {
        if (int.TryParse(input, out int value))
        {
            if (value < 0)
                MarkInvalid(processCountInput);
            else
            {
                MarkNormal(processCountInput);
                currentSettings.processCount = value;
            }

        }
        else
        {
            MarkInvalid(processCountInput);
        }
    }

    private void OnMinProcessDurationUpdate(string input, bool sub = false)
    {
        if (int.TryParse(input, out int minDuration))
        {
            if (minDuration < 0 || minDuration > currentSettings.maxProcessDuration)
                MarkInvalid(minProcessDurationInput);
            else
            {
                MarkNormal(minProcessDurationInput);
                currentSettings.minProcessDuration = minDuration;
            }
        }
        else
        {
            MarkInvalid(minProcessDurationInput);
        }

        if (!sub)
            OnMaxProcessDurationUpdate(maxProcessDurationInput.text, true);
    }

    private void OnMaxProcessDurationUpdate(string input, bool sub = false)
    {
        if (int.TryParse(input, out int maxDuration))
        {
            if (maxDuration < 0 || maxDuration < currentSettings.minProcessDuration)
                MarkInvalid(maxProcessDurationInput);
            else
            {
                MarkNormal(maxProcessDurationInput);
                currentSettings.maxProcessDuration = maxDuration;
            }
        }
        else
        {
            MarkInvalid(maxProcessDurationInput);
        }

        if (!sub)
            OnMinProcessDurationUpdate(minProcessDurationInput.text, true);
    }

    private void OnRRIntervalUpdate(string input)
    {
        if (int.TryParse(input, out int value))
        {
            if (value < 1)
                MarkInvalid(roundRobinIntervalInput);
            else
            {
                MarkNormal(roundRobinIntervalInput);
                currentSettings.roundRobinInterval = value;
            }
        }
        else
        {
            MarkInvalid(roundRobinIntervalInput);
        }
    }

    private void OnSimulationSpeedUpdate(string input)
    {
        if (float.TryParse(input, out float value))
        {
            if (value < minSimulationSpeed || value > maxSimulationSpeed)
                MarkInvalid(simulationSpeedInput);
            else
            {
                MarkNormal(simulationSpeedInput);
                currentSettings.simulationSpeed = value;
            }
        }
        else
        {
            MarkInvalid(simulationSpeedInput);
        }
    }

    private void MarkInvalid(TMP_InputField input)
    {
        inputValidity[input] = false;
    }

    private void MarkNormal(TMP_InputField input)
    {
        inputValidity[input] = true;
    }

    public void OnApply()
    {
        ApplySettings();
    }

    public void OnReturn()
    {
        Hide();
    }
}
