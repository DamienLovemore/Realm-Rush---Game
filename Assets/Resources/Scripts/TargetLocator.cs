using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform towerWeapon;
    private Transform shootTarget;
    
    //Finds the enemy to be shooting at
    void Start()
    {
        shootTarget = FindObjectOfType<EnemyMover>().transform;
    }
    
    void Update()
    {
        this.AimWeapon();
    }

    //Rotates the weapon of this tower to be facing the enemy
    private void AimWeapon()
    {
        this.towerWeapon.LookAt(shootTarget);
    }
}
