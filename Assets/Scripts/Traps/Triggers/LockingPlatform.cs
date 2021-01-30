using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LockingPlatform : Toggleable
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
        StartCoroutine(DeactivateDelay());
    }

    private IEnumerator DeactivateDelay()
    {
        yield return new WaitForSeconds(pauseDuration);
        _isOn = false;
        _isMoving = true;
        _body.velocity = moveDirection * -moveSpeed;
    }

    private void Update()
    {
        if (_isMoving)
        {
            if (_isOn)
            {
                _durationMoved += Time.deltaTime;
                if (_durationMoved >= moveDuration)
                {
                    _isMoving = false;
                    _body.velocity = Vector2.zero;
                }
            }
            else
            {
                _durationMoved -= Time.deltaTime;
                if (_durationMoved <= 0)
                {
                    _isMoving = false;
                    _body.velocity = Vector2.zero;
                }
            }
        }
    }
}
