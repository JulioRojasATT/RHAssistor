using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NetworkBubbleAccumulator : MonoBehaviour
{
    [SerializeField] private int maxNetworkCount;

    [SerializeField] private UnityEvent onMaxNetworkCountReached;

    [SerializeField] private UnityEvent onMaxNetworkCountNotReached;

    private int _numberOfSelectedNetworks;

    public void OnNetworkBubbleClicked(NetworkBubble networkBubble)  {
        if(_numberOfSelectedNetworks >= maxNetworkCount && !networkBubble.selected)
        {
            return;
        }        
        networkBubble.selected = !networkBubble.selected;
        _numberOfSelectedNetworks += networkBubble.selected ? 1 : -1;
        if (networkBubble.selected)
        {
            networkBubble.OnSelected(networkBubble);
        } else
        {
            networkBubble.OnUnSelected(networkBubble);
        }
        UnityEvent networkCountEvent = _numberOfSelectedNetworks >= maxNetworkCount ? onMaxNetworkCountReached : onMaxNetworkCountNotReached;
        networkCountEvent?.Invoke();
    }
}