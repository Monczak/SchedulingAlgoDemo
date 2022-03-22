public abstract class SchedulingAlgorithm
{
    public abstract string AlgorithmName { get; }

    public abstract void OnNewProcess(ProcessBlock block, int time);
    public abstract void OnProcessFinished(int time);
    public abstract void Tick(int time);

    public delegate void OnNewOperationDelegate(AlgorithmOperation operation, int time);
    public event OnNewOperationDelegate OnNewOperation;

    protected virtual void InvokeOperation(AlgorithmOperation operation, int time)
    {
        OnNewOperation?.Invoke(operation, time);
    }
}
