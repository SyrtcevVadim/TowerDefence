using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static Action onLoadingSceneCallback;

    public static Slider LoaderBar;
    public static GameObject ContinueText;

    public class SceneLoaderMonobehaviour : MonoBehaviour { };

    public static void LoadScene(string SceneName)
    {
        onLoadingSceneCallback = () =>
        {
            GameObject LoadingGameObject = new GameObject();
            LoadingGameObject.AddComponent<SceneLoaderMonobehaviour>().StartCoroutine(LoadSceneAsync(SceneName));
        };
        SceneManager.LoadScene("LoadingScene");

    }
    public static IEnumerator LoadSceneAsync(string SceneName)
    {
        Scene LoadingScene = SceneManager.GetActiveScene();
        GameObject RootGameObject = LoadingScene.GetRootGameObjects()[0];
        AsyncOperation LoadingOperation = SceneManager.LoadSceneAsync(SceneName);
        LoadingOperation.allowSceneActivation = false;
        while (!LoadingOperation.isDone)
        {
            LoaderBar.value = LoadingOperation.progress;
            if (LoadingOperation.progress >= 0.9f)
            {
                if (!ContinueText.activeSelf)
                {
                    ContinueText.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    LoadingOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
