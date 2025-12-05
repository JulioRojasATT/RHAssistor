using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVersion : MonoBehaviour
{
    [SerializeField] private string id;
    public string ID => id;
    
    [SerializeField] private List<Material> materialsPerRenderer;
    public List<Material> MaterialsPerRenderer => materialsPerRenderer;

    [SerializeField] private List<Color> colorsPerRenderer;
    public List<Color> ColorsPerRenderer => colorsPerRenderer;

    [SerializeField] private Color uiColor;
    public Color UIColor=> uiColor;

    [SerializeField] private Sprite uiImage;
    public Sprite UIImage => uiImage;

    public void SetMaterialsAndColors(List<Material> materialsPerRenderer, List<Color> colorsPerRenderer){
        this.materialsPerRenderer = materialsPerRenderer;
        this.colorsPerRenderer = colorsPerRenderer;
    }

    public void SetMaterials(List<Material> materials) {
        materialsPerRenderer = materials;
    }

    public void SetColors(List<Color> colors) {
        colorsPerRenderer = colors;
    }

    public void SetId(string id) {
        this.id = id;
    }
}
