using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoMovement : MonoBehaviour
{
    public float speed;
    public float jumpMultiplier;

    private Rigidbody2D _r2D;
    private bool grounded;

    private Vector2 force;

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

    // Start is called before the first frame update
    void Start()
    {
        _r2D = GetComponent<Rigidbody2D>();
        grounded = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");
        if (input != 0 && grounded)
        {
            _r2D.velocity = new Vector2(speed * input, jumpMultiplier);
            grounded = false;
        }
    }
}
