using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    public float speed;
    [SerializeField]
    private float climbSpeed;

    private Rigidbody2D _r2D;

    [SerializeField]private float climbTime;
    private float curClimbTime;
    private bool climbing;
    [SerializeField]
    private Transform wallCheck;
    private float checkRadius = 0.1f;
    [SerializeField]
    private LayerMask whatIsWall;


    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        _r2D = GetComponent<Rigidbody2D>();
        curClimbTime = 0f;
        climbing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Floor")
        {
            Debug.Log("On ground.");
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        grounded = false;
    }

    public void Movement()
    {
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
    }

    public void Climb()
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
}
