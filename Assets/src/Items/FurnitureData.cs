using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureData : ItemMonoBehaviourData{
    [SerializeField] private GameObject furniturePrefab;

    public GameObject FurniturePrefab => furniturePrefab;
}
