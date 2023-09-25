using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{ 
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    [SerializeField] float difficultySpeed = 0.5f;
    List<Node> path = new List<Node>();
    int pathSize;
    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;
  
    void OnEnable()
    {   
        speed += difficultySpeed;
        returnToStart();
        recalculatePath(true);
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void returnToStart()
    {
        transform.position = gridManager.getPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    void recalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if(resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else{
            coordinates = gridManager.getCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.getNewPath(coordinates);
        StartCoroutine(followPath());
    }

    void finishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    // void finishPath(bool gameEnd)
    // {
    //     if(!gameEnd){
    //         enemy.StealGold();
    //     }
    //     gameObject.SetActive(false);
    // }

    IEnumerator followPath()
    {
        // Debug.Log("path Length :"+path.Count);
        for(int i = 1; i<path.Count ;i++)
        {
            // Tile wayPoint = path[i];
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.getPositionFromCoordinates(path[i].coordinates);
            
            float travelPercent = 0f;
            
            transform.LookAt( endPosition);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            
        }
        
        finishPath();
    }

}
