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
        PlayerManger playerManger;
        if (other.gameObject.CompareTag("Player") && (playerManger = other.gameObject.GetComponent<PlayerManger>()))
        {
            if (playerManger.weight >= weightRequirement)
            {
                StartCoroutine(toggleable.Activate());
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        PlayerManger playerManger;
        if (other.gameObject.CompareTag("Player") && (playerManger = other.gameObject.GetComponent<PlayerManger>()))
        {
            if (playerManger.weight >= weightRequirement)
            {
                StartCoroutine(toggleable.Deactivate());
            }
        }
    }
}
