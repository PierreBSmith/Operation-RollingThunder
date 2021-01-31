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

    private int _colorIndex;
    private readonly int _indexCycles = 6;

    private readonly float _increment = 0.015f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = new Color(1f,0f,0f,1f);
        ReloadSprite();
    }

    public void ReloadSprite()
    {
        if (bodyPart && bodyPart.partAppearance)
            _spriteRenderer.sprite = bodyPart.partAppearance;
    }

    private void FixedUpdate()
    {
        switch (_colorIndex)
        {
            case 0:
                // increase blue
                _spriteColor.b += _increment;
                if (_spriteColor.b >= 1f)
                {
                    _spriteColor.b = 1f;
                    _colorIndex = (_colorIndex + 1) % _indexCycles;
                }
                _spriteRenderer.color = _spriteColor;
                break;
            case 1:
                // reduce red
                _spriteColor.r -= _increment;
                if (_spriteColor.r <= 0f)
                {
                    _spriteColor.r = 0f;
                    _colorIndex = (_colorIndex + 1) % _indexCycles;
                }
                _spriteRenderer.color = _spriteColor;
                break;
            case 2:
                // increase green
                _spriteColor.g += _increment;
                if (_spriteColor.g >= 1f)
                {
                    _spriteColor.g = 1f;
                    _colorIndex = (_colorIndex + 1) % _indexCycles;
                }
                _spriteRenderer.color = _spriteColor;
                break;
            case 3:
                // reduce blue
                _spriteColor.b -= _increment;
                if (_spriteColor.b <= 0f)
                {
                    _spriteColor.b = 0f;
                    _colorIndex = (_colorIndex + 1) % _indexCycles;
                }
                _spriteRenderer.color = _spriteColor;
                break;
            case 4:
                // increase red
                _spriteColor.r += _increment;
                if (_spriteColor.r >= 1f)
                {
                    _spriteColor.r = 1f;
                    _colorIndex = (_colorIndex + 1) % _indexCycles;
                }
                _spriteRenderer.color = _spriteColor;
                break;
            case 5:
                // reduce green
                _spriteColor.g -= _increment;
                if (_spriteColor.g <= 0f)
                {
                    _spriteColor.g = 0f;
                    _colorIndex = (_colorIndex + 1) % _indexCycles;
                }
                _spriteRenderer.color = _spriteColor;
                break;
        }
    }
}
