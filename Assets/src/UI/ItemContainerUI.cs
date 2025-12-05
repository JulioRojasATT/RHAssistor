using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainerUI : MonoBehaviour {
    [SerializeField] private Image gradientImage;

    [SerializeField] private Image itemImage;

    public void OnPickedUp(){
        gradientImage.gameObject.SetActive(true);
        itemImage.color = Color.white;
    }

    public void OnDropped()
    {
        gradientImage.gameObject.SetActive(false);
        itemImage.color = new Color(0.43f, 0.43f, 0.43f, 1f);
    }
}
