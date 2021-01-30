using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D _r2D;

    // Start is called before the first frame update
    void Start()
    {
        _r2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        float input = Input.GetAxis("Horizontal");
        _r2D.velocity = new Vector2(input * speed, 0);
    }
}
