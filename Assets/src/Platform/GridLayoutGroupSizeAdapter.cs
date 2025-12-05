using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridLayoutGroupSizeAdapter : ScreenSizeAdapter
{

    private GridLayoutGroup m_GridLayoutGroup;

    [SerializeField] private RectTransform contentContainer;

    [SerializeField] private int rowNumber, columnNumber;

    private float originalLayoutHeight = 273, originalLayoutWidth = 835;

    private void Awake()
    {
        m_GridLayoutGroup= GetComponent<GridLayoutGroup>();
    }

    public override void AutoAdapt()
    {
        ResizeGridLayoutUsingContainerDimensions();
    }

    public void ResizeGridLayoutAfterSeconds(float seconds)
    {
        StartCoroutine(ResizeGridLayoutAfterSecondsCor(seconds));
    }

    public IEnumerator ResizeGridLayoutAfterSecondsCor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ResizeGridLayoutUsingContainerDimensions();
    }

    public void ResizeGridLayoutUsingContainerDimensions()
    {
        m_GridLayoutGroup.padding.left = Mathf.FloorToInt(Adapt(originalLayoutWidth, contentContainer.rect.width, m_GridLayoutGroup.padding.left));
        m_GridLayoutGroup.padding.right = Mathf.FloorToInt(Adapt(originalLayoutWidth, contentContainer.rect.width, m_GridLayoutGroup.padding.right));
        m_GridLayoutGroup.padding.bottom = Mathf.FloorToInt(Adapt(originalLayoutHeight, contentContainer.rect.height, m_GridLayoutGroup.padding.bottom));
        m_GridLayoutGroup.padding.top = Mathf.FloorToInt(Adapt(originalLayoutHeight, contentContainer.rect.height, m_GridLayoutGroup.padding.top));
        m_GridLayoutGroup.spacing = new Vector2(Adapt(originalLayoutWidth, contentContainer.rect.width, m_GridLayoutGroup.spacing.x),
            Adapt(originalLayoutHeight, contentContainer.rect.height, m_GridLayoutGroup.spacing.y));
        m_GridLayoutGroup.cellSize = Vector2.one * (contentContainer.rect.width - m_GridLayoutGroup.padding.left -
                                                  m_GridLayoutGroup.padding.right - m_GridLayoutGroup.spacing.x * (columnNumber-1)) / columnNumber;
    }
}
