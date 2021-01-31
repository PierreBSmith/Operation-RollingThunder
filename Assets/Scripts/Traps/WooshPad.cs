using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooshPad : MonoBehaviour
{
    [SerializeField] private float wooshHeight;
    [SerializeField] private int maxWeight;
    public AudioSource wooshSound;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Detected player");
            PlayerInventory playerInventory = other.gameObject.GetComponent<PlayerInventory>();
            float jumpHeight;
            if (playerInventory.weight >= maxWeight)
                jumpHeight = 0;
            else{
                jumpHeight = Mathf.Max(0, wooshHeight / (playerInventory.weight / 2f));
                wooshSound.Play();
            }
            Debug.Log("Player Weight: " + playerInventory.weight);
            Debug.Log("Player should launch: " + jumpHeight);
            other.rigidbody.AddForce(Vector2.up * jumpHeight);
        }
    }
}
