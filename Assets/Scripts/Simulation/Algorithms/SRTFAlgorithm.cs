public class SRTFAlgorithm : SchedulingAlgorithm
{
    public override string AlgorithmName => "SRTF";

    public override void OnNewProcess(ProcessBlock block, int time)
    {
        if (BlockManager.Instance.processTimeline.GetBlockCount() > 0)
        {
            int lastBlockIndex = BlockManager.Instance.processTimeline.GetBlockCount() - 1;
            ProcessBlock currentBlock = BlockManager.Instance.processTimeline.GetBlock(lastBlockIndex);

            if (BlockManager.Instance.GetBlockAtTime(time) != null)
            {
                if (block.length < currentBlock.length - (time - currentBlock.startTime))
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
                }
            }
        }
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
