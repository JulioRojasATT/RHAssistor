[System.Serializable]
public class FirebaseUserData
{
    public string id = "ur4550";

    public bool gameCompleted = false;
    
    public int uniqueViews;
    
    public float totalTimePlayed;

    public int errorAmount;

    /// <summary>
    /// Scores
    /// </summary>
    public int[] levelsScore;

    public int timesPlayedInPC;
    
    public int timesPlayedInAndroid;
    
    public int timesPlayedIniOS;
    
    public FirebaseUserData(string userID) {
        this.id= userID;
        this.levelsScore = new int[4];
    }

    public void OnLevelWon(bool isFinalLevel, int levelIndex, int levelScore, float timeElapsedInLevel) {
        // If game isn't completed yet, update the game completed flag
        if (!gameCompleted) {
            gameCompleted = isFinalLevel;
        }

        if (levelsScore[levelIndex] < levelScore) {
            levelsScore[levelIndex] = levelScore;
        }

        totalTimePlayed += timeElapsedInLevel;
    }

    public override string ToString() {
        return id;
    }

    public override int GetHashCode() {
        return id.GetHashCode();
    }
}