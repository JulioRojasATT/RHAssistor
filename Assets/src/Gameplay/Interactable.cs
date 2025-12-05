using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent<Interactable> onHoverEntered;

    [SerializeField] private UnityEvent<Interactable> onHoverExited;

    [SerializeField] private UnityEvent<Interactable> onSelected;

    [SerializeField] private UnityEvent<Interactable> onUnselected;

    [SerializeField] protected bool isSelectable;
    public bool IsSelectable => isSelectable;

    public void OnHoverEntered(Interactable interactable)
    {
        onHoverEntered?.Invoke(interactable);
    }

    public void OnHoverExited(Interactable interactable)
    {
        onHoverExited?.Invoke(interactable);
    }

    public void OnSelected(Interactable interactable)
    {
        onSelected?.Invoke(interactable);
    }

    public void OnUnSelected(Interactable interactable)
    {
        onUnselected?.Invoke(interactable);
    }
}