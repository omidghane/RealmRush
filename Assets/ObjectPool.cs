using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float waitInstantiateEnemy = 1f;
    [SerializeField] int difficultyPoolSize = 1;
    int numberOfActivatingEnemy = 0;

    GameObject[] pool;
    
    void Awake()
    {
        populatePool(poolSize);
    }

    void Start()
    {
        StartCoroutine(instantiatingObject());
    }

    private void populatePool(int size)
    {
        GameObject[] newPool = new GameObject[size];

        for (int i = 0; i < newPool.Length; i++)
        {
            newPool[i] = Instantiate(enemyPrefab, transform);
            newPool[i].SetActive(false);
        }
        if(pool==null){
            pool = new GameObject[size];
            pool = newPool;
        }
        else
        {
            int j=0;
            while (pool[j] != null)
            {
                j++;
            }
            pool[j] = newPool[0]; 
        }
    }

     void EnableObjectInPool()
    {
        // if(numberOfActivatingEnemy >= 10){
        //     poolSize++;
        //     populatePool(1);
        //     numberOfActivatingEnemy = 0;
        //     Debug.Log(poolSize + " test");
        // }
        for(int i = 0; i<poolSize; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                numberOfActivatingEnemy ++;
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
