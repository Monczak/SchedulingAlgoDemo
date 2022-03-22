public class RoundRobinAlgorithm : SchedulingAlgorithm
{
    private int interval;

    public RoundRobinAlgorithm(int interval)
    {
        this.interval = interval;
    }

    public override string AlgorithmName => "Round Robin";

    public override void OnNewProcess(ProcessBlock block, int time)
    {
        // Do nothing
    }

    public override void OnProcessFinished(int time)
    {
        if (time % interval != 0 && time != SimulationManager.Instance.simulationDuration)
            TakeNextBlock(time);
    }

    public override void Tick(int time)
    {
        int blockCount = BlockManager.Instance.queueTimeline.GetBlockCount();
        if (blockCount == 0)
        {
            return;
        }

        if (time % interval == 0)
        {
            TakeNextBlock(time);
        }
    }

    private void TakeNextBlock(int time)
    {
        int lastBlockIndex = BlockManager.Instance.processTimeline.GetBlockCount() - 1;

        ProcessBlock currentBlock = BlockManager.Instance.GetBlockAtTime(time);

        if (currentBlock == null)
        {
            InvokeOperation(new AlgorithmOperation
            {
                type = AlgorithmOperationType.TakeFromQueue,
                fromIndex = 0,
                toIndex = -1
            }, time);
        }
        else
        {
            InvokeOperation(new AlgorithmOperation
            {
                type = AlgorithmOperationType.Split,
                fromIndex = lastBlockIndex,
                toIndex = -1
            }, time);
            InvokeOperation(new AlgorithmOperation
            {
                type = AlgorithmOperationType.MoveToQueue,
                fromIndex = lastBlockIndex + 1,
                toIndex = -1
            }, time);
            InvokeOperation(new AlgorithmOperation
            {
                type = AlgorithmOperationType.TakeFromQueue,
                fromIndex = 0,
                toIndex = -1
            }, time);
        }
    }
}
