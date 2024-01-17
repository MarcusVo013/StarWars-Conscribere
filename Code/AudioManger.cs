
using UnityEngine;
public class AudioManger : MonoBehaviour
{
    [Header("-------Audio Source------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------Audio Clip------")]
    public AudioClip background;
    public AudioClip blaster;
    public AudioClip miniBlaster;
    public AudioClip bigBlaster;
    public AudioClip cloneHurt;
    public AudioClip zombieHurt;
    public AudioClip droidHurt;
    public AudioClip winRound;
    public AudioClip gameOver;
    public AudioClip zombieAtk;
    public AudioClip droidAtk;
    public AudioClip health;
    private void Awake()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
