using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform towerWeapon;
    [SerializeField] private ParticleSystem projectileParticles;
    [SerializeField] private float towerRange = 15;

    private Transform shootTarget;
    
    //Find and aim the closest enemy to shoot at    
    void Update()
    {
        this.FindClosestTarget();
        this.AimWeapon();
    }

    //Finds the enemy to be shooting at
    private void FindClosestTarget()
    {
        //Gets all the enemies in the scene
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        //Store the target that it will shoot
        Transform closestTarget = null;
        //The maximum distance that it will look
        //for the closest enemy
        float maxDistance = Mathf.Infinity;
        //Loop through all the enemies to find the closest
        foreach (Enemy enemy in enemies)
        {
            //Calculates the distance between the tower and this enemy
            float targetDistance = Vector3.Distance(this.transform.position, enemy.transform.position);

            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        //Updates the target that the tower should shoot at
        this.shootTarget = closestTarget;
    }

    //Rotates the weapon of this tower to be facing the enemy
    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(this.transform.position, this.shootTarget.position);

        this.towerWeapon.LookAt(shootTarget);

        if (targetDistance <= this.towerRange)
        {
            this.Attack(true);
        }
        else
        {
            this.Attack(false);
        }
    }

    //Toggles the shooting of the tower weapon
    private void Attack(bool isActive)
    {
        var emissionModule = this.projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
