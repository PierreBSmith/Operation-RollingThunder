using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables for Checking")]
    [SerializeField] private BodyCollection _bodyCollection;
    private Rigidbody2D[] _r2DCollection;
    private Collider2D[] _colliderCollection;
    private GameObject[] collectedBodyParts;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
