using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent onValueChanged = new UnityEvent();

    public static UnityEvent onEnemyDied = new UnityEvent();
}
