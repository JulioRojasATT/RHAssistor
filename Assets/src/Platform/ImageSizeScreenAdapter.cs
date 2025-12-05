using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSizeScreenAdapter : ScreenSizeAdapter
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public override void AutoAdapt()
    {
        Rect originalRectTransform = rectTransform.rect;
        rectTransform.rect.Set(originalRectTransform.left, originalRectTransform.right, originalRectTransform.width, originalRectTransform.height);
    }
}
