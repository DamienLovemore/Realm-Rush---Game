using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int destinatedCoordinates;
    
    //The initial tile
    private Node startNode;
    //The tile that it should reach
    private Node destinationNode;
    //The node(tile) we currently are in the search
    private Node currentSearchNode;

    private Queue<Node> frontier = new Queue<Node>();
    //Hols all the nodes that has been reached
    private Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    //The four directions for possible neighbors
    private Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down}; 
    //The class that manage the grids(tiles)  
    private GridManager gridManager;
    private Dictionary<Vector2Int, Node> grid;

    void Awake() 
    {
        this.gridManager = FindObjectOfType<GridManager>();
        //If it finds the grid manager, gets the dictionary
        //that holds the info for all the nodes (tiles)
        if(this.gridManager != null)
        {
            this.grid = this.gridManager.Grid;
        }

        this.startNode = new Node(startCoordinates, true);
        this.destinationNode = new Node(destinatedCoordinates, true);
    }

    void Start()
    {
        this.BreadthFirstSearch();
    }

    private void ExploreNeighbors()
    {
        //Stores all the neightbors of the current node
        List<Node> neighbors = new List<Node>();

        //Look through all the four direction for
        //possible neightbors for this node
        foreach (Vector2Int searchDirection in this.directions)
        {
            //Calculate what the neightbor coordinates for
            //the current node should be
            Vector2Int neighborCoordinates = this.currentSearchNode.coordinates + searchDirection;
            
            //Verifies if this node(tile) exists, if it
            //exists then add it to the neighbors
            Node nextNode = this.gridManager.GetNode(neighborCoordinates);
            if(nextNode != null)
            {
                neighbors.Add(nextNode);
            }  
        }

        //After all the neighbors are found, we should add the neighbors
        //that are walkable and have yet not been explored
        foreach (Node neighbor in neighbors)
        {
            if((!this.reached.ContainsKey(neighbor.coordinates)) && (neighbor.isWalkable))
            {
                frontier.Enqueue(neighbor);
                reached.Add(neighbor.coordinates, neighbor);                
            }
        }
    }

    private void BreadthFirstSearch()
    {
        //Tells that it is trying to find the path
        bool isRunning = true;

        //Begin exploring the initial node(tile)
        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);


        while(frontier.Count > 0 && isRunning)
        {
            //Gets the current tile that it is
            this.currentSearchNode = frontier.Dequeue();
            //Mark that it has been explored
            this.currentSearchNode.isExplored = true;
            //Discover its valid neighbors and add to the queue
            //to search later            
            this.ExploreNeighbors();

            //If it have already reached the end of the path,
            //then it should stop running
            if(this.currentSearchNode.coordinates == this.destinatedCoordinates)
            {
                isRunning = false;
            }
        }
    }
}
