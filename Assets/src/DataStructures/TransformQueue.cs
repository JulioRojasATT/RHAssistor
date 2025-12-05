using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransformQueue : MonoBehaviour
{
    private int index;
    public int Index => index;

    [SerializeField] private GenericEventBroadcaster onQueueFilledEvent;

    [SerializeField] private List<Transform> objectPositions;

    [Header("Random management")]
    [SerializeField] private bool random = false;

    private int[] randomIndexes;

    public int MaxItems => objectPositions.Count;

    private Stack<Transform> queue = new Stack<Transform>();

    public int ItemCount => queue.Count;

    [SerializeField] private UnityEvent onLocked;

    public void Initialize(int index)
    {
        this.index = index;
        randomIndexes = ArrayExtensions.GetRandomlyShuffledIntArray(objectPositions.Count);
    }

    public bool ContainsCards()
    {
        return queue.Count > 0;
    }

    public void CheckCompletion()
    {
        if (queue.Count < MaxItems)
        {
            return;
        }
        onQueueFilledEvent.BroadCast(this, System.EventArgs.Empty);
    }

    public void Lock()
    {
        onLocked?.Invoke();
    }

    public void DequeueAll()
    {
        queue.Clear();
    }

    public bool TryEnqueue(Transform item)
    {
        if (queue.Count >= MaxItems)
        {
            return false;
        }
        item.transform.position = random ? objectPositions[randomIndexes[queue.Count]].position : objectPositions[queue.Count].position;
        queue.Push(item);
        CheckCompletion();
        return true;
    }

    public bool TryDequeue()
    {
        if (queue.Count <= 0)
        {
            return false;
        }
        queue.Pop();
        return true;
    }

    public bool TryDequeue(out Transform sortCard)
    {
        if (queue.Count <= 0)
        {
            sortCard = null;
            return false;
        }
        sortCard = queue.Pop();
        return true;
    }
}