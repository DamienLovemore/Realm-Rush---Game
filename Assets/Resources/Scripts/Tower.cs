using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float buildDelay = 1f;
    [SerializeField] private int cost = 75;

    void Start() 
    {
        StartCoroutine(this.Build());
    }

    //Creates a tower in a tile
    public bool CreateTower(Tower tower, Vector3 position)
    {
        //Finds the bank system to see if there is money
        Bank bankSystem = FindObjectOfType<Bank>();

        //If the bank system is not found then do nothing
        if (bankSystem == null)
            return false;

        //Only lets it create the tower if it has enough
        //balance
        int currentBalance = bankSystem.GetCurrentBalance();
        if (currentBalance >= this.cost)
        {
            bankSystem.Withdraw(this.cost);
            Instantiate(tower.gameObject, position, Quaternion.identity);
            return true;
        }
        else
        {
            return false;
        }
    }

    //Is called when the tower instantiates(creates) in its
    //start so that its parts become visible over time, and
    //not suddenly appear.
    private IEnumerator Build()
    {
        //Gets all the children and grand-children of this tower
        //because all of them have a Transform component
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandchild in child)
            {
                child.gameObject.SetActive(false);
            }
        }

        //Enable the parts of the tower if a delay, showing
        //that the tower is being built and not just appearing
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(this.buildDelay);
            foreach (Transform grandchild in child)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
