using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture Info", menuName = "ATTStore/Furniture Info")]
public class FurnitureInfo : GeneralInfo {

    [SerializeField] private GameObject furniturePrefab;

    public GameObject FurniturePrefab => furniturePrefab;
     
}
