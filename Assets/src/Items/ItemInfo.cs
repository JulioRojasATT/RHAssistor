using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

public class ItemInfo : MonoBehaviourGeneralInfo
{
    [Header("Info")]
    [SerializeField] private List<Characteristic> characteristics;
    public List<Characteristic> Characteristics => characteristics;

    [Header("Versions")]
    [SerializeField] private List<MeshRenderer> meshes;

    [SerializeField] private List<ItemVersion> versions;
    public List<ItemVersion> Versions => versions;

    public string currentVersion;

    [Header("Editor generation")]
    [SerializeField] private Object modelAsset;

    [SerializeField] private string deviceModelFolder = "Assets/Res/model/items";

    [SerializeField] private string newVersionName;
    
    [SerializeField] private List<Material> originalMaterials;

    public void SwapToVersion(string versionName) {
        ItemVersion version = versions.Find(x => x.ID == versionName);
        int currentlyAssignedMaterials = 0;
        for (int i = 0; i<meshes.Count; i++) {
            meshes[i].sharedMaterials = version.MaterialsPerRenderer.GetRange(currentlyAssignedMaterials,
                meshes[i].sharedMaterials.Length).ToArray();
            Color[] colorArray = version.ColorsPerRenderer.GetRange(currentlyAssignedMaterials,
                meshes[i].sharedMaterials.Length).ToArray();
            for (var j = 0; j < meshes[i].sharedMaterials.Length; j++) {
                meshes[i].sharedMaterials[j].color = colorArray[j];
            }
            currentlyAssignedMaterials += meshes[i].sharedMaterials.Length;
        }
    }

    public void CalculateMaterialsAndColorsOfVersion(string versionName){
        List<Material> materials = new List<Material>();
        meshes.ForEach(m=>materials.AddRange(m.sharedMaterials));
        List<Color> colors = new List<Color>();
        meshes.ForEach(m=>colors.AddRange(m.sharedMaterials.Select(sm => sm.color)));
        versions.Find(x=>x.ID.Equals(versionName)).SetMaterialsAndColors(materials,colors);
    }

    public void CalculateDefaultVersionAndMaterials() {
        meshes = GetComponentsInChildren<MeshRenderer>().ToList();
        HashSet<Material> originalMaterialsHashSet = new HashSet<Material>();
        foreach (MeshRenderer meshRenderer in meshes) {
            foreach (Material meshRendererSharedMaterial in meshRenderer.sharedMaterials) {
                originalMaterialsHashSet.Add(meshRendererSharedMaterial);
            }
        }
        originalMaterials = originalMaterialsHashSet.ToList();
        CreateNewVersionUsingCurrentState("Default");
    }

    public void ExtractModelMaterials() {
        #if UNITY_EDITOR
        string modelPath = TryGetModelAsset();
        string folderPath = TryCreateFolder(modelPath.Substring(0, modelPath.Length - modelAsset.name.Length - 5),"OriginalMaterials");
        try {
            AssetDatabase.StartAssetEditing();
            var assetsToReload = new HashSet<string>();
            var materials = AssetDatabase.LoadAllAssetsAtPath(modelPath).Where(x => x.GetType() == typeof(Material));
            foreach (var material in materials){
                Material m = material as Material;
                var newAssetPath = folderPath + "/" + material.name + ".mat"; // -4 is for .fbx
                newAssetPath = AssetDatabase.GenerateUniqueAssetPath(newAssetPath);
                var error = AssetDatabase.ExtractAsset(material, newAssetPath);
                if (string.IsNullOrEmpty(error)){
                    Debug.Log("Error loading asset. Adding entry to reload.");
                    assetsToReload.Add(modelPath);
                }
            }
            foreach (var path in assetsToReload){
                AssetDatabase.WriteImportSettingsIfDirty(path);
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }
        }
        finally {
            AssetDatabase.StopAssetEditing();
        }
        #endif
        
    }
    
