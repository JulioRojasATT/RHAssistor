using UnityEngine;

public class NetworkSelector : GenericSelector<NetworkBubble>
{
    public override void StartSelection(Vector2 screenPosition)
    {
        return;
    }

    public override void StopSelection(Vector2 screenPosition)
    {
        return;
    }

    public override void TryInteractWithObjectsAtPosition(Vector2 screenPosition)
    {
        RaycastHit2D hit = Physics2D.CircleCast(m_Camera.ScreenToWorldPoint(screenPosition), interactionRadius, Vector2.zero);
        if (!hit || !hit.collider || !hit.collider.TryGetComponent(out NetworkBubble interactedBubble))
        {
            return;
        }
        Debug.Log("Selecting " + interactedBubble.gameObject.name);
        Select(interactedBubble);
    }
}
