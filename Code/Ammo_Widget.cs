using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Widget : MonoBehaviour
{
    public TMPro.TMP_Text AmmoText;

    public void Refresh(int ammoCount)
    {
        AmmoText.text = ammoCount.ToString();
    }
}
