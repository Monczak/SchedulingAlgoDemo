using TMPro;
using UnityEngine;

public class ProcessBlock : MonoBehaviour
{
    public ProcessData processData;

    public int startTime;
    public float length;
    public Color color;

    public float cubeMargin;
    public float textHeight;
    public float widthScale;

    public Transform cubeTransform;
    public RectTransform canvasTransform;

    public TMP_Text text;

    private Material cubeMaterial;

    private const float brightnessThreshold = 0.78f;

    private Vector3 targetPosition;
    public float movementSmoothing;

    public float shadowValue = 0.2f;

    public bool onQueue;

    private Color shadowColor;
    private Color currentShadowColor;

    private Vector3 targetScale;
    public float scaleSmoothing;
    public float destroyDelay;

    [HideInInspector]
    public Transform arrow;

    private void Awake()
    {
        cubeMaterial = GetComponentInChildren<MeshRenderer>().material;
        targetPosition = transform.position;

        currentShadowColor = shadowColor;

        transform.localScale = Vector3.zero;
        targetScale = Vector3.one;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateComponents();
        Move();
        UpdateScale();
    }

    public void UpdateComponents()
    {
        length = processData.Duration;

        text.text = processData.Name;
        text.color = ColorUtility.CalculateLuminance(color) < brightnessThreshold ? Color.white : Color.black;

        cubeMaterial.SetColor("_Color", color);
        cubeMaterial.SetFloat("_ShadowPos", arrow.position.x + (transform.position.x - targetPosition.x));

        cubeMaterial.SetColor("_ShadowColor", currentShadowColor);

        cubeTransform.localScale = new Vector3((length - cubeMargin) * widthScale, 1, 1);
        canvasTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length * widthScale);
        canvasTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textHeight);

        shadowColor = color - Color.white * shadowValue;
        currentShadowColor = Color.Lerp(currentShadowColor, onQueue ? color : shadowColor, 15 * Time.deltaTime);
    }

    public void ForceNoShadow()
    {
        currentShadowColor = color;
    }

    public void ForceShadow()
    {
        currentShadowColor = shadowColor;
    }

    public void MoveTo(Vector3 pos, Transform parent)
    {
        transform.parent = parent;
        targetPosition = pos;
    }

    public void SetPosition(Vector3 pos, Transform parent)
    {
        MoveTo(pos, parent);
        transform.localPosition = pos;
    }

    private void Move()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, movementSmoothing * Time.deltaTime);
    }

    private void UpdateScale()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSmoothing * Time.deltaTime);
    }

    public void SetNormalScale()
    {
        transform.localScale = Vector3.one;
    }

    public void Destroy()
    {
        targetScale = Vector3.zero;
        Destroy(gameObject, destroyDelay);
    }
}
