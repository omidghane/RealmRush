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
    public int CurrentBalance {get { return currentBalance; }}

    void Awake()
    {
        currentBalance = firstBalance;  
        display();  
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
        Debug.Log(index + " wwwwwww");
        SceneManager.LoadScene(index);
    }
}
