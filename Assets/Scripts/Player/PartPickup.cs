using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class PartPickup : MonoBehaviour
{
    public BodyPart bodyPart;

    private SpriteRenderer _spriteRenderer; 
        
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (bodyPart && bodyPart.partAppearance)
            _spriteRenderer.sprite = bodyPart.partAppearance;
    }
}
