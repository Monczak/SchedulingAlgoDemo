using System.Collections.Generic;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    private List<ProcessBlock> processBlocks;

    public Transform blockParent;

    [HideInInspector]
    public float totalLength;

    public Dictionary<int, (ProcessBlock, ProcessBlock)> processKeyframes;

    private void Awake()
    {
        processBlocks = new List<ProcessBlock>();
        processKeyframes = new Dictionary<int, (ProcessBlock, ProcessBlock)>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int PutOnTimeline(ProcessBlock block, int pos = -1, bool setPosition = true)
    {
        if (pos == -1 || processBlocks.Count == 0)
        {
            processBlocks.Add(block);
        }
        else
        {
            processBlocks.Insert(pos, block);
        }

        if (setPosition) SetBlockPosition(block);

        UpdateBlocks();

        return pos == -1 ? processBlocks.Count - 1 : pos;
    }

    public int MoveToTimeline(ProcessBlock block, TimelineController timeline, int pos = -1)
    {
        int index = timeline.PutOnTimeline(block, pos, false);
        RemoveFromTimeline(block);
        return index;
    }

    public void UpdateBlocks()
    {
        processKeyframes.Clear();
        processKeyframes.Add(0, (null, null));

        float position = 0;
        for (int i = 0; i < processBlocks.Count; i++)
        {
            ProcessBlock block = processBlocks[i];
            block.MoveTo(new Vector3(position + block.length / 2f, 0, 0), blockParent);

            processKeyframes[(int)position] = (i - 1 < 0 ? null : processBlocks[i], block);

            position += processBlocks[i].length;

            processKeyframes[(int)position] = (block, null);
        }
        totalLength = position;

    }

    public void SetBlockPosition(ProcessBlock block)
    {
        float position = 0;
        for (int i = 0; i < processBlocks.Count; i++)
        {
            ProcessBlock currentBlock = processBlocks[i];
            if (currentBlock == block) block.SetPosition(new Vector3(position + block.length / 2f, 0, 0), blockParent);
            position += processBlocks[i].length;
        }
    }

    public int RemoveFromTimeline(ProcessBlock block)
    {
        int index = processBlocks.IndexOf(block);
        processBlocks.RemoveAt(index);

        UpdateBlocks();

        return index;
    }

    public ProcessBlock RemoveFromTimeline(int index)
    {
        ProcessBlock block = processBlocks[index];
        processBlocks.RemoveAt(index);

        UpdateBlocks();

        return block;
    }

    public void Clear()
    {
        if (processBlocks != null)
        {
            foreach (ProcessBlock block in processBlocks)
                block.Destroy();
            processBlocks.Clear();
        }
    }

    public ProcessBlock GetBlock(int index) => processBlocks[index];
    public List<ProcessBlock> GetBlocks() => processBlocks;
    public int GetBlockCount() => processBlocks.Count;
}
