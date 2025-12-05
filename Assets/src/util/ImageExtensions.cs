using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageExtensions : MonoBehaviour
{
    private Image m_Image;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    public void SetColor(ScriptableColor color)
    {
        m_Image.color = color.Value;
    }
}
