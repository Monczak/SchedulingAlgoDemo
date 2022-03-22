using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed;
    public float moveSmoothing, coastSmoothing;
    public float maxSpeed;
    private float velocity;
    private float velocitySmoothing;
    private float currentVelocity;
    private float targetVelocity;

    private Controls controls;
    private float moveInput;

    private CameraController controller;

    private bool enableMovement;

    public GameObject followTarget;
    public bool following;
    public float followSmoothing;
    private Vector3 smoothVelocity;

    public bool panDown;
    public float panSpeed;
    private Vector3 panSmoothVelocity;

    public float restrictSpeed;

    private void Awake()
    {
        controls = new Controls();

        controls.Camera.Move.performed += OnMouseMove;
        controls.Camera.Move.canceled += OnMouseStop;
        controls.Camera.StartMovement.performed += OnStartMovement;
        controls.Camera.StartMovement.canceled += OnCancelMovement;
        controls.Camera.Enable();

        velocitySmoothing = coastSmoothing;
        currentVelocity = 0;

        controller = GetComponent<CameraController>();
    }

    private void OnMouseStop(InputAction.CallbackContext obj)
    {
        moveInput = 0;
    }

    private void OnCancelMovement(InputAction.CallbackContext obj)
    {
        enableMovement = false;
        velocitySmoothing = coastSmoothing;
    }

    private void OnStartMovement(InputAction.CallbackContext obj)
    {
        enableMovement = true;
        velocitySmoothing = moveSmoothing;
    }

    private void OnMouseMove(InputAction.CallbackContext obj)
    {
        moveInput = obj.ReadValue<Vector2>().x;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessMouseInput();

        if (following)
        {
            Follow();
        }
        else
        {
            controller.position += Vector3.right * currentVelocity * Time.smoothDeltaTime;
            currentVelocity = Mathf.SmoothDamp(currentVelocity, targetVelocity, ref velocity, velocitySmoothing);
            RestrictMovement();
        }

        PanDown();
    }

    void ProcessMouseInput()
    {
        if (enableMovement)
        {
            if (Mathf.Abs(moveInput) != 0)
            {
                following = false;
            }
            currentVelocity = -moveSpeed * moveInput / Time.smoothDeltaTime;
            targetVelocity = currentVelocity;
        }
        else
            targetVelocity = 0;
    }

    void Follow()
    {
        controller.position = Vector3.SmoothDamp(controller.position, followTarget.transform.position, ref smoothVelocity, followSmoothing, maxSpeed);
    }

    void PanDown()
    {
        controller.offset = Vector3.SmoothDamp(controller.offset, panDown ? Vector3.back * CameraUtility.GetScreenHeightInUnits() : Vector3.zero, ref panSmoothVelocity, panSpeed);
    }

    void RestrictMovement()
    {
        if (controller.position.x < 0)
            controller.position = Vector3.Lerp(controller.position, new Vector3(0, controller.position.y, controller.position.z), restrictSpeed * Time.smoothDeltaTime);

        if (controller.position.x > SimulationManager.Instance.simulationDuration)
            controller.position = Vector3.Lerp(controller.position, new Vector3(SimulationManager.Instance.simulationDuration, controller.position.y, controller.position.z), restrictSpeed * Time.smoothDeltaTime);
    }
}
