using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public float changetime = 3;
    public string scenename;

    public void ChangeScene()
    {
            SceneManager.LoadScene(scenename);
        changetime -= Time.deltaTime;
        Time.timeScale = 1f;
    }
}
