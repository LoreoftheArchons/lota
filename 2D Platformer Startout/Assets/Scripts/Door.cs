using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour {

    public int LevelToLoad;
    private GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gm.InputText.text = ("[E] to Enter");
            if (Input.GetKeyDown("e"))
            {
                SaveScore();
                SceneManager.LoadScene("testground2");
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown("e"))
            {
                SaveScore();
                SceneManager.LoadScene("testground2");
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gm.InputText.text = (" ");
        }
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt("Score", gm.points);
    }
}
