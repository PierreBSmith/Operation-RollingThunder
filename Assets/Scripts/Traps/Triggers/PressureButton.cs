using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PressureButton : MonoBehaviour
{
    [SerializeField] private int weightRequirement;
    [SerializeField] private Toggleable[] toggleables;
    [SerializeField] private float gracePeriod;

    private bool _pressedByButton;
    private bool _pressedByPlayer;
    private float _grace;
    private void Start()
    {
        if (toggleables.Length == 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_pressedByButton)
        {
            PlayerInventory playerInventory;
            if (other.gameObject.CompareTag("Player") &&
                (playerInventory = other.gameObject.GetComponent<PlayerInventory>()))
            {
                if (playerInventory.weight >= weightRequirement && _grace <= 0)
                {
                    _grace = gracePeriod;
                    _pressedByPlayer = true;
                    foreach (Toggleable toggle in toggleables)
                    {
                        if (toggle != null)
                            StartCoroutine(toggle.Activate());
                    }
                }
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        PlayerInventory playerInventory;
        if (other.gameObject.CompareTag("Player") && (playerInventory = other.gameObject.GetComponent<PlayerInventory>()))
        {
            if (playerInventory.weight >= weightRequirement && _grace <= 0)
            {
                foreach (Toggleable toggle in toggleables)
                {
                    if (toggle != null)
                        StartCoroutine(toggle.Deactivate());
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            _pressedByButton = true;
            if (!_pressedByPlayer)
            {
                foreach (Toggleable toggle in toggleables)
                {
                    if (toggle != null)
                        StartCoroutine(toggle.Activate());
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _pressedByButton = false;
        if (other.gameObject.CompareTag("Box"))
        {
            if(!_pressedByPlayer)
            {
                foreach (Toggleable toggle in toggleables)
                {
                    if (toggle != null)
                        StartCoroutine(toggle.Deactivate());
                }
            }
        }
    }

    private void Update()
    {
        _grace -= Time.deltaTime;
    }
}
