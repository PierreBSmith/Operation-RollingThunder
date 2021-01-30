using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooshPad : MonoBehaviour
{
    [SerializeField] private float wooshHeight;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Woosh lol
        }
    }
}
