using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    [SerializeField] private int weightRequirement;
    [SerializeField] private Toggleable[] toggleables;
    [SerializeField] private float gracePeriod;

    private void Start()
    {
        if (toggleables.Length == 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerInventory playerInventory;
        if (other.gameObject.CompareTag("Player") && (playerInventory = other.gameObject.GetComponent<PlayerInventory>()))
        {
            if (playerInventory.weight >= weightRequirement)
            {
                foreach (Toggleable toggle in toggleables)
                {
                    if (toggle != null)
                        StartCoroutine(toggle.Activate());
                }
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
                foreach (Toggleable toggle in toggleables)
                {
                    if (toggle != null)
                        StartCoroutine(toggle.Deactivate());
                }
            }
        }
    }
}
