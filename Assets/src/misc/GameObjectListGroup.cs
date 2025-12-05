using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameObjectListGroup : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;

    public void EnableAll()
    {
        ApplyToAll(x=>x.SetActive(true));
    }

    public void DisableAll()
    {
        ApplyToAll(x => x.SetActive(false));
    }

    public void ApplyToAll(Action<GameObject> toApply)
    {
        objects.ForEach(x => toApply.Invoke(x));
    }
}
