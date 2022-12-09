using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private float spawnTimer = 1f;

    private GameObject[] pool;

    void Awake() 
    {
        this.PopulatePool();
    }

    void Start()
    {
        StartCoroutine(this.SpawnEnemy());
    }

    private void PopulatePool()
    {
        //Initialize a array with the given size
        this.pool = new GameObject[this.poolSize];

        for(int count = 0;count < this.pool.Length;count++)
        {
            //Creates a new enemy in the starter position, and set is parent
            //to be the ObjectPool GameObject
            this.pool[count] = Instantiate(enemyPrefab, transform);
            //Deactivate it so it is stored in the pool to be used later, and
            //not appear imediality
            this.pool[count].SetActive(false);
        }
    }

    private void EnableObjectInPool()
    {
        foreach (GameObject enemy in this.pool)
        {
            //If the enemy is not active(dead) respawn it
            //so it can be battled again
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                //Do it to only one enemy per time, so the spawner
                //spawn it again when it reaches the right time
                return;
            }
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            //"Respawn" enemies again when they are destroyed
            this.EnableObjectInPool();
            yield return new WaitForSeconds(this.spawnTimer);
        }
    }    
}
