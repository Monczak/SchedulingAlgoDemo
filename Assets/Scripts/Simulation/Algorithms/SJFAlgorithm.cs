public class SJFAlgorithm : SchedulingAlgorithm
{
    public override string AlgorithmName => "SJF";

    public override void OnNewProcess(ProcessBlock block, int time)
    {
        // Do nothing
    }

    public override void OnProcessFinished(int time)
    {
        if (BlockManager.Instance.queueTimeline.GetBlockCount() > 0)
        {
            int minIndex = -1;
            float minDuration = float.PositiveInfinity;

            for (int i = 0; i < BlockManager.Instance.queueTimeline.GetBlockCount(); i++)
            {
                ProcessBlock block = BlockManager.Instance.queueTimeline.GetBlock(i);
                if (block.length < minDuration)
                {
                    minDuration = block.length;
                    minIndex = i;
                }
            }

            AlgorithmOperation operation = new AlgorithmOperation
            {
                type = AlgorithmOperationType.TakeFromQueue,
                fromIndex = minIndex,
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
