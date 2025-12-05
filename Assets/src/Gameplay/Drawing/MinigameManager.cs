using UnityEngine;
using UnityEngine.Events;

public abstract class MinigameManager : MonoBehaviour {

    [Header("Progress")]
    [SerializeField] private int minProgress;

    [SerializeField] private int maxProgress;

    [SerializeField] private int progress;

    [SerializeField] private UnityEvent onProgress;

    [Header("Events")]
    [SerializeField] protected UnityEvent onGameStarted;
    
    [SerializeField] protected UnityEvent onGameEnded;
    
    [SerializeField] protected UnityEvent onGameLost;
    
    [SerializeField] protected UnityEvent onGameWon;

    [Header("Game state management")]
    [SerializeField] private MiniGameState gameState;

    public bool gameWon => gameState == MiniGameState.WON;

    public bool gameLost => gameState == MiniGameState.LOST;

    public bool gameOnGoing => gameState == MiniGameState.ON_GOING;

    public bool gameEnded => gameWon || gameLost;

    public enum MiniGameState
    {
        ON_GOING,
        WON,
        LOST
    }

    public virtual void StartGame()
    {
        ResetProgress();
        onGameStarted.Invoke();
        gameState = MiniGameState.ON_GOING;
    }

    public void LoseGame() {
        gameState = MiniGameState.LOST;
        onGameLost.Invoke();
        EndGame();
    }

    public void WinGame()
    {
        gameState = MiniGameState.WON;
        onGameWon.Invoke();
        EndGame();
    }    

    public void EndGame()
    {
        onGameEnded.Invoke();
    }

    public void ResetProgress()
    {
        progress = 0;
    }

    public void Progress()
    {
        Progress(1);
    }

    public void Progress(bool progressBoolValue)
    {
        Progress(progressBoolValue ? 1 : -1);
    }

    public void Progress(int progressAmount)
    {
        if (gameEnded) {
            return;
        }
        progress += progressAmount;
        gameState = progress == maxProgress ? MiniGameState.WON : MiniGameState.ON_GOING;
        if(gameWon) {
            WinGame();
        }
    }
}
