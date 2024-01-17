using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeSceenInterac : MonoBehaviour
{
    bool Isplayer = false;
    [SerializeField] private GameObject interactUi;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject Dialog;
    public CharactorMotion charactorMotion;
    public CharactorAimAt aimAt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Isplayer = true;
            interactUi.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Isplayer = false;
            interactUi.SetActive(false);
            Back();
        }
    }
    public void Back()
    {
        Dialog.SetActive(false);
        aimAt.isInteract = false;
        charactorMotion.isInteract = false;
        inGameUI.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Interactive()
    {
        if (Isplayer && Input.GetKeyDown(KeyCode.E) && !aimAt.isInteract && !charactorMotion.isInteract)
        {            
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Dialog.SetActive(true);
                interactUi.SetActive(false);
                inGameUI.SetActive(false);
                aimAt.isInteract = true;
                charactorMotion.isInteract = true;
            
        }
    }

    void Update()
    {
        Interactive();
    }

}
