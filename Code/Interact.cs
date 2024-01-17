using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.Rendering;

public class Interact : MonoBehaviour
{
  
    bool Isplayer = false;
    int index = 0;
    public TextMeshProUGUI textMesh;
    public float wordSpeed;
    public string[] dialogue;
    public GameObject contButton;
    [SerializeField] private GameObject interactUi;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject Dialog;
    public CharactorMotion charactorMotion;
    public CharactorAimAt aimAt;
    public DialogueVoice dialogueVoice;

    public void Awake()
    {
        dialogueVoice = GetComponent<DialogueVoice>();
        textMesh.text = "";
    }
    private void Update()
    {
        Interactive();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
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
            NoText();
        }
    }
    private void Interactive()
    {
        if (Isplayer && Input.GetKeyDown(KeyCode.E) && !aimAt.isInteract && !charactorMotion.isInteract )
        {
            if (Dialog.activeInHierarchy) 
            {
                NoText();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Dialog.SetActive(true);
                StartCoroutine(Typing());
                interactUi.SetActive(false);
                inGameUI.SetActive(false);
                aimAt.isInteract = true;
                charactorMotion.isInteract = true;
            }
        }
        if (textMesh.text == dialogue[index] || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            contButton.SetActive(true);
        }
    }
    public void NoText()
    {
        textMesh.text = "";
        index = 0;
        Dialog.SetActive(false);
        aimAt.isInteract = false;
        charactorMotion.isInteract = false;
        inGameUI.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {                    
             textMesh.text += letter;
             yield return new WaitForSeconds(wordSpeed);            
        }
        dialogueVoice.PlayVoiceClip(index);
    }
    public void NextLine()
    {
        contButton.SetActive(false);
        if (index < dialogue.Length - 1 )
        {
            index++;
            textMesh.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            NoText();
        }
    }

}
