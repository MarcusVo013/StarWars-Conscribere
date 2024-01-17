using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionTimer : MonoBehaviour
{
    bool ischeat = false;
    [SerializeField] float MissionTime;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject victoryCanvas;
    [Header("___Audio___")]
    [SerializeField] AudioSource Music;
    [SerializeField] AudioSource soundAndVoice;
    [SerializeField] AudioClip startTimer;
    [SerializeField] AudioClip endTimer;
    [Header("___TrigerA___")]
    [SerializeField] float triggerTimeA;
    [SerializeField] GameObject triggerEnemyZoneA;
    [SerializeField] GameObject triggerHealA;
    [Header("___TrigerB___")]
    [SerializeField] float triggerTimeB;
    [SerializeField] GameObject triggerEnemyZoneB;
    [SerializeField] GameObject triggerWeaponB;
    [SerializeField] GameObject triggerHealB;
    [Header("___TrigerC___")]
    [SerializeField] float triggerTimeC;
    [SerializeField] GameObject triggerEnemyZoneC1;
    [SerializeField] GameObject triggerEnemyZoneC2;
    [SerializeField] GameObject triggerWeaponC;
    [SerializeField] GameObject triggerHealC;
    private void Awake()
    {
        soundAndVoice.PlayOneShot(startTimer);
    }
    private void Update()
    {
        if (MissionTime >= 0 )
        {
         MissionTime -= Time.deltaTime;
        }
        TimeDisplay();
        ZoneA();
        ZoneB();
        ZoneC();
    }
    private void TimeDisplay()
    {
        float timeLeft = MissionTime;
        if (timeLeft > 0)
        {
            string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
            string seconds = Mathf.Floor(timeLeft % 60).ToString("00");
            timerText.text = $"{minutes}:{seconds}";
        }
        if(timeLeft <= 0)
        {
            Victory();
            MissionTime = 0;
            timerText.text = "00:00";
        }
    }
    private void ZoneA()
    {
        if(MissionTime <= triggerTimeA)
        {
            triggerEnemyZoneA.SetActive(true);
            triggerHealA.SetActive(true);
        }
    }
    private void ZoneB()
    {
        if (MissionTime <= triggerTimeB)
        {
            triggerEnemyZoneB.SetActive(true);
            triggerHealB.SetActive(true);
            triggerWeaponB.SetActive(true);
        }
    }
    private void ZoneC()
    {
        if (MissionTime <= triggerTimeC)
        {
            triggerEnemyZoneC1.SetActive(true);
            triggerEnemyZoneC2.SetActive(true);
            triggerHealC.SetActive(true);
            triggerWeaponC.SetActive(true);
        }
    }
    private void Victory()
    {       
            soundAndVoice.PlayOneShot(endTimer);
        //triggerEnemyZoneA.SetActive(false);
        //triggerEnemyZoneB.SetActive(false);
        //triggerEnemyZoneC1.SetActive(false);
        //triggerEnemyZoneC2.SetActive(false);
            victoryCanvas.SetActive(true);
            Invoke("SetTime", 1.5f);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;       
    }
    private void SetTime()
    {
        Time.timeScale = 0f;
    }
    public void Cheat()
    {
        ischeat = true;
        if (ischeat)
        {
            MissionTime = 1;
        }
    }
}
