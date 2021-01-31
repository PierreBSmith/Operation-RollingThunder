using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSwitch : MonoBehaviour
{
    [SerializeField] private int weightRequirement;
    [SerializeField] private Toggleable toggleable;

    private void Start()
    {
        if (toggleable == null)
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
                StartCoroutine(toggleable.Activate());
                Collider2D box = gameObject.GetComponent<Collider2D>();
                box.enabled = false;
            }
        }
    }
}
