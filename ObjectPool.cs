using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float waitInstantiateEnemy = 1f;

    GameObject[] pool;
    
    void Awake()
    {
        populatePool();
    }
    
    void Start()
    {
        StartCoroutine(instantiatingObject());
    }

    private void populatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

     void EnableObjectInPool()
    {
        for(int i = 0; i<pool.Length; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator instantiatingObject()
    {
        while(true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(waitInstantiateEnemy);
        }
    }

}
