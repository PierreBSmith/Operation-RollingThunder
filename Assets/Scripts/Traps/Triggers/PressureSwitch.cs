using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSwitch : MonoBehaviour
{
    [SerializeField] private int weightRequirement;
    [SerializeField] private Toggleable[] toggleables;

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
                Collider2D buttonBox = gameObject.GetComponent<Collider2D>();
                buttonBox.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            foreach (Toggleable toggle in toggleables)
            {
                if (toggle != null)
                    StartCoroutine(toggle.Activate());
            }
            Collider2D buttonBox = gameObject.GetComponent<Collider2D>();
            buttonBox.enabled = false;
        }
    }
}
