using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 60;
    [SerializeField] float buildTime = 1f;
    Bank bank;
    
    void Start()
    {
        StartCoroutine(build());   
    }

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

    IEnumerator build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildTime);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }

    }
}
