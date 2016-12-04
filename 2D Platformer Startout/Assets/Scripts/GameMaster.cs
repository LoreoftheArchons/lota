using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public int points;
    public int highscore = 0;

    public Text pointsText;
    public Text InputText;

    void Start()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                PlayerPrefs.DeleteKey("Score");
                points = 0;
            }
            else
            {
                points = PlayerPrefs.GetInt("Score");
            }
        }

        if (PlayerPrefs.HasKey("Highscore"))
        {
            highscore = PlayerPrefs.GetInt("Highscore");
        }

    }

    void Update()
    {
        pointsText.text = ("Points: " + points);
    }
}
