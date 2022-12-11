using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2 gridSize;

    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        this.CreateGrid();
    }

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
