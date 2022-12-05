using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CanvasController : MonoBehaviour
{
   [SerializeField] private int score = 0; //this score needs to be maintened acrros multiple levels
   [SerializeField] public TextMeshProUGUI scoreText;

    private bool isGameStoped;

    [Header("In Game Panel")]
    public GameObject InGamePanel;

    [Header("End Game Panel")]
    public GameObject EndGamePanel;
    public TextMeshProUGUI endGameScoreText;
    public Button buttonAbilityUpgrade;
    public Button buttonHealtUpgrade;
    public Button buttonDamageUpgrade;


    [Header("Pause Panel")]
    public GameObject PausePanel;

    public GameController GC;
    public DataBase dataBase;

    public static CanvasController instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        GC = GameController.instance;
      
        if (GC.data.isFirstTime)
        {
            GC.data.Score = 0;
            GC.data.isFirstTime = false;
            DataManager.SaveData(dataBase);
        }
        score = 0;
        scoreText.text = "SCORE: " + score.ToString();
        CheckData();

    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGameStoped)
            {
                isGameStoped = true;
                InGamePanel.SetActive(false);
                PausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                isGameStoped = false;
                InGamePanel.SetActive(true);
                PausePanel.SetActive(false);

                Time.timeScale = 1;
              
            }
        }
    }

    public void ExitGameButtonAction()
    {
        Application.Quit();
    }

    public void StopButtonAction()
    {
        isGameStoped = false;
        InGamePanel.SetActive(true);
        PausePanel.SetActive(false);

        Time.timeScale = 1;
    }

    private void CheckData() 
    {
        endGameScoreText.text = "Total Score  " + GC.data.Score;
        if (GC.data.playerData.currentHealthIndex < GC.data.playerData.HealthCosts.Count - 1)
        {
          
            buttonHealtUpgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GC.data.playerData.HealthCosts[GC.data.playerData.currentHealthIndex].ToString();

        }
        else
        {
            buttonHealtUpgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAX";
            buttonHealtUpgrade.interactable = false;
        }


        if (!GC.data.playerData.isDoubleJumpActive)
        {
           
            buttonAbilityUpgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GC.data.playerData.AbilityCosts[GC.data.playerData.currentAbilityIndex].ToString();

        }
        else
        {
            buttonAbilityUpgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAX";
            buttonAbilityUpgrade.interactable = false;
        }


        if (GC.data.playerData.currentDamageIndex < GC.data.playerData.DamageCosts.Count - 1)
        {
          
            buttonDamageUpgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GC.data.playerData.DamageCosts[GC.data.playerData.currentDamageIndex].ToString();

        }
        else
        {
            buttonDamageUpgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAX";
            buttonDamageUpgrade.interactable = false;
        }
    }


    public void UpdateScore(int Score)
    {
        score += Score;
        scoreText.text = "SCORE: " + score.ToString();
   }

    public void EndGamePanelActions()
    {
        InGamePanel.SetActive(false);
        EndGamePanel.SetActive(true);
       // GC.data.Score += score;
        DataManager.SaveData(dataBase);
        endGameScoreText.text = "Total Score  " + GC.data.Score;

        CheckButtonCosts(1);
    }


    public void ButtonAbilityUpgradeAction()
    {
        if(GC.data.playerData.currentAbilityIndex == 0)
        {
            GC.data.playerData.isDoubleJumpActive = true;
        }
        else if (GC.data.playerData.currentAbilityIndex == 1)
        {
            GC.data.playerData.isDashActive = true;
        }

        GC.data.Score -= GC.data.playerData.AbilityCosts[GC.data.playerData.currentAbilityIndex];
        endGameScoreText.text = "Total Score  " + GC.data.Score;

        if (GC.data.playerData.currentAbilityIndex <= GC.data.playerData.AbilityCosts.Count)
        {
            GC.data.playerData.currentAbilityIndex++;
            buttonAbilityUpgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAX";

            buttonAbilityUpgrade.interactable = false;

        }
      

        DataManager.SaveData(dataBase);

        CheckButtonCosts(0);
    }

    public void ButtonHealtUpgradeAction()
    {
        GC.data.Score -= GC.data.playerData.HealthCosts[GC.data.playerData.currentHealthIndex];
        endGameScoreText.text = "Total Score  " + GC.data.Score;

        if (GC.data.playerData.currentHealthIndex < GC.data.playerData.HealthCosts.Count-1)
        {
            GC.data.playerData.currentHealthIndex++;
            buttonHealtUpgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GC.data.playerData.HealthCosts[GC.data.playerData.currentHealthIndex].ToString();

        }
        else
        {
            buttonHealtUpgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAX";
            Debug.Log("MAX");

            buttonHealtUpgrade.interactable = false;
        }
        

        DataManager.SaveData(dataBase);
        CheckButtonCosts(0);
    }

    public void ButtonDamageUpgradeAction()
    {
        GC.data.Score -= GC.data.playerData.DamageCosts[GC.data.playerData.currentDamageIndex];
        endGameScoreText.text = "Total Score  " + GC.data.Score;

        if (GC.data.playerData.currentDamageIndex < GC.data.playerData.DamageCosts.Count-1)
        {
            GC.data.playerData.currentDamageIndex++;
            buttonDamageUpgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GC.data.playerData.DamageCosts[GC.data.playerData.currentDamageIndex].ToString();

        }
        else
        {
            buttonDamageUpgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAX";
            Debug.Log("MAX");
           buttonDamageUpgrade.interactable = false;
        }
        DataManager.SaveData(dataBase);
        CheckButtonCosts(0);
    }
   

    void CheckButtonCosts(int index)
    {
        if (GC.data.Score < GC.data.playerData.AbilityCosts[0] || GC.data.playerData.isDoubleJumpActive )
        {
            buttonAbilityUpgrade.interactable = false;
        }

        if (GC.data.Score < GC.data.playerData.HealthCosts[GC.data.playerData.currentHealthIndex] || GC.data.playerData.currentHealthIndex >= GC.data.playerData.HealthCosts.Count-index)
        {
            buttonHealtUpgrade.interactable = false;
        }


        if (GC.data.Score < GC.data.playerData.DamageCosts[GC.data.playerData.currentDamageIndex] || GC.data.playerData.currentDamageIndex >= GC.data.playerData.DamageCosts.Count-index)
        {
            buttonDamageUpgrade.interactable = false;
        }
    }
  
}
