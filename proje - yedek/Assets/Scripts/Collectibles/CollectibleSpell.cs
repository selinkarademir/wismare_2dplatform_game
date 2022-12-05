using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CollectibleSpell : MonoBehaviour
{
    [SerializeField] private int score; 
    [SerializeField] public TextMeshProUGUI scoreText;

    GameController GC;

    private void Start()
    {
       GC = GameController.instance;
    }

    public void UpdateScore()
    {
        GC.UpdateScore(score);
       
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Temas va");
            UpdateScore();

            GC.data.Score += score;

            gameObject.SetActive(false);

        }
    }

}
