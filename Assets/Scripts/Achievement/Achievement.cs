[System.Serializable]
public class Achievement
{
    public string name;
    public bool isUnlocked;

    public Achievement(string name)
    {
        this.name = name;
        this.isUnlocked = false;
    }
}