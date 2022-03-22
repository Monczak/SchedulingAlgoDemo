using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    public TMP_Text algorithmNameText, algorithmDetailsText;

    public Dispatcher dispatcher;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        algorithmNameText.text = $"{dispatcher.schedulingAlgorithm.AlgorithmName} Algorithm";
        algorithmDetailsText.text = $"{SimulationManager.Instance.simulationSettings.processCount} Processes\nMean Waiting Time: {SimulationManager.Instance.MeanWaitingTime}";
    }
}
