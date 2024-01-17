using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image loadingBar;

    public void LoadScene(int sceneID)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progressVale = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.fillAmount = progressVale;
            yield return null;
        }
    }
}
