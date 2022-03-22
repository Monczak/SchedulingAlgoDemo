using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public bool follow;
    public new Camera camera;

    public ProcessTimeline processTimeline;
    private ProcessTimeline queueTimeline;

    private Transform initialParent;
    private Vector3 initialPosition;

    public float viewportMargin;

    private bool enterRestriction = false;

    private void Awake()
    {
        initialParent = transform.parent;
        initialPosition = transform.position;
        queueTimeline = GetComponentInChildren<ProcessTimeline>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            RestrictPosition();
        }
    }

    void RestrictPosition()
    {
        Vector3 queueViewportPos = camera.WorldToViewportPoint(transform.position - Vector3.right * queueTimeline.margin / 2);
        Vector3 queueEndViewportPos = camera.WorldToViewportPoint(transform.position + Vector3.right * (queueTimeline.width + queueTimeline.margin / 2));

        float screenWidth = (camera.ViewportToWorldPoint(new Vector3(1, 1, queueViewportPos.z)) - camera.ViewportToWorldPoint(new Vector3(0, 0, queueViewportPos.z))).x;

        Vector3 processTimelineViewportPos = camera.WorldToViewportPoint(processTimeline.transform.position - Vector3.right * (processTimeline.width / 2 + processTimeline.margin / 2));

        if (queueViewportPos.x - viewportMargin < 0 && processTimelineViewportPos.x - viewportMargin < 0)
        {
            transform.parent = camera.transform;

            //if (!enterRestriction)
            //{
            //    transform.position = new Vector3(camera.transform.position.x - queueTimeline.width / 2, transform.localPosition.y, transform.localPosition.z);
            //}
            //enterRestriction = true;
        }
        else
        {
            transform.parent = initialParent;
            if (processTimelineViewportPos.x - viewportMargin >= 0)
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            enterRestriction = false;
        }

        transform.position = new Vector3(transform.position.x, initialPosition.y, initialPosition.z);
    }
}
