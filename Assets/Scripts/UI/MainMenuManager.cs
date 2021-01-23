using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // ссылки на объекты сцены для наглядного управления
    private Transform canvas;
    private GameObject mainMenuButtons;
    private GameObject difficultyMenuButtons;

    private void Start()
    {
        canvas = transform.GetChild(0);
        mainMenuButtons = canvas.Find("MainMenuBack").gameObject;
        difficultyMenuButtons = canvas.Find("DifficultyBack").gameObject;
    }
    public void OnStartButtonClick()
    {
        mainMenuButtons.SetActive(false);
        difficultyMenuButtons.SetActive(true);
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
