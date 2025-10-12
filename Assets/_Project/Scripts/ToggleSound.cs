using UnityEngine;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour
{
    public ActionTimelineController actionTimelineController;
    public float pitch;

    public void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(delegate { BeatSound(); });
    }
    
    public void BeatSound()
    {
        GameManager.Instance.AudioManagerRef.PlayBeatTune(actionTimelineController.BeatIndex, pitch);
    }
}
