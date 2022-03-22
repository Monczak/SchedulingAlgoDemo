using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public bool showOptionsMenu = false;

    [Header("UI Elements")]
    public ActivityMarker playMarker;
    public ActivityMarker reversePlayMarker, cameraFollowMarker;

    public CameraMovement cameraMovement;
    public BottomMenuAnimationController bottomMenuController;

    public TogglableButton newSequenceButton;

    public OptionsMenu optionsMenu;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        UpdateActivityMarkers();
        UpdateCameraMovement();
    }

    private void UpdateActivityMarkers()
    {
        if (SimulationManager.Instance.IsPlaying())
        {
            if (SimulationManager.Instance.GetPlayingSpeed() > 0)
            {
                playMarker.active = true;
                reversePlayMarker.active = false;
            }
            else
            {
                playMarker.active = false;
                reversePlayMarker.active = true;
            }
        }
        else
        {
            playMarker.active = false;
            reversePlayMarker.active = false;
        }

        cameraFollowMarker.active = SimulationManager.Instance.cameraMovement.following;
    }

    private void UpdateCameraMovement()
    {
        cameraMovement.panDown = showOptionsMenu;
    }

    private void UpdateUI()
    {
        if (showOptionsMenu)
        {
            bottomMenuController.animator.SetBool("Show", false);
        }

        newSequenceButton.SetActive(SimulationManager.Instance.currentTime == 0);
    }

    public void OnPlayForwardButton()
    {
        SimulationManager.Instance.PlayForward();
    }

    public void OnPlayBackwardButton()
    {
        SimulationManager.Instance.PlayBackward();
    }

    public void OnRewindButton()
    {
        SimulationManager.Instance.Rewind();
    }

    public void OnForwardButton()
    {
        SimulationManager.Instance.Forward();
    }

    public void OnCameraFollowButton()
    {
        SimulationManager.Instance.ToggleCameraFollow();
    }

    public void OnNewSequenceButton()
    {
        SimulationManager.Instance.GenerateNewSequence();
    }

    public void OnMenuButton()
    {
        optionsMenu.Show();
        showOptionsMenu = true;
    }
}
