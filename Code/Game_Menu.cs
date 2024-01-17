using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Menu : MonoBehaviour
{
    [Header("Load_Game Menu")]
    public string NewGame_Level;
    private string LevelToLoad;
    [SerializeField] private GameObject No_SaveToLoad = null;

    public void NewGame_Dialog_Yes()
    {

    }
    public void LoadGame_Dialog_Yes() 
    {
        if(PlayerPrefs.HasKey("LoadSave"))
        {
            LevelToLoad = PlayerPrefs.GetString("LoadSave");
        }
        else
        {
            No_SaveToLoad.SetActive(true);
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
