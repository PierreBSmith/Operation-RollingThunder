using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManger : MonoBehaviour
{
    public int weight;
    
    [SerializeField] private BodyCollection baseBodyCollection;
    [SerializeField] private int baseWeight;

    private GameObject _nearestBodyPart;
    public BodyCollection _bodyCollection;

    private void Awake()
    {
        _bodyCollection = Instantiate(baseBodyCollection);
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
        weight += _bodyCollection.head != null ? _bodyCollection.head.weight : 0;
        weight += _bodyCollection.arms != null ? _bodyCollection.arms.weight : 0;
        weight += _bodyCollection.legs != null ? _bodyCollection.legs.weight : 0;
        weight += _bodyCollection.torso != null ? _bodyCollection.torso.weight : 0;
    }

    private void AddPart()
    {
        BodyPart newestPart = _nearestBodyPart.GetComponent<PartPickup>().bodyPart;
        switch (newestPart.partType)
        {
            case BodyPart.PartType.Head:
                _bodyCollection.head = newestPart;
                break;
            case BodyPart.PartType.Arms:
                _bodyCollection.arms = newestPart;
                break;
            case BodyPart.PartType.Legs:
                _bodyCollection.legs = newestPart;
                break;
            case BodyPart.PartType.Torso:
                _bodyCollection.torso = newestPart;
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
