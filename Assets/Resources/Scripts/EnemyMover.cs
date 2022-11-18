using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> path = new List<Waypoint>();
    
    void Start()
    {
        this.PrintWaypointName();
    }

    private void PrintWaypointName()
    {
        foreach (Waypoint waypoint in path)
        {
            Debug.Log(waypoint.name);
        }
    }
}
