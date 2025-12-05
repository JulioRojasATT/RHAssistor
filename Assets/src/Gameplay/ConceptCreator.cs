using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConceptCreator : MonoBehaviour
{
    [SerializeField] private ComboComponent comboComponentPrefab;    

    [SerializeField] private FloatScriptableValue zCoordinate;
    private float ZCoordinate => zCoordinate.Value;

    private List<ComboComponent> spawnedComponents = new List<ComboComponent>();

    /// <summary>
    /// Spawning parameters
    /// First Transform sets the starting x position. Last one sets the finishing x position.
    /// Transforms[index] sets the y positions of the quads at their respective rows
    /// </summary>
    [Header("Row spawning parameters")]
    [SerializeField] private List<Transform> twoRowTransforms;

    [SerializeField] private List<Transform> threeRowTransforms;

    [SerializeField] private List<Transform> fourRowTransforms;

    [SerializeField] private Transform conceptsParent;

    private List<List<Transform>> _rowTransforms;

    [Header("Radial spawning parameters")]

    [SerializeField] private float spawnRadius;

    [SerializeField] private Vector3 targetScale;

    [SerializeField] private float angleConstant;

    private void Awake()
    {
        _rowTransforms = new List<List<Transform>> { null, twoRowTransforms, threeRowTransforms, fourRowTransforms };
        AdaptToScreenResolution();
    }

    public void SpawnComponents(int componentNumber) {
        for (int i = 0; i < componentNumber; i++) {
            ComboComponent comboComponent = Instantiate(comboComponentPrefab, conceptsParent);
            comboComponent.transform.localScale = targetScale;
            spawnedComponents.Add(comboComponent);
        }
    }

    public void UpdateComboNamesAndIds(List<string> comboIDs, List<int> rows) {
        spawnedComponents.ForEach(x => x.SetRow(-1));
        for (int i = 0; i<spawnedComponents.Count && i<comboIDs.Count; i++) {
            spawnedComponents[i].SetUIText(comboIDs[i]);
            spawnedComponents[i].SetRow(rows[i]);
            spawnedComponents[i].ID = comboIDs[i];
        }
        PlaceComponentsInRows(comboIDs.Count);
        MixComboComponentPositions();
    }

    public void AdaptToScreenResolution()
    {
        //spawnRadius = Screen.height / Screen.width;
        //targetScale = Vector3.one * Screen.height / Screen.width;
    }    

    public void PlaceComponentsRadially()
    {        
        float angleFraction = angleConstant / spawnedComponents.Count;
        for (int i = 0;i < spawnedComponents.Count; i++)
        {
            float xComponent = Mathf.Sin(angleFraction * i) * spawnRadius, yComponent = Mathf.Cos(angleFraction * i) * spawnRadius;
            spawnedComponents[i].transform.position = transform.position + new Vector3(xComponent, yComponent, ZCoordinate);
        }
    }

    public void PlaceComponentsInRows(int comboEntries)
    {
        int maxYPosition = spawnedComponents.Max(x => x.Row);
        List<Transform> rowTransformLocations = _rowTransforms[maxYPosition];
        spawnedComponents.ForEach(x=>x.gameObject.SetActive(false));
        Dictionary<int, int> processedComponentsPerRow = new Dictionary<int, int>();
        spawnedComponents.ForEach(x => processedComponentsPerRow.TryAdd(x.Row, 0));
        for (int i = 0; i < comboEntries; i++) {
            // Check how many rows are in the same row as current spawned component, and lerp with it.
            ComboComponent comboComponent = spawnedComponents[i];
            comboComponent.gameObject.SetActive(true);
            int componentsInRow = spawnedComponents.Count(x => x.Row == comboComponent.Row);
            // If components in row are 0, set the xPosition to half. If not, lerp it using processedComponentsPerRow
            float lerpFactor = componentsInRow==1 ? 0.5f : processedComponentsPerRow[comboComponent.Row] / (float)(componentsInRow - 1);
            float xPosition = Vector3.Lerp(rowTransformLocations[0].position, rowTransformLocations[rowTransformLocations.Count -1].position, lerpFactor).x;
            processedComponentsPerRow[comboComponent.Row]++;
            comboComponent.transform.position = new Vector3(xPosition, rowTransformLocations[comboComponent.Row].position.y, zCoordinate.Value);
            comboComponent.transform.localScale = rowTransformLocations[0].localScale;
        }
    }

    public void MixComboComponentPositions()
    {
        int maxYPosition = spawnedComponents.Max(x => x.Row);
        // Because row goes from 0 to max, we must stop one step later to correctly include all rows
        for(int i = 0; i<maxYPosition+1; i++) {
            List<ComboComponent> componentsInRow = spawnedComponents.FindAll(x => x.Row == i);
            for (int j = 0; j < componentsInRow.Count-1; j++) {
                // Mix with 50% chance
                if (Random.Range(0f, 1f) <= 0.5f)
                {
                    continue;
                }
                // Swap current component position with any of the next ones in the row
                int randomlySelectedComponentIndex = Random.Range(j + 1, componentsInRow.Count);
                Vector3 randomlySelectedPosition = componentsInRow[randomlySelectedComponentIndex].transform.position;
                componentsInRow[randomlySelectedComponentIndex].transform.position = componentsInRow[j].transform.position;
                componentsInRow[j].transform.position = randomlySelectedPosition;
            }
        }
    }

    private void OnDrawGizmos()
    {
        #if UNITY_EDITOR
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, spawnRadius);
        #endif
    }
}

public class ComboComponentPool : MonoBehaviourObjectPool<ComboComponent>
{

}