using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : Toggleable
{
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool cycles;
    [SerializeField] private float timeBeforeCycling;

    private bool _isOn;
    private Rigidbody _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    public override void ActivatedFunction()
    {
        _isOn = true;
        _body.velocity = moveDirection * (moveSpeed * Time.deltaTime);
    }

    public override void DeactivatedFunction()
    {
        _isOn = false;
    }

    private void FixedUpdate()
    {
        if (_isOn)
        {

        }
    }
}
