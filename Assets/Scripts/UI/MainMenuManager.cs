using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Level");
    }
    public void OnSettingsButtonClick()
    {

    }
    public void OnStatsButtonClick()
    {

    }
    public void OnCreditsButtonClick()
    {

    }
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
