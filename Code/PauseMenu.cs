using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private static bool IsPause = false;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private string LoadShip = "ShipBegin";
    [SerializeField] private CharactorMotion charactorMotion;
    [SerializeField] PlayerHealth playerHealth;

    void Update()
    {
        MenuPause();
    }
    public void MenuPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerHealth.Isdead())
        {
            if (IsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {       
        charactorMotion.enabled = true;
         PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Pause()
    {
        charactorMotion.enabled = false;
        PauseMenuUI.SetActive(true);
        IsPause = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene(LoadShip);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
