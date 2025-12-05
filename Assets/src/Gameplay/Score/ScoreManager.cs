using UnityEngine;

public abstract class ScoreManager : MonoBehaviour
{
    [SerializeField] protected IntScriptableValue scoreValue;

    [SerializeField] protected IntScriptableValue defaultScore;

    public abstract void CalculateScore();
}