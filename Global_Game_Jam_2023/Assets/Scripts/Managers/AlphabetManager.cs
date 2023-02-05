using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlphabetManager : MonoBehaviour
{
    [SerializeField] Letter letterPrefab;

    [SerializeField] List<Sprite> spriteLetterList;

    [SerializeField] Transform startPivot;
    [SerializeField] Transform endPivot;

    Stack<Letter> spawnedLetters;
    SpriteRenderer spRenderer;
    private void Start()
    {
        spawnedLetters = new Stack<Letter>();
        spRenderer = GetComponent<SpriteRenderer>();
    }

    internal void SpawnLetter(string vletter)
    {
        if (startPivot.position.x + spawnedLetters.Count > endPivot.position.x) return;

        var spriteLetter = spriteLetterList.Find(x => x.name == vletter);
        var letterSpawned = Instantiate(letterPrefab, new Vector3(startPivot.position.x + spawnedLetters.Count, startPivot.position.y, 0), Quaternion.identity);
        letterSpawned.Initialize(spriteLetter);


        letterSpawned.transform.SetParent(transform);

        letterSpawned.SpriteRenderer.sortingLayerName = spRenderer.sortingLayerName;
        letterSpawned.SpriteRenderer.sortingOrder = spRenderer.sortingOrder;

        spawnedLetters.Push(letterSpawned);
    }

    public void Backspace()
    {
        if (spawnedLetters.Count <= 0) return;

        var letter = spawnedLetters.Pop();
        Destroy(letter.gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(startPivot != null && endPivot != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(startPivot.position, endPivot.position);
            Gizmos.DrawSphere(startPivot.position, 0.1f);
            Gizmos.DrawSphere(endPivot.position, 0.1f);
        }
    }
#endif
}
