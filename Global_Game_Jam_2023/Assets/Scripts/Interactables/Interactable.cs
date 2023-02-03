using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    [SerializeField] UnityEvent OnInteract;

    public virtual void Interact(Player player)
    {
        Debug.Log("INTERACTED");
        OnInteract.Invoke();
    }
}
