using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int firstBalance = 150;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI displayBalance;
    [SerializeField] TextMeshProUGUI showWin;
    [SerializeField] GameObject ImagesParent;
    public int enemyDiedNumber;
    public int CurrentBalance {get { return currentBalance; }}
    bool isScenePaused = false;
    bool isEndHappen =false;
    float currTime;
    float previousTimeScale;

    void Awake()
    {
        enemyDiedNumber = 0;
        currentBalance = firstBalance;  
        display();  
        // showWin = GetComponent<Canvas>();
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
    }

    public void deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        display();
    }

    public void withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        display();

        if(currentBalance < 0)
        {
            reloadScene();
        }
    }

    void display()
    {
        displayBalance.text = "Gold: " + CurrentBalance;
    }

    void reloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int index = currentScene.buildIndex;
        SceneManager.LoadScene(index);
    }

    public void win()
    {
        showWin.text = "win";
        ImagesParent.SetActive(true);
        // Rawimage im = 
        // BroadcastMessage("finishPath",SendMessageOptions.DontRequireReceiver);
        isScenePaused = true;
        // previousTimeScale = Time.timeScale;
        // Time.timeScale = 0f;
        // reloadScene();
    }

    void Update() {
        if(isScenePaused && !isEndHappen)
        {
            currTime = Time.time;
            isScenePaused = false;
            isEndHappen = true;
        }
        if(Time.time > currTime+5 && isEndHappen)
        {
            reloadScene();
        }
    }

}
