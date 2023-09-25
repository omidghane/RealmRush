using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    public void SetGoldReward(int value) { goldReward+=value; }
    public int GoldReward{ get{ return goldReward; } }

    [SerializeField] int goldPenality = 25;
    [SerializeField] int enemyDieToEnd = 20;
    Bank bank;
    bool gameIsFinished = false;

    void Start() 
    {
        bank = FindObjectOfType<Bank>();    
    }

    public void RewardGold()
    {
        if(bank == null) { return; }
        bank.deposit(goldReward);
    }

    public void StealGold()
    {
        if(bank == null) { return; }
        bank.withdraw(goldPenality);
    }

    public void checkWin()
    {
        bank.enemyDiedNumber += 1;
        Debug.Log(bank.enemyDiedNumber+ " dies number");
        if(bank.enemyDiedNumber > enemyDieToEnd)
        {
            bank.win();
        }
    }

}
