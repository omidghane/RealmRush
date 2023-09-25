using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();   
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
 
    // [SerializeField] Node currentNode;
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid;

            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }
    }

    void Start()
    {
        getNewPath();
    }

    public List<Node> getNewPath()
    {
        return getNewPath(startCoordinates);
    }

    public List<Node> getNewPath(Vector2Int currCoordinates)
    {
        gridManager.resetNodes();
        BreathFirstSearch(currCoordinates);
        return buildPath();
    }

    void exploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoord = currentSearchNode.coordinates + direction;
            if(grid.ContainsKey(neighborCoord))
            {
                neighbors.Add(grid[neighborCoord]);
            }
        }

        foreach(Node neighbor in neighbors)
        {
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreathFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true; 

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            exploreNeighbors();
            if(currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> buildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool willBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = getNewPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                getNewPath();
                return true;
            }
        }

        return false;
    }

    public void notifyReceivers()
    {
        BroadcastMessage("recalculatePath", false , SendMessageOptions.DontRequireReceiver);
    }
}
