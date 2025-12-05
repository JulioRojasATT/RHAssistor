using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FurnitureContainerUI : ContainerUI<FurnitureInfo>
{
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private TextMeshProUGUI itemNameText;

    [SerializeField] private Transform objectRenderingTransform;

    public override void LoadInfo(FurnitureInfo entityInfo) {
        infoDisplayed = entityInfo;
        TryWriteText(itemNameText, entityInfo.displayName);
        TryWriteText(descriptionText, entityInfo.Description);
    }

    public void LoadInfo(FurnitureData furnitureData) {
        Instantiate(furnitureData.FurniturePrefab, objectRenderingTransform.position, objectRenderingTransform.rotation, objectRenderingTransform);
        TryWriteText(itemNameText, furnitureData.displayName);
        TryWriteText(descriptionText, furnitureData.Description);
        //StartCoroutine(LoadInfoAfter(furnitureData, 2f));
    }

    public IEnumerator LoadInfoAfter(FurnitureData furnitureData, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        TryWriteText(itemNameText, furnitureData.displayName);
        TryWriteText(descriptionText, furnitureData.Description);
    }

    public override void OnContainerHovered()
    {
        throw new NotImplementedException();
    }

    public override void OnContainerSelected()
    {
        throw new NotImplementedException();
    }
}
