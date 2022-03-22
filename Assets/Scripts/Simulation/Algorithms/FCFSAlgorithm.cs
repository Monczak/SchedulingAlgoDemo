public class FCFSAlgorithm : SchedulingAlgorithm
{
    public override string AlgorithmName => "FCFS";

    public override void OnNewProcess(ProcessBlock block, int time)
    {
        // Do nothing
    }

    public override void OnProcessFinished(int time)
    {
        if (BlockManager.Instance.queueTimeline.GetBlockCount() > 0)
        {
            AlgorithmOperation operation = new AlgorithmOperation
            {
                type = AlgorithmOperationType.TakeFromQueue,
                fromIndex = 0,
                toIndex = -1
            };
            InvokeOperation(operation, time);
        }

    }

    public override void Tick(int time)
    {
        // Do nothing
    }
}
