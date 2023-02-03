using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    [SerializeField] float timeDisable;
    BoxCollider2D boxCollider;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    internal void FastDisable()
    {
        StartCoroutine(DisableCoroutine());
    }

    private IEnumerator DisableCoroutine()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(timeDisable);
        boxCollider.enabled = true;
    }
}
