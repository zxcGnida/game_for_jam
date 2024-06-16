using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelResource : Resource
{
    public override void Collect()
    {
        Inventory.fuelInHands++;
        base.Collect();
        Debug.Log("Resource was collected" + this.gameObject + Inventory.fuelInHands);
    }
}
