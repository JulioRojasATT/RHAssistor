using UnityEngine;

[ExecuteInEditMode]
public class VirtualLightmapBaker : MonoBehaviour
{
    public Camera bakeCamera;
    public int textureSize = 1024;
    public LayerMask bakeLayer;
    public Material targetMaterial;
    public string lightmapPropertyName = "_MainTex";

    private RenderTexture virtualLightmap;

    void Start()
    {
        CreateVirtualLightmap();
        BakeLightmap();
    }

    void CreateVirtualLightmap()
    {
        virtualLightmap = new RenderTexture(textureSize, textureSize, 16, RenderTextureFormat.ARGB32);
        virtualLightmap.name = "VirtualLightmap";
        virtualLightmap.Create();
    }

    public void BakeLightmap()
    {
        if (bakeCamera == null)
        {
            Debug.LogError("Bake Camera is not assigned.");
            return;
        }

        // Set up the camera
        bakeCamera.targetTexture = virtualLightmap;
        bakeCamera.cullingMask = bakeLayer;
        bakeCamera.Render();

        // Assign to material
        if (targetMaterial != null)
        {
            targetMaterial.SetTexture(lightmapPropertyName, virtualLightmap);
            Debug.Log("Virtual lightmap applied.");
        }
    }

    void OnDestroy()
    {
        if (virtualLightmap != null)
        {
            virtualLightmap.Release();
        }
    }
}