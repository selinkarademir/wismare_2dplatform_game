using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{

    public int lastLevel;
    private int isFirstLevelFirstlyCompleted;
    int tryNum;
    public int level = 0;
    public int randomLevelIndex;
    private int totalScore;

    public static DataBase dataBase;
    public DataBase data;
    CanvasController UI;
    PlayerMovement playerMovement;

    public static GameController instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        if(data != null)
        {
            DataManager.LoadData(data);
            dataBase = data;
        }

    }

    void Start()
    {
        UI = CanvasController.instance;
        playerMovement = PlayerMovement.instance;
    }

    public void UpdateScore(int score)
    {
       totalScore += score;
       UI.UpdateScore(score);
    }

    public void EndGameActions()
    {
        UI.EndGamePanelActions();
        playerMovement.EndGameAction();
    }

    public void EndGameButtonAction()
    {
        lastLevel++;
        PlayerPrefs.SetInt("lastLevel", lastLevel);


        if (PlayerPrefs.GetInt("Level") < 2)
        {
            PlayerPrefs.SetInt("Level", (PlayerPrefs.GetInt("Level") + 1));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        else
        {
            lastLevel = 0;
            PlayerPrefs.SetInt("Level", 0);
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
    }
 
}
