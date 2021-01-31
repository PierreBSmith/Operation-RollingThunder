using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureToggle : MonoBehaviour
{
    [SerializeField] private int weightRequirement;
    [SerializeField] private Toggleable toggleable;

    private void Awake()
    {
        if (!toggleable)
        {
            enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerInventory playerInventory;
        if (other.gameObject.CompareTag("Player") && (playerInventory = other.gameObject.GetComponent<PlayerInventory>()))
        {
            if (playerInventory.weight >= weightRequirement)
            {
                StartCoroutine(toggleable.Activate());
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        PlayerInventory playerInventory;
        if (other.gameObject.CompareTag("Player") && (playerInventory = other.gameObject.GetComponent<PlayerInventory>()))
        {
            if (playerInventory.weight >= weightRequirement)
            {
                StartCoroutine(toggleable.Deactivate());
            }
        }
    }
}
