using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int weight;

    [SerializeField] private BodyCollection baseBodyCollection;
    [SerializeField] private int baseWeight;

    private GameObject _nearestBodyPart;
    [HideInInspector] public BodyCollection bodyCollection;

    
    
    
    private void Awake()
    {
        bodyCollection = Instantiate(baseBodyCollection);
        RecalcWeight();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BodyPart"))
        {
            _nearestBodyPart = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BodyPart"))
        {
            _nearestBodyPart = null;
        }
    }


    private void RecalcWeight()
    {
        weight = baseWeight;
        weight += bodyCollection.head != null ? bodyCollection.head.weight : 0;
        weight += bodyCollection.arms != null ? bodyCollection.arms.weight : 0;
        weight += bodyCollection.legs != null ? bodyCollection.legs.weight : 0;
        weight += bodyCollection.torso != null ? bodyCollection.torso.weight : 0;
    }

    private void AddPart()
    {
        BodyPart newestPart = _nearestBodyPart.GetComponent<PartPickup>().bodyPart;
        switch (newestPart.partType)
        {
            case BodyPart.PartType.Head:
                bodyCollection.head = newestPart;
                break;
            case BodyPart.PartType.Arms:
                bodyCollection.arms = newestPart;
                break;
            case BodyPart.PartType.Legs:
                bodyCollection.legs = newestPart;
                break;
            case BodyPart.PartType.Torso:
                bodyCollection.torso = newestPart;
                break;
        }
        RecalcWeight();
        // Make a call to update the player's body state
        _nearestBodyPart.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && _nearestBodyPart)
        {
            AddPart();
        }
    }
}
