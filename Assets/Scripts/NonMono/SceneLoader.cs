using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static Action onLoadingSceneCallback;

    public static Slider LoaderBar;
    public static GameObject ContinueText;

    public class SceneLoaderMonobehaviour : MonoBehaviour { };
    /// <summary>
    /// ��������� ����� scene, ��������� ����������� �����
    /// </summary>
    /// <param name="SceneName">�������� ����������� �����</param>
    public static void LoadScene(string SceneName)
    {
        Scene sceneToLoad = SceneManager.GetSceneByName(SceneName);
        if (!sceneToLoad.IsValid())
        {
            Debug.LogError("SceneLoader: Invalid Scene name");
            return;
        }
        if(sceneToLoad == SceneManager.GetActiveScene())
        {
            Debug.LogWarning("SceneLoader: Attempt to load active scene, refused");
            return;
        }
        onLoadingSceneCallback = () =>
        {
            GameObject LoadingGameObject = new GameObject();
            LoadingGameObject.AddComponent<SceneLoaderMonobehaviour>().StartCoroutine(LoadSceneAsync(SceneName));
        };
        SceneManager.LoadScene("LoadingScene");

    }
    public static IEnumerator LoadSceneAsync(string SceneName)
    {
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
