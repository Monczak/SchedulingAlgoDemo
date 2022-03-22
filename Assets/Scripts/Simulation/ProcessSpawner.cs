using System.Collections.Generic;
using UnityEngine;

public class ProcessSpawner : MonoBehaviour
{
    public Dictionary<int, List<ProcessData>> sequence;

    private Dispatcher dispatcher;

    private void Awake()
    {
        dispatcher = GetComponent<Dispatcher>();

        SimulationManager.Instance.Tick += Tick;
    }

    // Start is called before the first frame update
    void Start()
    {
        SimulationManager.Instance.ReverseTick += ReverseTick;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Tick(int time)
    {
        if (sequence.ContainsKey(time))
        {
            foreach (ProcessData processData in sequence[time])
            {
                ProcessBlock block = BlockManager.Instance.SpawnProcessBlock(processData);
                BlockManager.Instance.queueTimeline.PutOnTimeline(block);
                block.onQueue = true;
                block.ForceNoShadow();

                if (SimulationManager.Instance.spawnTimes[block.processData.Name] == -1)
                    SimulationManager.Instance.spawnTimes[block.processData.Name] = time;

                dispatcher.schedulingAlgorithm.OnNewProcess(block, time);
            }

        }
    }

    private void ReverseTick(int time)
    {
        if (sequence.ContainsKey(time))
        {
            foreach (ProcessData _ in sequence[time])
            {
                ProcessBlock block = BlockManager.Instance.queueTimeline.RemoveFromTimeline(BlockManager.Instance.queueTimeline.GetBlockCount() - 1);
                block.Destroy();
            }

        }
    }
}
