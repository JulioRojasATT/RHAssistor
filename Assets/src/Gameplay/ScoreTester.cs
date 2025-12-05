using UnityEngine;
using UnityEngine.Events;

public class ScoreTester : MonoBehaviour {
    [SerializeField] private UnityEvent onEnoughScoreEarned;
    
    [SerializeField] private UnityEvent onNotEnoughScoreEarned;

    [SerializeField] private int score;
    
    [SerializeField] private CompareOperation compareOperation;

    [SerializeField] private int valueToCompare;

    public void CheckScore() {
        bool successfulComparing;
        switch (compareOperation) {
            default:
            case CompareOperation.EQUAL:
                successfulComparing = score == valueToCompare;
                break;
            case CompareOperation.NOT_EQUAL:
                successfulComparing = score != valueToCompare;
                break;
            case CompareOperation.GREATER_THAN:
                successfulComparing = score > valueToCompare;
                break;
            case CompareOperation.GREATER_OR_EQUAL_THAN:
                successfulComparing = score >= valueToCompare;
                break;
            case CompareOperation.LESSER_THAN:
                successfulComparing = score < valueToCompare;
                break;
            case CompareOperation.LESSER_OR_EQUAL_THAN:
                successfulComparing = score <= valueToCompare;
                break;
        }
        if (successfulComparing) {
            Debug.Log("Enough score earned. Calling function.");
            onEnoughScoreEarned?.Invoke();
        } else {
            Debug.Log("Not enough score earned. Calling alternate function.");
            onNotEnoughScoreEarned?.Invoke();
        }
    }
    
    public void IncreaseScore() {
        score++;
    }
    
    public void IncreaseScore(int value) {
        score+=value;
    }

    public void ResetScore() {
        score = 0;
    }
}

public enum CompareOperation {
    EQUAL,
    NOT_EQUAL,
    GREATER_THAN,
    GREATER_OR_EQUAL_THAN,
    LESSER_THAN,
    LESSER_OR_EQUAL_THAN,
}