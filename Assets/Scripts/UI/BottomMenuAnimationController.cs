using UnityEngine;

public class BottomMenuAnimationController : MonoBehaviour
{
    public Vector2 activationAreaSize;

    private Controls controls;

    private RectTransform canvasRect;

    [HideInInspector]
    public Animator animator;

    private void Awake()
    {
        controls = new Controls();

        controls.Global.MousePosition.performed += OnMousePosChanged;

        controls.Global.Enable();

        animator = GetComponent<Animator>();
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    private void OnMousePosChanged(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 mousePos = obj.ReadValue<Vector2>();
        animator.SetBool("Show",
            mousePos.y < activationAreaSize.y &&
            mousePos.x > canvasRect.sizeDelta.x / 2 - activationAreaSize.x / 2 &&
            mousePos.x < canvasRect.sizeDelta.x / 2 + activationAreaSize.x / 2 &&
            !UIManager.Instance.showOptionsMenu);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
