using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    public Rigidbody2D _r2D;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        _r2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        _r2D.velocity = new Vector2(input * speed, 0);
    }
}
