using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Fire : MonoBehaviour
{
    [SerializeField]
    public UnityEvent onFireExtinguished;

    [HideInInspector]
    public bool isExtinguished = false;
}
