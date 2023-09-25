using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMoverFix : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    int pathSize;
    Enemy enemy;
  
    void OnEnable()
    {
        findPath();
        returnToStart();
        StartCoroutine(followPath());
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void returnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void findPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        Transform[] parentArray = parent.GetComponentsInChildren<Transform>();
        pathSize = parentArray.Length/3;

        foreach(Transform child in parent.transform)
        {
            Tile wayPoint = child.GetComponent<Tile>();
            if(wayPoint != null)
            {
                path.Add(wayPoint);
            }
            
        }
    }

    void finisPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator followPath()
    {
        // Debug.Log("path Length :"+path.Count);
        for(int i = 0; i<path.Count ;i++)
        {
            Tile wayPoint = path[i];
            Vector3 startPosition = transform.position;
            Vector3 endPosition = wayPoint.transform.position;
            float travelPercent = 0f;
            
            transform.LookAt( endPosition);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            
        }
        
        finisPath();
    }

}