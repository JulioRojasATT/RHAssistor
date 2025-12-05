using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineRendererTextureScroller : MonoBehaviour {

    [SerializeField] private float scrollSpeed = 5f;

    [SerializeField] private Material materialToUpdate;

    [SerializeField] private List<LineRenderer> lineRenderersToUpdate;

    private Vector3 _initialMaterialOffset;

    public List<Material> materialsToUpdate;

    private void Start()
    {
        materialsToUpdate = lineRenderersToUpdate.Select(x=>x.material).ToList();
        _initialMaterialOffset = materialToUpdate.mainTextureOffset;
    }

    private void Update()
    {
        lineRenderersToUpdate.ForEach(lineRenderer => lineRenderer.material.mainTextureOffset = new Vector2(scrollSpeed * Time.time, _initialMaterialOffset.y));
    }
}