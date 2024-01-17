using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AnimationEvent : UnityEvent<string>
{

}
public class Weapon_AnimationEvent : MonoBehaviour
{
    public AnimationEvent Weapon_animationEvent = new AnimationEvent();
    public void OnAnimayionEvent(string EventName)
    {
        Weapon_animationEvent.Invoke(EventName);
    }
}
