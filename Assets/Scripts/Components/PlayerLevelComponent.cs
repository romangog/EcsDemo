using System;

[Serializable]
public struct PlayerLevelComponent
{
    public int PlayerLevel;
    public int NextLevelXP;
    public int PlayerCurrentXP;

    public void Reinitialize()
    {
        PlayerLevel = 1;
        NextLevelXP = 4;
        PlayerCurrentXP = 0;
    }
}




