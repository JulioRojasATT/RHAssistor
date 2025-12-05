using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class ScreenResolutionController : Button {

    [SerializeField] private int defaultScreenWidth;

    [SerializeField] private int defaultScreenHeight;

    private int SCREEN_WIDTH = 1080, SCREEN_HEIGHT = 1920;

    private float TARGET_ASPECT_RATIO = 1080f / 1920f;

    public override void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pressed full screen button");
        ToggleFullScreen();
        base.OnPointerDown(eventData);
    }

    public void SetScreenResolution()
    {
        Screen.fullScreen = true;
        StartCoroutine(ToggleScreenResolutionAfterTime(0, 0, 1f));
    }

    public IEnumerator ToggleScreenResolutionAfterTime(int screenWidth, int screenHeight, float time)
    {
        yield return new WaitForSeconds(time);
        int targetScreenWidth = Mathf.FloorToInt(Screen.height * TARGET_ASPECT_RATIO);
        Screen.SetResolution(targetScreenWidth, Screen.height, true);
        Screen.SetResolution(screenWidth, screenHeight, true);
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public IEnumerator ToggleFullScreenAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        int targetScreenWidth = Mathf.FloorToInt(Screen.height * TARGET_ASPECT_RATIO);
        Screen.fullScreen = !Screen.fullScreen;
    }
}