using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerDefense;
    [SerializeField] bool isPlacable;
    public bool IsPlacable { get { return isPlacable; } }

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start() {
        if(gridManager != null)
        {
            coordinates = gridManager.getCoordinatesFromPosition(transform.position);

            if(!isPlacable)
            {
                gridManager.blockNode(coordinates);
            }
        }    
    }

    void OnMouseDown()
    {
        if(gridManager.getNode(coordinates).isWalkable && !pathFinder.willBlockPath(coordinates))
        {
            bool isSuccessful = towerDefense.create_tower(towerDefense, transform.position);
            if(isSuccessful)
            {
                gridManager.blockNode(coordinates);
                pathFinder.notifyReceivers();
            }
        }    
    }
}
