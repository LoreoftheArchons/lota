using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public GameObject PauseUI;

    private bool paused = false;

    //when game starts pauseUI is not there, or else it would overlap everything
    void Start()
    {
        PauseUI.SetActive(false);
    }

    //when the pause menu gets updated
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            //cause initially set to false, switches it to true
            paused = !paused;
        }
        //brings up the pause menu cause its true now
        if (paused)
        {
            PauseUI.SetActive(true);
            //stops time in the game
            Time.timeScale = 0;
        }
        //unpausing
        if (!paused)
        {
            PauseUI.SetActive(false);
            //this sets time back to normal (huge not for chronomancy, setting it to below one gives slow mo)
            Time.timeScale = 1;
        }     
    }

    public void Resume()
    {
        paused = false;
    }

    public void Restart(){

        Application.LoadLevel(Application.loadedLevel);

    }
    
    public void mainMenu()
    {
        Application.LoadLevel(0);
    }

    public void quit()
    {
        Application.Quit();
    }
}
