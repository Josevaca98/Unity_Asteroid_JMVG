using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreManager scoreManager;
    public TMP_InputField inputName;
    public PlayerMovement playerMovement;

    public void CheckPreviousData()
    {
        if(SaveSystem.LoadPlayer() != null)
        {
            PlayerData playerd = SaveSystem.LoadPlayer();
            scoreManager.AddScore(new Score(playerd.playerName, playerd.playerScore)); 
        }
    }

    public void SaveName()
    {           
        PlayerData playerd = SaveSystem.LoadPlayer();
        scoreManager.AddScore(new Score(playerd.playerName = inputName.text, playerd.playerScore));

        var scores = scoreManager.GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
            row.rank.text = (i + 1).ToString();
            row.name.text = scores[i].name;
            row.score.text = scores[i].score.ToString();
        }
    }
}