    public void CreateNewVersionUsingCurrentState(string newVersionName) {
        List<Material> materials = new List<Material>();
        meshes.ForEach(m=>materials.AddRange(m.sharedMaterials));
        CreateNewVersionGameObject(newVersionName, materials);
    }

    public void CreateNewVersionGameObject(string newVersionName, List<Material> materials) {
        GameObject newVersionGameObject = new GameObject(newVersionName + "Version");
        newVersionGameObject.transform.parent = transform;
        // Assign of materials
        ItemVersion newVersion = newVersionGameObject.AddComponent<ItemVersion>();
        newVersion.SetId(newVersionName);
        newVersion.SetMaterials(materials);
        newVersion.SetColors(materials.Select(x=>x.color).ToList());
        versions.Add(newVersion);
        SwapToVersion(newVersion.ID);
    }

    public List<Material> GetRendererMaterialReplacements(List<Material> replacementMaterials, string version) {
        List<Material> result = new List<Material>();
        Dictionary<string, Material> replacementsByName = replacementMaterials.ToDictionary(x => x.name);
        foreach (MeshRenderer meshRenderer in meshes) {
            foreach (Material meshRendererSharedMaterial in meshRenderer.sharedMaterials) {
                if (replacementsByName.ContainsKey(meshRendererSharedMaterial.name + version)) {
                    result.Add(replacementsByName[meshRendererSharedMaterial.name + version]);
                }                
            }
        }
        return result;
    }

    public void GenerateNewVersion() {
        #if UNITY_EDITOR
        string modelFolderPath = TryGetModelAsset();
        modelFolderPath = modelFolderPath.Substring(0, modelFolderPath.Length - modelAsset.name.Length - 5);
        Debug.Log(modelFolderPath);
        string newFolderPath = TryCreateFolder(modelFolderPath, newVersionName + "Materials");
        // Create the folder
        if (newFolderPath.Length <= 0) {
            return;
        }
        List<Material> newMaterials = new List<Material>();
        // Create a simple material asset in the created folder
        foreach (Material originalMaterial in originalMaterials) {
            Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            material.SetFloat("_Smoothness", originalMaterial.GetFloat("_Smoothness"));
            material.SetFloat("_Metallic", originalMaterial.GetFloat("_Metallic"));
            material.SetFloat("_Cutoff", originalMaterial.GetFloat("_Cutoff"));
            material.SetColor("_BaseColor", originalMaterial.GetColor("_BaseColor"));
            material.SetTexture("_BaseMap", originalMaterial.GetTexture("_BaseMap"));
            string newAssetPath = newFolderPath + "/" + originalMaterial.name + newVersionName + ".mat";
            AssetDatabase.CreateAsset(material, newAssetPath);
            Debug.Log(AssetDatabase.GetAssetPath(material));
            newMaterials.Add(material);
        }
        // Create a new game object with the version and 
        CreateNewVersionGameObject(newVersionName, GetRendererMaterialReplacements(newMaterials, newVersionName));
        #endif
    }
    
    public string TryCreateFolder(string folderPath, string folderName) {
        #if UNITY_EDITOR
        // If already created, create nothing.
        if (AssetDatabase.IsValidFolder(folderPath + "/" + folderName)) {
            Debug.Log("Folder already created. Not creating new one");
            return folderPath + "/" + folderName;
        }
        string guid = AssetDatabase.CreateFolder(folderPath, folderName);
        string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
        return newFolderPath;
        #endif
        return "";
    }
    
    public string TryGetModelAsset() {
        #if UNITY_EDITOR
        if (modelAsset == null) {
            string modelPath = EditorUtility.OpenFilePanel("Select model", deviceModelFolder, "fbx");
            if (modelPath.StartsWith(Application.dataPath)) {
                modelPath = "Assets" + modelPath.Substring(Application.dataPath.Length);
            }
            modelAsset = AssetDatabase.LoadAssetAtPath(modelPath, typeof(Object)) as Object;
            return modelPath;
        }
        return AssetDatabase.GetAssetPath(modelAsset);
        #endif
        return "";
    }
}