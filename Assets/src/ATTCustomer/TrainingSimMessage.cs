[System.Serializable]
public struct TrainingSimMessage
{
    public TrainingSimMessage(string role, string content) {
        this.role = role;
        this.content = content;
    }
    public string role;
    public string content;
}