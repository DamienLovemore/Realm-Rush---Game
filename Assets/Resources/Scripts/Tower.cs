using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost = 75;

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
}
