using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnAnimationEvent : MonoBehaviour
{
    public UnityEvent<string> OnAnimation;

    public void OnEvent(string name)
    {
        OnAnimation?.Invoke(name);
    }
}