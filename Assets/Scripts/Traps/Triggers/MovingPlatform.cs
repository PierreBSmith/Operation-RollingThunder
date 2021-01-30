using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : Toggleable
{
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDuration;
    [SerializeField] private float pauseDuration;

    private bool _isOn;
    private bool _isMoving;
    private bool _flipped;
    private float _durationMoved;
    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        moveDirection = moveDirection.normalized;
    }

    public override void ActivatedFunction()
    {
        _isOn = true;
        _isMoving = true;
        _body.velocity = moveDirection * moveSpeed;
    }

    public override void DeactivatedFunction()
    {
        _isOn = false;
        _isMoving = false;
        _body.velocity = Vector2.zero;
    }

    private IEnumerator CyclePause()
    {
        _isMoving = false;
        
        yield return new WaitForSeconds(pauseDuration);
        if (_isOn)
        {
            _isMoving = true;
            _body.velocity = moveDirection * moveSpeed;
        }

    }
    private void Update()
    {
        if (_isOn && _isMoving)
        {
            _durationMoved += Time.deltaTime;
            if (_durationMoved >= moveDuration)
            {
                _durationMoved = 0;
                moveDirection *= -1;
                _flipped = !_flipped;
                _body.velocity = Vector2.zero;
                StartCoroutine(CyclePause());
            }
        }
    }
}