using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkenerController : MonoBehaviour
{
    /// <summary>
    /// Image that darkens / fades the scenes.
    /// </summary>
    public Image darkener;

    public float fadeTime;

    protected virtual void Awake()
    {
        darkener.gameObject.SetActive(true);
    }

    public IEnumerator OpaqueDarkenerCor()
    {
        yield return UIImageManager.InterpolateUImageColor(darkener, Color.black, fadeTime);
    }

    public IEnumerator FadeDarkenerCor()
    {
        yield return UIImageManager.FadeImage(darkener, fadeTime);
    }

    public void OpaqueDarkener()
    {
        StartCoroutine(UIImageManager.InterpolateUImageColor(darkener, Color.black, fadeTime));
    }

    public void FadeDarkener()
    {
        StartCoroutine(UIImageManager.FadeImage(darkener, fadeTime));
    }
}
