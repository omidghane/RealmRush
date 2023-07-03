using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new List<WayPoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    [SerializeField] float difficultySpeed = 0.5f;
    int pathSize;
    Enemy enemy;
  
    void OnEnable()
    {   
        speed += difficultySpeed;
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
        
        List<WayPoint> addedPath;
        foreach(Transform child in parent.transform)
        {
            WayPoint wayPoint = child.GetComponent<WayPoint>();
            if(wayPoint != null)
            {

                path.Add(wayPoint);
            }
            
        }

        // foreach(Transform child in parent.transform)
        // {

        //     WayPoint wayPoint = child.GetComponent<WayPoint>();
        //     if(wayPoint != null)
        //     {
        //         Vector3 currPosition = wayPoint.transform.position;
        //         foreach(Transform nextChild in parent.transform)
        //         {
        //             if (Mathf.Abs(nextChild.transform.position.x - currPosition.x)==1)
        //             {
        //                 addedPath.Add(nextChild);
        //                 path.Add(wayPoint);
        //                 break;
        //             }
        //             else if(Mathf.Abs(nextChild.transform.position.y - currPosition.y)==1)
        //             {
        //                 path.Add(wayPoint);
        //                 break;
        //             }
        //         }
        //     }
        // }

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
            WayPoint wayPoint = path[i];
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
