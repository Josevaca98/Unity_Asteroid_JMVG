using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour
{

    public ScoreData sd;

    private void Awake()
    {
        sd = new ScoreData();
    }

    public IEnumerable<Score> GetHighScores()
    {
        return ScoreData.scores.OrderByDescending(x => x.score);
    }

    public void AddScore(Score score)
    {
        ScoreData.scores.Add(score);
    }
}
