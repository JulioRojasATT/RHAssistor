using DamageNumbersPro;
using UnityEngine;

public class OnScreenMeshNotificationSpawner : MonoBehaviour
{
    [SerializeField] private DamageNumber damageNumberPrefab;

    private string [] GOOD_MESSAGES = new string[] { "Good!", "Nice!", "Excelent!" };

    public void SpawnGoodMessage(Interactable interactable)
    {
        DamageNumber newNotification = damageNumberPrefab.Spawn(interactable.transform.position);
        newNotification.rightText = GOOD_MESSAGES[Random.Range(0, GOOD_MESSAGES.Length)];
        newNotification.gameObject.SetActive(true);
    }
}
