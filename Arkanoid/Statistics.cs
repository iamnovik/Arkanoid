using System.Threading.Tasks.Sources;

namespace Arkanoid;

public class Statistics
{
    public int score;
    private int time;
    public int bestScore;
    private int bestTime;
    private int healths;

    Statistics GetStats()
    {
        return null;
    }

    public Statistics()
    {
        score = 0;
    }
    public void UpdateScore()
    {
        score += 10;
    }
    void UpdateTime(){}
    void UpdateHealth(){}

    public void UpdateBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
        }
    }

   
    void UpdateBestTime(){}

}