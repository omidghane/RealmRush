using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    [Tooltip("World grid size - should match unityEditor snap settings.")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize {get { return unityGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get{ return grid; } }

    void Awake() {
        creatGrid();
    }

    public Node getNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        return null;
    }
    
    public void blockNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public void resetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int getCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }

    public Vector3 getPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = Mathf.RoundToInt(coordinates.x * unityGridSize);
        position.z = Mathf.RoundToInt(coordinates.y * unityGridSize);
        // position.y = 0;
        
        return position;
    }

    void creatGrid()
    {
        for(int x=0; x < gridSize.x; x++)
        {
            for(int y=0; y<gridSize.y; y++)
            {
                Vector2Int coordinate = new Vector2Int(x,y);
                grid.Add(coordinate, new Node(coordinate, true));
                // Debug.Log(grid[coordinate].coordinates + " - " + grid[coordinate].isWalkable);
            }
        }
    }
}
