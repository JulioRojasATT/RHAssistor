using UnityEngine;

public class PiperTrigger : MonoBehaviour {
    [SerializeField] private PiperConnector piperConnector;

    public void SpeakToMe(StringScriptableValue answerText) {
        piperConnector.TextToSpeech(answerText.Value);
    }
}
