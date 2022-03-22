public struct AlgorithmOperation
{
    public AlgorithmOperationType type;
    public int fromIndex, toIndex;

    public AlgorithmOperation Reverse()
    {
        AlgorithmOperation result = new AlgorithmOperation
        {
            type = type switch
            {
                AlgorithmOperationType.TakeFromQueue => AlgorithmOperationType.MoveToQueue,
                AlgorithmOperationType.MoveToQueue => AlgorithmOperationType.TakeFromQueue,
                AlgorithmOperationType.Split => AlgorithmOperationType.Join,
                AlgorithmOperationType.Join => AlgorithmOperationType.Split,
                _ => AlgorithmOperationType.Unknown,
            }
        };
        (result.fromIndex, result.toIndex) = (toIndex, fromIndex);
        return result;
    }
}
