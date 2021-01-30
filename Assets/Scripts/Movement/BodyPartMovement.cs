using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartMovement : MonoBehaviour
{
    [Header("General Variables")]
    public BodyPart _bodyPart;
    [SerializeField] private float speed;
    private bool grounded;
    private Rigidbody2D _r2D;
    private Animator _animator;

    [Header("Torso")]
    [SerializeField] private float jumpMultiplier;

    [Header("Arm")]
    [SerializeField] private float climbSpeed;
    [SerializeField] private float climbTime;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask whatIsWall;
    private float curClimbTime;
    private bool climbing;
    private float checkRadius = 0.1f;
    private bool carrying;
    private GameObject canCarryObject;

    [Header("Leg")]
    [SerializeField] private float jumpForce;
    private bool jumping;

    // Start is called before the first frame update
    void Start()
    {
        _r2D = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        
        if (_bodyPart.partType == BodyPart.PartType.Legs)
        {
            _animator = GetComponent<Animator>();
        }
        curClimbTime = 0f;
        climbing = false;
        carrying = false;
    }

    void OnCollisionStay2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Floor"))
        {
            grounded = true;
            jumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        grounded = false;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Box"))
        {
            Debug.Log("Found object");
            canCarryObject = collider.gameObject;
            Debug.Log(canCarryObject.name);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Box"))
        {
            Debug.Log("Leaving Object");
            canCarryObject = null;
        }
    }

    public void HeadMovement()
    {
        float input = Input.GetAxis("Horizontal");
        _r2D.constraints = RigidbodyConstraints2D.None;
        _r2D.velocity = new Vector2(input * speed, 0);
    }

    public void TorsoMovement()
    {
        _r2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        float input = Input.GetAxis("Horizontal");
        if(input != 0 && grounded)
        {
            _r2D.velocity = new Vector2(speed * input, jumpMultiplier);
            grounded = false;
        }
    }

    public void ArmMovement()
    {
        _r2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        float input = Input.GetAxis("Horizontal");
        _r2D.velocity = new Vector2(input * speed, 0);
        if(climbing)
        {
            if(curClimbTime < climbTime)
            {
                curClimbTime += Time.deltaTime;
            }
            else
            {
                curClimbTime = 0f;
                climbing = false;
                _r2D.gravityScale = 1f;
            }
        }
        if(Physics2D.OverlapCircle(wallCheck.position, checkRadius, whatIsWall))
        {
            Climb();
        }
        else
        {
            curClimbTime = 0f;
            climbing = false;
            _r2D.gravityScale = 1f;
        }
        //TODO: Add picking up boxes and stuff.
        PickUp();
        PutDown();
    }

    private void Climb()
    {
        if (Input.GetKey(KeyCode.Space) && curClimbTime < climbTime)
        {
            climbing = true;
            _r2D.gravityScale = 0;
            _r2D.velocity = Vector2.up * climbSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && climbing)
        {
            curClimbTime = 0f;
            climbing = false;
            _r2D.gravityScale = 1f;
        }
    }

    private void PickUp()
    {
        if(canCarryObject)
        {
            if(Input.GetKey(KeyCode.LeftShift) && !carrying)
            {
                Debug.Log("Entering carrying");
                carrying = true;
                canCarryObject.transform.parent = gameObject.transform.parent;
            }
        }
    }
    private void PutDown()
    {
        if(Input.GetKey(KeyCode.LeftShift) && carrying)
        {
            carrying = false;
            canCarryObject.transform.parent = null;
        }
    }
    
    public void LegMovement()
    {
        _r2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        float input = Input.GetAxis("Horizontal");
        _animator.SetBool("Walking", false);
        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            _r2D.AddForce(new Vector2(_r2D.velocity.x, jumpForce));
            jumping = true;
        }
        if (input != 0 && !jumping)
        {
            _r2D.velocity = new Vector2(speed * input, 0);
            _animator.SetBool("Walking", true);
        }
    }
}
