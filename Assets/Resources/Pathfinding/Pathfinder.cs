using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get{return this.startCoordinates;}}

    [SerializeField] private Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get{return this.destinationCoordinates;}}
    
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

            //Gets the start and end node(tiles) from the grid manager
            //(it already generated all the nodes for the coordinate)
            this.startNode = this.gridManager.Grid[this.startCoordinates];
            this.destinationNode = this.gridManager.Grid[this.destinationCoordinates];
        }
    }

    void Start()
    {
        this.GetNewPath();
    }

    //Buils a new path to be used by the enemies to get to the
    //destination. Considering that it must find the shortest
    //path, and avoid blocked Nodes(tiles).
    public List<Node> GetNewPath()
    {
        //Resets the information marked on the nodes about
        //which path the enemies should use
        this.gridManager.ResetNodes();

        //Generates the search tree for all the tiles
        this.BreadthFirstSearch();
        //Build the path that the enemy should follow
        return this.BuildPath();
    }

    //Gets all the neighbors of the current node to explore
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
                //Mark the original node that this neighbor is
                //connected to
                neighbor.connectedTo = currentSearchNode;
                frontier.Enqueue(neighbor);
                reached.Add(neighbor.coordinates, neighbor);                
            }
        }
    }

    //Builds the tree that has all the conections of the tiles,
    //and the paths
    private void BreadthFirstSearch()
    {
        //Makes the start and end nodes to be available for
        //the path, but not for placing towers
        this.startNode.isWalkable = true;
        this.destinationNode.isWalkable = true;

        //Clears the previous found path to create the new one
        this.frontier.Clear();
        this.reached.Clear();

        //Tells that it is trying to find the path
        bool isRunning = true;

        //Begin exploring the initial node(tile)
        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while((frontier.Count > 0) && (isRunning))
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
            if(this.currentSearchNode.coordinates == this.destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    //Gets the info from the tree and with that build the path
    //that the enemy should follow
    private List<Node> BuildPath()
    {
        //Stores the tiles that should be followed to reach
        //the destiny
        List<Node> path = new List<Node>();
        //Starts by the end, because BreadthFirstSearch finds
        //the path in reverse
        Node currentNode = this.destinationNode;

        //Add the node(tile) that was added as a valid path
        path.Add(currentNode);
        currentNode.isPath = true;

        //Go from the end node to the beginning in reverse order
        //to get the shortest and ideal path
        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;

            path.Add(currentNode);
            currentNode.isPath = true;
        }

        //Reserve the order of the list, so the path is in
        //the right direction (beginning to end)
        path.Reverse();

        return path;
    }

    //Verifies if the new path generated was not able to find
    //the path to the destination.
    //True means it was not able to find.
    public bool WillBlockPath(Vector2Int coordinates)
    {
        Node pathNode = this.gridManager.GetNode(coordinates);

        if (pathNode != null)
        {
            bool previousState = pathNode.isWalkable;

            //Set this Node(tile) as blocked to verify if this
            //does not result in blocking the enemies path
            pathNode.isWalkable = false;
            List<Node> newPath = this.GetNewPath();
            pathNode.isWalkable = previousState;

            //If the new path was not able to advance more
            //then 1 Node(tile) means that the path is blocked
            if(newPath.Count <= 1)
            {
                //Generates a new path for the enemies to use
                this.GetNewPath();
                return true;
            }
        }

        return false;
    }

    //Notify about the change in the path to all scripts
    public void NotifyReceivers()
    {
        //Function that is called by BroadcastMessage so that every script
        //that has this function receives the update in the path (DontRequireReceiver
        //makes it be able to send messages, even if no one is receiving)
        BroadcastMessage("RecalculatePath", SendMessageOptions.DontRequireReceiver);
    }
}
