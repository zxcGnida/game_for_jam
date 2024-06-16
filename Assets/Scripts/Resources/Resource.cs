using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public abstract class Resource : MonoBehaviour
{
    

    public virtual void Collect()
    {
        Destroy(gameObject);
        EventManager.onValueChanged.Invoke();
    }
}
