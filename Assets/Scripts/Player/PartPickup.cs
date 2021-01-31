using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class PartPickup : MonoBehaviour
{
    public BodyPart bodyPart;

    private SpriteRenderer _spriteRenderer;
    private Color _spriteColor;

    private float _colorIndex;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = _spriteRenderer.color;
        ReloadSprite();
    }

    public void ReloadSprite()
    {
        if (bodyPart && bodyPart.partAppearance)
            _spriteRenderer.sprite = bodyPart.partAppearance;
    }

    // private void FixedUpdate()
    // {
    //     _spriteColor = (_spriteColor + 1) % 
    // }
}
