using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class MonoBehaviourObjectPool<T> where T: Component{

    /// <summary>
    /// Sample of the object to have in the pool
    /// </summary>
    [SerializeField] public T sample;

    public int initialSamples;

    [SerializeField] private Transform _parent;

    protected Queue<T> availableObjects = new Queue<T>();
    public Queue<T> AvailableObjects => availableObjects;

    
    protected Queue<T> occupiedObjects = new Queue<T>();

    public int AvailableCount => availableObjects.Count;
    
    public int OccupiedCount => occupiedObjects.Count;
    
    public int GlobalCount => AvailableCount + OccupiedCount;

    public void RegisterAsAvailable(List<T> availableObjects) {
        availableObjects.ForEach(x=>this.availableObjects.Enqueue(x));
    }
    
    public void CreateInitialInstances() {
        CreateInstances(initialSamples);
    }

    public void CreateInstances(int instanceNumber) {
        for (int i = 0; i < instanceNumber; i++) {
            availableObjects.Enqueue(CreateNewObject());
        }
    }

    public void SetParent(Transform newParent) {
        _parent = newParent;
    }

    public List<T> GetAvailableObjects() {
        return availableObjects.ToList();
    }
    
    public void ForEachOccupied(Action<T> actionToApply) {
        foreach (T availableObject in occupiedObjects) {
            actionToApply.Invoke(availableObject);
        }
    }
    
    public void ForEachAvailable(Action<T> actionToApply) {
        foreach (T availableObject in availableObjects) {
            actionToApply.Invoke(availableObject);
        }
    }

    public T OccupyOne(out bool createdNewInstance) {
        createdNewInstance = false;
        if (availableObjects.Count <= 0) {
            availableObjects.Enqueue(CreateNewObject());
            createdNewInstance = true;
        }
        T newObject = availableObjects.Dequeue();
        occupiedObjects.Enqueue(newObject);
        return newObject;
    }

    public void ReturnToPool(T elementToDisOccupy) {
        if (occupiedObjects.Peek() == elementToDisOccupy) {
            occupiedObjects.Dequeue();
        }
        availableObjects.Enqueue(elementToDisOccupy);
    }

    public void ReturnAllToPool() {
        while (occupiedObjects.Count>0) {
            availableObjects.Enqueue(occupiedObjects.Dequeue());
        }
    }

    public T CreateNewObject() {
        return GameObject.Instantiate(sample.gameObject, _parent).GetComponent<T>();
    }
}
