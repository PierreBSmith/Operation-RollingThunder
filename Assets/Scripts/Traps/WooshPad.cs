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
            PlayerManger playerManger = other.gameObject.GetComponent<PlayerManger>();
            float jumpHeight;
            if (playerManger.weight == maxWeight)
                jumpHeight = 0;
            else
                jumpHeight = Mathf.Max(0, wooshHeight / (maxWeight - playerManger.weight));
            other.rigidbody.AddForce(Vector2.up * jumpHeight);
        }
    }
}
