[System.Serializable]
public class LevelScoreInfo
{
    /// <summary>
    /// Time spent playing the level
    /// </summary>
    public int elapsedTime;

    /// <summary>
    /// Score won on the level
    /// </summary>
    public int score;

    public string status;

    public LevelScoreInfo(int elapsedTime, int score, string status)
    {
        this.elapsedTime = elapsedTime;
        this.score = score;
        this.status = status;
    }
}