using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyController : MonoBehaviour
{
    public delegate void ObjectDestroyed();
    public static event ObjectDestroyed onDestroy;

    private void OnDestroy()
    {
        onDestroy?.Invoke();
    }
}
