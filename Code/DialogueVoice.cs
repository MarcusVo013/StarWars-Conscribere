using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueVoice : MonoBehaviour
{
    public AudioSource voiceAudioSource;
    public AudioClip[] dialogueVoiceClips;

    private void Start()
    {
        //voiceAudioSource = GetComponent<AudioSource>();
    }

    public void PlayVoiceClip(int index)
    {
        if (dialogueVoiceClips.Length > index)
        {
            voiceAudioSource.clip = dialogueVoiceClips[index];
            voiceAudioSource.Play();
        }
    }
}