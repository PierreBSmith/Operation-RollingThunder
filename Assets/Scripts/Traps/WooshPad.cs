using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooshPad : MonoBehaviour
{
    [SerializeField] private float wooshHeight;
    [SerializeField] private int maxWeight;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.gameObject.GetComponent<PlayerInventory>();
            float jumpHeight;
            if (playerInventory.weight == maxWeight)
                jumpHeight = 0;
            else
                jumpHeight = Mathf.Max(0, wooshHeight / (maxWeight - playerInventory.weight));
            other.rigidbody.AddForce(Vector2.up * jumpHeight);
        }
    }
}
