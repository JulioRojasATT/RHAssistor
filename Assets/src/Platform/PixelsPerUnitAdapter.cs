using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PixelsPerUnitAdapter : ScreenSizeAdapter
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void AdaptUsingScreenWidth()
    {
        _image.pixelsPerUnitMultiplier = AdaptUsingWidth(_image.pixelsPerUnitMultiplier);
    }

    public override void AutoAdapt()
    {
        AdaptUsingScreenWidth();
    }
}