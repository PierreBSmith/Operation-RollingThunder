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
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject torso;
    [SerializeField] private GameObject leg;
    [SerializeField] private GameObject arm;

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
                head.SetActive(true);
                head.GetComponent<SpriteRenderer>().sprite = newestPart.partAppearance;
                break;
            case BodyPart.PartType.Arms:
                bodyCollection.arms = newestPart;
                gameObject.transform.rotation = Quaternion.identity;
                arm.SetActive(true);
                SpriteRenderer[] armSprites = arm.GetComponentsInChildren<SpriteRenderer>();
                foreach(SpriteRenderer appearance in armSprites)
                {
                    appearance.sprite = newestPart.partAppearance;
                }
                break;
            case BodyPart.PartType.Legs:
                bodyCollection.legs = newestPart;
                if(OnlyHead())
                {
                    gameObject.transform.rotation = Quaternion.identity;
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1.3f);
                }
                leg.SetActive(true);
                SpriteRenderer[] legSprites = leg.GetComponentsInChildren<SpriteRenderer>();
                foreach(SpriteRenderer appearance in legSprites)
                {
                    appearance.sprite = newestPart.partAppearance;
                }
                break;
            case BodyPart.PartType.Torso:
                bodyCollection.torso = newestPart;
                if(OnlyHead())
                {
                    gameObject.transform.rotation = Quaternion.identity;
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1.3f);
                }
                torso.SetActive(true);
                torso.GetComponent<SpriteRenderer>().sprite = newestPart.partAppearance;
                break;
        }
        RecalcWeight();
        // Make a call to update the player's body state
        _nearestBodyPart.SetActive(false);
        GetComponent<PlayerMovement>().SetBodyState();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && _nearestBodyPart)
        {
            AddPart();
        }
    }

    private bool OnlyHead()
    {
        bool hasOnlyHead = true;
        foreach (Transform child in transform)
        {
            if(child.gameObject.name != "Head" && child.gameObject.activeSelf)
            {
                hasOnlyHead = false;
                break;
            }
        }
        return hasOnlyHead;
    }
}
