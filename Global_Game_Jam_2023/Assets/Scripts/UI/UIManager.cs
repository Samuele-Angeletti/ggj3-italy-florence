using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    #region SINGLETON
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance != null)
                    return instance;

                GameObject go = new GameObject("UIManager");
                return go.AddComponent<UIManager>();
            }
            else
                return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    [SerializeField] TextMeshProUGUI linkBar;
    [SerializeField] GameObject keyPanel;
    [SerializeField] GameObject keyPrefab;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject creditsPanel;
    private Queue<GameObject> keys;

    private const string linkBase = @"C:\";

    private string currentLink = @"Desktop\";
    Animator Animator;
    private void Awake()
    {
        keys = new Queue<GameObject>();
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentLink = linkBase + currentLink;
        linkBar.text = currentLink;
    }

    public void UpdateKeys(bool add)
    {
        if(add)
        {
            var key = Instantiate(keyPrefab, keyPanel.transform);
            keys.Enqueue(key);

        }
        else
        {
            if(keys.Count > 0)
            {
                var key = keys.Dequeue();
                Destroy(key);
            }
        }
    }

    public void AddText(string folderName)
    {
        if(BackCheck(folderName))
        {
            var stringList = currentLink.Split('\\').Where(x => x != string.Empty).ToList();
            currentLink = string.Empty;
            for (int i = 0; i < stringList.Count; i++)
            {
                if(i < stringList.Count - 1)
                    currentLink += @$"{stringList[i]}\";
            }
        }
        else
            currentLink += $@"{folderName}\";

        linkBar.text = currentLink;
    }

    private bool BackCheck(string folderName)
    {
        if(folderName.ToLower().Contains("back"))
        {
            return true;
        }
        return false;
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void SpawnPlayer()
    {
        GameManager.Instance.SpawnPlayer();

    }

    public void SetTrigger()
    {
        Animator.SetTrigger("Start");
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
