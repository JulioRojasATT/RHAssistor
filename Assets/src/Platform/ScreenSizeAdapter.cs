using UnityEngine;

public abstract class ScreenSizeAdapter : MonoBehaviour {

    protected const int BASE_WIDTH = 1080, BASE_HEIGHT = 1920;

    [SerializeField] protected bool autoAdaptAtStart;

    public abstract void AutoAdapt();

    private void Start(){
        if (!autoAdaptAtStart){
            return;
        }
        AutoAdapt();
    }

    public float AdaptUsingHeight(float value) {
        return Screen.height * value / BASE_HEIGHT;
    }

    public float AdaptUsingWidth(float value)
    {
        return Screen.width * value / BASE_WIDTH;
    }

    public float Adapt(float originalValue, float newValue, float value) {
        return newValue * value / originalValue;
    }
}
