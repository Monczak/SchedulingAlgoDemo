using UnityEngine;
using UnityEngine.UI;

public class TogglableButton : MonoBehaviour
{
    private bool active;

    private Button button;
    private Image image;

    private Color currentColor;

    public Color activeColor, inactiveColor;
    public float colorSmoothing;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        currentColor = button.interactable ? activeColor : inactiveColor;
    }

    private void Update()
    {
        currentColor = Color.Lerp(currentColor, active ? activeColor : inactiveColor, colorSmoothing * Time.deltaTime);
        image.color = currentColor;
    }

    public void SetActive(bool active)
    {
        this.active = active;
        button.interactable = active;
    }
}
