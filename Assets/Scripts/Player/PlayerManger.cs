using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManger : MonoBehaviour
{
    public int weight;
    
    [SerializeField] private BodyCollection baseBodyCollection;
    [SerializeField] private int baseWeight;
    
    [HideInInspector] public BodyCollection _bodyCollection;
    private void Awake()
    {
        _bodyCollection = Instantiate(baseBodyCollection);
        RecalcWeight();
    }

    private void RecalcWeight()
    {
        weight = baseWeight;
        weight += _bodyCollection.head != null ? _bodyCollection.head.weight : 0;
        weight += _bodyCollection.arms != null ? _bodyCollection.arms.weight : 0;
        weight += _bodyCollection.legs != null ? _bodyCollection.legs.weight : 0;
        weight += _bodyCollection.torso != null ? _bodyCollection.torso.weight : 0;
    }
}
