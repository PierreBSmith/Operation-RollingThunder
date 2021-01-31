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
    [HideInInspector] public Animator _animator;

    [Header("Torso")]
    [SerializeField] private float jumpMultiplier;

    [Header("Arm")]
    [SerializeField] private float climbSpeed;
    private bool climbing;
    private bool carrying;
    private GameObject canCarryObject;
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

    public void SetLegAnimator(Animator animator)
    {
        _animator = animator;
    }

    public void HeadMovement()
    {
        float input = Input.GetAxis("Horizontal");
        _r2D.constraints = RigidbodyConstraints2D.None;
        _r2D.velocity = new Vector2(input * speed, _r2D.velocity.y);
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
        _r2D.velocity = new Vector2(input * speed, _r2D.velocity.y);
        //Climb();
        PickUp();
        PutDown();
    }

    private void Climb()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            climbing = true;
            _r2D.velocity = Vector2.up * climbSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && climbing)
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
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            _r2D.AddForce(new Vector2(_r2D.velocity.x, jumpForce));
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
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            _r2D.AddForce(new Vector2(_r2D.velocity.x, jumpForce));
            jumping = true;
        }
        if (input != 0 && !jumping)
        {
            _r2D.velocity = new Vector2(input * speed, _r2D.velocity.y);
            //Climb();
            PickUp();
            PutDown();
            _animator.SetBool("Walking", true);
        }
    }
}
