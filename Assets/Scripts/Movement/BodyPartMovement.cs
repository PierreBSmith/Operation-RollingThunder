using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartMovement : MonoBehaviour
{
    public AudioSource bounce;
    public AudioSource footsteps;
    [Header("General Variables")]
    public BodyPart _bodyPart;
    public AudioSource headroll;
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
        if(Physics2D.OverlapCircle(GroundDetector.position, 1f, whatIsGround))
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
        if(!headroll.isPlaying && isGrounded() && _r2D.velocity != new Vector2(0,0))
            headroll.Play();
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
            if(!bounce.isPlaying)
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
        }
        else
        {
            _playerMovement.facingRight = true;
        }
        if(!footsteps.isPlaying && isGrounded()){
            footsteps.Play();
        }
        _r2D.velocity = new Vector2(input * speed, _r2D.velocity.y);
        Climb(input);
        PickUp();
        PutDown();
    }

    private void Climb(float xMovement)
    {
        if (Input.GetKey(KeyCode.Space) && canClimb)
        {
            climbing = true;
            _r2D.velocity = new Vector2(xMovement * speed, climbSpeed);
        }
        else if ((!canClimb || Input.GetKeyUp(KeyCode.Space)) && climbing)
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
        }
        else
        {
            _playerMovement.facingRight = true;
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            if(!bounce.isPlaying)
                bounce.Play();
            _r2D.AddForce(new Vector2(_r2D.velocity.x, jumpForce));
            jumping = true;
        }
        if (input != 0 && !jumping)
        {
            if(!footsteps.isPlaying && isGrounded())
                footsteps.Play();
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
        }
        else
        {
            _playerMovement.facingRight = true;
        }
        Debug.Log(isGrounded());
        if(Input.GetKey(KeyCode.Space) && isGrounded() && !canClimb)
        {
            if(!bounce.isPlaying)
                bounce.Play();
            Debug.Log("Jumping");
            _r2D.velocity = new Vector2(_r2D.velocity.x, jumpForce);
            jumping = true;
        }
        if (input != 0 && !jumping)
        {
            if(!footsteps.isPlaying && isGrounded())
                footsteps.Play();
            _r2D.velocity = new Vector2(input * speed, _r2D.velocity.y);
            Climb(input);
            PickUp();
            PutDown();
            _animator.SetBool("Walking", true);
        }
    }
}
