using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2 gridSize;
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize{ get{return this.unityGridSize;} }

    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get{ return this.grid;}}

    void Awake()
    {
        this.CreateGrid();
    }

    //Get a Node(tile) corresponding to a specific coordinate
    //if that exists. null otherwise
    public Node GetNode(Vector2Int coordinates)
    {
        //Verify if a node if this coordinates exists
        //(Key exists in the dictionary)
        if(this.grid.ContainsKey(coordinates))
        {
            return this.grid[coordinates];
        }

        return null;        
    }

    //If a Node if the coordinates passed exists them block
    //that Node from being walkable.
    public void BlockNode(Vector2Int coordinates)
    {
        if(this.grid.ContainsKey(coordinates))
        {
            this.grid[coordinates].isWalkable = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();

        //Calculates the coordinate for this tile (Divides by the snap move value,
        //so it display 0,1 instead of 0, 10 with 10 in the move for snap settings)
        coordinates.x = Mathf.RoundToInt(position.x / this.unityGridSize);
        //Uses z instead of y, because y is up and down not front or back
        coordinates.y = Mathf.RoundToInt(position.z / this.unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();

        // To convert back to position from coordinates if to calculate the
        //coordinates we divide by unityGridSize, to get the original value
        //we must multiply by unityGridSize.
        position.x = coordinates.x * this.unityGridSize;
        position.z = coordinates.y * this.unityGridSize;

        return position;
    }

    //Create a Node for every coordinate available in the game
    private void CreateGrid()
    {
        //Go through all the tiles of the game
        for(int x = 0; x < this.gridSize.x;x++)
        {
            for(int y = 0; y < this.gridSize.y;y++)
            {
                //Adds all the tiles to the dictionary
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}
