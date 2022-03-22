using UnityEngine;

public class ProcessTimeline : MonoBehaviour
{
    [Header("Timeline")]
    public float width;
    public float height, margin;

    private SpriteRenderer timelineRenderer;

    [Header("Ticks")]
    public bool ticksEnabled;
    public Transform ticks;
    public float tickHeight;
    public float tickMargin;
    public Vector3 ticksOffset;

    private void Awake()
    {
        timelineRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(width / 2, 0, 0);
        timelineRenderer.size = new Vector2(width + margin, height);

        ticks.gameObject.SetActive(ticksEnabled);
        if (ticksEnabled)
        {
            ticks.localPosition = ticksOffset + Vector3.down * (height / 2 + tickHeight / 2);
            ticks.localScale = new Vector2(width + tickMargin, tickHeight);
        }

    }
}
