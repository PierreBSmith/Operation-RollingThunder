using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private Rigidbody2D _r2D;
    private Animator _animator;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        _r2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(grounded);
        Movement();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Floor")
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        grounded = false;
    }

    public void Movement()
    {
        Jump();
        float input = Input.GetAxis("Horizontal");
        _animator.SetBool("Walking", false);
        if (input != 0)
        {
            _r2D.velocity = new Vector2(speed * input, 0);
            _animator.SetBool("Walking", true);
        }
    }

    private void Jump()
    {
        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            Debug.Log("Jumping");
            Vector2 jump = new Vector2(0, jumpForce);
            _r2D.AddForce(jump);
        }
    }
}
