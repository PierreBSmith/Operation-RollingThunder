﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartMovement : MonoBehaviour
{
    public AudioSource bounce;
    //public AudioSource footsteps;
    [Header("General Variables")]
    public BodyPart _bodyPart;
    [SerializeField] private float speed;
    private Rigidbody2D _r2D;
    [HideInInspector] public Animator _animator;
    private PlayerMovement _playerMovement;
    public Transform GroundDetector;
    public LayerMask whatIsGround;

    [Header("Torso")]
    [SerializeField] private float jumpMultiplier;

    [Header("Arm")]
    [SerializeField] private float climbSpeed;
    [HideInInspector] public bool canClimb;
    private bool climbing;
    [HideInInspector] public bool carrying;
    [HideInInspector] public GameObject canCarryObject;
    private GameObject carryingObject;

    [Header("Leg")]
    [SerializeField] private float jumpForce;
    private bool jumping;

    // Start is called before the first frame update
    void Start()
    {
        _r2D = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        climbing = false;
        carrying = false;

        _playerMovement = transform.parent.gameObject.GetComponent<PlayerMovement>();
    }

    private bool isGrounded()
    {
        bool grounded;
        if(Physics2D.OverlapCircle(GroundDetector.position, 0.1f, whatIsGround))
        {
            grounded = true;
            jumping = false;
        }
        else
        {
            grounded = false;
        }
        return grounded;
    }
    
    public void SetLegAnimator(Animator animator)
    {
        _animator = animator;
    }

    public void HeadMovement()
    {
        float input = Input.GetAxis("Horizontal");
        if (input <= 0)
        {
            _playerMovement.facingRight = false;
        }
        else
        {
            _playerMovement.facingRight = true;
        }
        _r2D.constraints = RigidbodyConstraints2D.None;
        _r2D.velocity = new Vector2(input * speed, _r2D.velocity.y);
    }

    public void TorsoMovement()
    {
        _r2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        float input = Input.GetAxis("Horizontal");
        if (input <= 0)
        {
            _playerMovement.facingRight = false;
        }
        else
        {
            _playerMovement.facingRight = true;
        }
        if(input != 0 && isGrounded())
        {
            bounce.Play();
            _r2D.velocity = new Vector2(speed * input, jumpMultiplier);
        }
    }

    public void ArmMovement()
    {
        _r2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        float input = Input.GetAxis("Horizontal");
        if (input <= 0)
        {
            _playerMovement.facingRight = false;
           // footsteps.Play();
        }
        else
        {
            _playerMovement.facingRight = true;
            //footsteps.Play();
        }
        _r2D.velocity = new Vector2(input * speed, _r2D.velocity.y);
        Climb();
        PickUp();
        PutDown();
    }

    private void Climb()
    {
        if (Input.GetKey(KeyCode.Space) && canClimb)
        {
            climbing = true;
            _r2D.velocity = Vector2.up * climbSpeed;
        }
        else if (!canClimb && climbing)
        {
            climbing = false;
        }
    }

    private IEnumerator BoxPickUp()
    {
        canCarryObject.transform.SetParent(this.gameObject.transform.parent, true);
        carryingObject = canCarryObject;
        yield return new WaitForSeconds(1f);
        carrying = true;
    }

    private void PickUp()
    {
        if(canCarryObject)
        {
            if(Input.GetKey(KeyCode.LeftShift) && !carrying)
            {
               StartCoroutine(BoxPickUp()); 
            }
        }
    }
    private IEnumerator BoxPutDown()
    {
        carryingObject.transform.SetParent(null, true);
        yield return new WaitForSeconds(1f);
        carrying = false;
    }
    private void PutDown()
    {
        if(Input.GetKey(KeyCode.LeftShift) && carrying)
        {
            StartCoroutine(BoxPutDown());
        }
    }
    
    public void LegMovement()
    {
        _r2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        float input = Input.GetAxis("Horizontal");
        _animator.SetBool("Walking", false);
        if (input <= 0)
        {
            _playerMovement.facingRight = false;
            //footsteps.Play();
        }
        else
        {
            _playerMovement.facingRight = true;
            //footsteps.Play();
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            _r2D.AddForce(new Vector2(input * speed, jumpForce));
            jumping = true;
        }
        if (input != 0 && !jumping)
        {
            _r2D.velocity = new Vector2(input * speed, _r2D.velocity.y);
            _animator.SetBool("Walking", true);
        }
    }

    public void ArmLegMovement()
    {
        _r2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        float input = Input.GetAxis("Horizontal");
        _animator.SetBool("Walking", false);
        if (input <= 0)
        {
            _playerMovement.facingRight = false;
           // footsteps.Play();
        }
        else
        {
            _playerMovement.facingRight = true;
            //footsteps.Play();
        }
        Debug.Log(isGrounded());
        if(Input.GetKey(KeyCode.Space) && isGrounded() && !canClimb)
        {
            Debug.Log("Jumping");
            _r2D.velocity = new Vector2(input * speed, jumpForce);
            jumping = true;
        }
        if (input != 0 && !jumping)
        {
            _r2D.velocity = new Vector2(input * speed, _r2D.velocity.y);
            Climb();
            PickUp();
            PutDown();
            _animator.SetBool("Walking", true);
        }
    }
}
