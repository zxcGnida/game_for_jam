using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class MetalResource : Resource
{
    public override void Collect()
    {
        Inventory.metalInHands += 5;
        base.Collect();
        Debug.Log("Resource was collected" + this.gameObject + Inventory.metalInHands);
    }
}
