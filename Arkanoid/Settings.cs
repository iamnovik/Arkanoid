using SFML.System;

namespace Arkanoid;

public class Settings
{
    public int level;
    public float volume;
    public int difficulty;
    public Vector2u res;
    public Settings()
    {
        level = 1;
        volume = 0f;
        difficulty = 1;
        res = new Vector2u(1280, 720);
    }

    public String GetDiff()
    {
        switch (difficulty)
        {
            case 1:return "KID";
            case 2: return "EZ";
            case 3: return "MID";
            case 4: return "HARD";
            case 5: return "GG";
            default: return "InProgress..";
        }
    }
}