using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 60;
    Bank bank;
    public bool create_tower(Tower tower, Vector3 position)
    {
        bank = FindObjectOfType<Bank>();

        if(bank == null)
        {
            return false;
        }

        if(bank.CurrentBalance >= cost)
        {
            bank.withdraw(cost);
            Instantiate(tower , position, Quaternion.identity);
            return true;
        }

        return false;
    }
}
