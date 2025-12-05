using UnityEngine;

public class ImageTransformManipulator : MonoBehaviour
{
    /*[SerializeField] private Image targetImage;

    [SerializeField] private Vector3 _startingImageScale;

    [SerializeField] private Vector3 _startingAnchoredPosition;

    [Header("Scaling parameters")]
    [SerializeField] private float scaleMultiplier;

    private ScaleGestureRecognizer _scaleGesture;

    [Header("Translation parameters")]
    [SerializeField] private float translationMultiplier;

    [SerializeField] private Transform comboImageOrigin;

    private PanGestureRecognizer _panGesture;

    private void Start()
    {
        _startingImageScale = targetImage.rectTransform.localScale;
        _startingAnchoredPosition = targetImage.rectTransform.anchoredPosition;
        CreateScaleGesture();
        CreatePanGesture();
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y!=0) {
            TryAddToScale(Input.mouseScrollDelta.y * scaleMultiplier * Vector3.one);
        }
    }

    private void CreatePanGesture()
    {
        _panGesture = new PanGestureRecognizer();
        _panGesture.MinimumNumberOfTouchesToTrack = 1;
        _panGesture.StateUpdated += PanGestureCallback;
        FingersScript.Instance.AddGesture(_panGesture);
    }

    private void PanGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            TryTranslateImage(translationMultiplier * new Vector3(_panGesture.DeltaX, _panGesture.DeltaY));
        }   
    }

    public void TryTranslateImage(Vector3 movementVector)
    {
        if (!targetImage.gameObject.activeSelf)
        {
            return;
        }
        Debug.Log("Moving image");
        targetImage.rectTransform.localPosition+=  movementVector;
    }

    public void ResetImagePosition()
    {
        targetImage.rectTransform.anchoredPosition = _startingAnchoredPosition;
        //targetImage.rectTransform.position = _startingImagePosition;
    }

    private void CreateScaleGesture()
    {
        _scaleGesture = new ScaleGestureRecognizer();
        _scaleGesture.StateUpdated += ScaleGestureCallback;
        FingersScript.Instance.AddGesture(_scaleGesture);
    }

    private void ScaleGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            //DebugText("Scaled: {0}, Focus: {1}, {2}", scaleGesture.ScaleMultiplier, scaleGesture.FocusX, scaleGesture.FocusY);
            //Earth.transform.localScale *= scaleGesture.ScaleMultiplier;
            TryAddToScale(new Vector3(_scaleGesture.ScaleMultiplier, _scaleGesture.ScaleMultiplier, _scaleGesture.ScaleMultiplier));
        }
    }

    public void TryAddToScale(Vector3 scaleVector)
    {
        if (!targetImage.gameObject.activeSelf)
        {
            return;
        }
        targetImage.rectTransform.localScale += scaleVector;
    }

    public void ResetImageScale()
    {
        targetImage.rectTransform.localScale = _startingImageScale;
    }*/
}
