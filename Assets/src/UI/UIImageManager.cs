using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageManager : MonoBehaviour {
    
    /// <summary>
    /// Fades the image sprite in the given time
    /// </summary>
    /// <param name="i"></param>
    /// <param name="fadingTime"></param>
    /// <returns></returns>
    public static IEnumerator FadeImage(Image i, float fadingTime) {
        return InterpolateUImageColor(i, new Color(0, 0, 0, 0), fadingTime);
    }
    
    /// <summary>
    /// Interpolates the image color to the targetColor in interpolateTime units of time
    /// </summary>
    /// <param name="i"></param>
    /// <param name="targetColor"></param>
    /// <param name="interpolationTime"></param>
    /// <returns></returns>
    public static IEnumerator InterpolateUImageColor(Image i, Color targetColor, float interpolationTime) {
        Color initialColor = i.color;
        float currentTime = 0;
        while (currentTime<interpolationTime) {
            currentTime += Time.deltaTime;
            i.color = Color.Lerp(initialColor, targetColor, currentTime / interpolationTime);
            yield return new WaitForEndOfFrame();
        }
    }
    
    /// <summary>
    /// Opaques the sprite renderer sprite in the given time
    /// </summary>
    /// <param name="s"></param>
    /// <param name="fadingTime"></param>
    /// <returns></returns>
    public static IEnumerator OpaqueImage(Image i, float fadingTime) {
        return InterpolateUImageColor(i, Color.white, fadingTime);
    }
}
