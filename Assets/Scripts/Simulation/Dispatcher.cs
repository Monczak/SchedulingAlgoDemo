using System.Collections.Generic;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{
    public TimelineController processTimeline, queueTimeline;

    public SchedulingAlgorithm schedulingAlgorithm;

    private Dictionary<int, Stack<AlgorithmOperation>> operationHistory;

    private void Awake()
    {
        operationHistory = new Dictionary<int, Stack<AlgorithmOperation>>();

        SetAlgorithm(AlgorithmType.FCFS);

        SimulationManager.Instance.ReverseTick += ReverseTick;
    }

    // Start is called before the first frame update
    void Start()
    {
        SimulationManager.Instance.Tick += Tick;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetAlgorithm(AlgorithmType type)
    {
        switch (type)
        {
            case AlgorithmType.FCFS:
                schedulingAlgorithm = new FCFSAlgorithm();
                break;
            case AlgorithmType.SJF:
                schedulingAlgorithm = new SJFAlgorithm();
                break;
            case AlgorithmType.SRTF:
                schedulingAlgorithm = new SRTFAlgorithm();
                break;
            case AlgorithmType.RoundRobin:
                schedulingAlgorithm = new RoundRobinAlgorithm(SimulationManager.Instance.simulationSettings.roundRobinInterval);
                break;
            default:
                break;
        }
        schedulingAlgorithm.OnNewOperation += (operation, time) => OnNewOperation(operation, time);
    }

    private void OnNewOperation(AlgorithmOperation operation, int time, bool record = true)
    {
        switch (operation.type)
        {
            case AlgorithmOperationType.TakeFromQueue:
                {
                    operation.toIndex = BlockManager.Instance.TakeProcessFromQueue(operation.fromIndex, operation.toIndex);

                    ProcessBlock block = BlockManager.Instance.processTimeline.GetBlock(operation.toIndex);
                    block.startTime = time;
                    break;
                }

            case AlgorithmOperationType.MoveToQueue:
                {
                    operation.toIndex = BlockManager.Instance.MoveProcessToQueue(operation.fromIndex, operation.toIndex);
                    break;
                }

            case AlgorithmOperationType.Split:
                {
                    ProcessBlock block = processTimeline.GetBlock(processTimeline.GetBlockCount() - 1);
                    int splitTime = time - block.startTime;
                    BlockManager.Instance.Split(block, splitTime);
                    break;
                }

            case AlgorithmOperationType.Join:
                {
                    ProcessBlock block1 = processTimeline.GetBlock(processTimeline.GetBlockCount() - 2);
                    ProcessBlock block2 = processTimeline.GetBlock(processTimeline.GetBlockCount() - 1);
                    BlockManager.Instance.Join(block1, block2);
                    break;
                }


            default:
                break;
        }

        Debug.Log($"[{schedulingAlgorithm.AlgorithmName}] {operation.type} from {operation.fromIndex} to {operation.toIndex}");

        if (record)
        {
            if (!operationHistory.ContainsKey(time))
                operationHistory.Add(time, new Stack<AlgorithmOperation>());
            operationHistory[time].Push(operation);
        }
    }

    private void Tick(int time)
    {
        bool onKeyframe = processTimeline.processKeyframes.ContainsKey(time) || (BlockManager.Instance.queueTimeline.GetBlockCount() > 0 && time == 0);
        if (onKeyframe)
        {
            if (processTimeline.GetBlockCount() > 0 && time != 0)
            {
                SetEndTime(time);
            }
        }

        schedulingAlgorithm.Tick(time);

        if (onKeyframe)
        {
            schedulingAlgorithm.OnProcessFinished(time);
        }
    }

    private void ReverseTick(int time)
    {
        if (processTimeline.processKeyframes.ContainsKey(time) || (BlockManager.Instance.queueTimeline.GetBlockCount() > 0 && time == 0))
        {
            if (operationHistory.ContainsKey(time) && operationHistory[time].Count > 0)
            {
                SetEndTime(-1);

                ProcessBlock block = BlockManager.Instance.GetBlockAtTime(time);
                if (time == SimulationManager.Instance.spawnTimes[block.processData.Name])
                    SimulationManager.Instance.spawnTimes[block.processData.Name] = -1;

                while (operationHistory[time].Count > 0)
                {
                    AlgorithmOperation operation = operationHistory[time].Pop().Reverse();
                    OnNewOperation(operation, time, record: false);
                }
                operationHistory.Remove(time);
            }
        }
    }

    private void SetEndTime(int time)
    {
        ProcessBlock block = processTimeline.GetBlock(processTimeline.GetBlockCount() - 1);
        if (block != null)
            SimulationManager.Instance.endTimes[block.processData.Name] = time;
    }

    public void ClearOperationHistory()
    {
        operationHistory?.Clear();
    }
}
