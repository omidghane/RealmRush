using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenality = 25;
    Bank bank;

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
}
