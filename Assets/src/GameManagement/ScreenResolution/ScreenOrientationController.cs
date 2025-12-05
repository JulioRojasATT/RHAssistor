using UnityEngine;

public class ScreenOrientationController : MonoBehaviour
{
    public void SetOrientationToPortrait() {
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
