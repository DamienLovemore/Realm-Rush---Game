using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows to be viwed by the unity editor
[System.Serializable]
//Pure C# class (not derived from anything)
public class Node
{
    //The coordinates of this node (tile)
    public Vector2Int coordinates;
    //If the enemies can walk by this node
    public bool isWalkable;
    //If this node has already been walked
    public bool isExplored;
    //If it is part of the path, and just grass tiles
    public bool isPath;
    //The node that this node is connected to
    public Node connectedTo;

    //The construtor of the class
    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
