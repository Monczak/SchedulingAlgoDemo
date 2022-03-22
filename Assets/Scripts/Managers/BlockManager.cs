using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager Instance { get; private set; }

    public GameObject blockPrefab;
    public TimelineController processTimeline, queueTimeline;

    public Transform arrow;

    private Controls controls;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(gameObject);

        controls = new Controls();

        controls.Debug.AddBlock.performed += OnAddBlock;
        controls.Debug.RemoveBlock.performed += OnRemoveBlock;
        controls.Debug.MoveBlockUp.performed += OnMoveBlockUp;
        controls.Debug.MoveBlockDown.performed += OnMoveBlockDown;

        // controls.Debug.Enable();
    }

    private void OnMoveBlockDown(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        TakeProcessFromQueue(0);
    }

    private void OnMoveBlockUp(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        MoveProcessToQueue(processTimeline.GetBlockCount() - 1);
    }

    private void OnRemoveBlock(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //ProcessBlock block = processTimeline.RemoveFromTimeline(1);
        //Destroy(block.gameObject);

        ProcessBlock block1 = processTimeline.GetBlock(processTimeline.GetBlockCount() - 2);
        ProcessBlock block2 = processTimeline.GetBlock(processTimeline.GetBlockCount() - 1);

        //float arrowRelativePos = arrow.transform.position.x - block1.transform.position.x + block1.length / 2;
        //Split(block1, (int)arrowRelativePos);

        Join(block1, block2);
    }

    private void OnAddBlock(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ProcessData process = new ProcessData { Name = "Blah", Duration = Random.Range(1, 5), StartTime = 0 };
        ProcessBlock processBlock = SpawnProcessBlock(process);
        processTimeline.PutOnTimeline(processBlock, Mathf.Min(processTimeline.GetBlockCount() - 1, 2));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public ProcessBlock SpawnProcessBlock(ProcessData process, Color? color = null, bool skipAppearAnimation = false)
    {
        ProcessBlock block = Instantiate(blockPrefab, transform.position, Quaternion.identity).GetComponent<ProcessBlock>();
        block.processData = process;
        block.length = process.Duration;
        block.color = color ?? Color.HSVToRGB((process.Id - 1f) / SimulationManager.Instance.simulationSettings.processCount, 0.8f, 1f);

        if (skipAppearAnimation) block.SetNormalScale();

        block.transform.parent = processTimeline.blockParent;

        block.arrow = arrow;

        return block;
    }

    public (ProcessBlock, ProcessBlock) Split(ProcessBlock block, int splitPoint)
    {
        if (splitPoint <= 0 || splitPoint >= block.length)
            return (null, null);

        int index = processTimeline.RemoveFromTimeline(block);

        ProcessBlock left = SpawnProcessBlock(new ProcessData
        {
            Duration = splitPoint,
            Name = block.processData.Name,
            StartTime = block.processData.StartTime,
            Id = block.processData.Id,
        }, block.color, true);
        ProcessBlock right = SpawnProcessBlock(new ProcessData
        {
            Duration = block.processData.Duration - splitPoint,
            Name = block.processData.Name,
            StartTime = block.processData.StartTime + splitPoint,
            Id = block.processData.Id,
        }, block.color, true);

        left.startTime = block.startTime;
        right.startTime = block.startTime + splitPoint;

        processTimeline.PutOnTimeline(left, index);
        processTimeline.PutOnTimeline(right, index + 1);

        block.Destroy();

        return (left, right);
    }

    public (ProcessBlock, ProcessBlock) SplitAtArrow(ProcessBlock block)
    {
        float arrowRelativePos = arrow.transform.position.x - block.transform.position.x + block.length / 2;
        return Split(block, (int)arrowRelativePos);
    }

    public ProcessBlock Join(ProcessBlock left, ProcessBlock right)
    {
        ProcessBlock block = SpawnProcessBlock(new ProcessData
        {
            Duration = left.processData.Duration + right.processData.Duration,
            Name = left.processData.Name,
            StartTime = left.processData.StartTime,
            Id = left.processData.Id,
        }, left.color, true);
        block.startTime = left.startTime;

        processTimeline.RemoveFromTimeline(right);
        int index = processTimeline.RemoveFromTimeline(left);

        block.transform.position = left.transform.position + Vector3.right * (-left.length / 2 + block.length / 2);
        processTimeline.PutOnTimeline(block, index, false);

        left.Destroy();
        right.Destroy();

        return block;
    }

    public int TakeProcessFromQueue(int index, int destinationPos = -1)
    {
        ProcessBlock block = queueTimeline.GetBlock(index);
        block.onQueue = false;
        return queueTimeline.MoveToTimeline(block, processTimeline, destinationPos);
    }

    public int MoveProcessToQueue(int index, int destinationPos = -1)
    {
        ProcessBlock block = processTimeline.GetBlock(index);
        block.onQueue = true;
        return processTimeline.MoveToTimeline(block, queueTimeline, destinationPos);
    }

    public ProcessBlock GetBlockAtTime(int time)
    {
        float blockEnd = 0;
        foreach (ProcessBlock block in processTimeline.GetBlocks())
        {
            blockEnd += block.length;
            if (time < blockEnd)
                return block;
        }
        return null;
    }
}
