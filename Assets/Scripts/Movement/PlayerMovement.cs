using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private Rigidbody2D _r2D;
    private GameObject mainBodyPart;

    [HideInInspector] public bool facingRight;

    [SerializeField] private float throwStrengthX;
    [SerializeField] private float throwStrengthY;

    public enum BodyState
    {
        HEAD,
        TORSO,
        LEG,
        ARM_HEAD_TORSO,
        ARM_HEAD,
        ARM_LEG,
        ARM_LEG_HEAD,
        ALL_PARTS
    }
    public BodyState _bodyState;
    
    public void SetBodyState()
    {
        if(playerInventory.bodyCollection.head)
        {
            if(playerInventory.bodyCollection.torso)
            {
                if(playerInventory.bodyCollection.legs)
                {
                    if(playerInventory.bodyCollection.arms)
                    {
                        //this also takes leg movment, but also head throwing, just an easy check
                        _bodyState = BodyState.ALL_PARTS;
                    }
                    else
                    {
                        //if head, torso, leg, then take leg movement
                        _bodyState = BodyState.LEG;
                    }
                }
                else if(playerInventory.bodyCollection.arms)
                {
                    //has head, torso, arm, then take torso movement, but throwing enabled
                    _bodyState = BodyState.ARM_HEAD_TORSO;
                }
                else
                {
                    //only head and torso, take torso movement
                    _bodyState = BodyState.TORSO;
                }
            }
            else if (playerInventory.bodyCollection.legs)
            {
                if(playerInventory.bodyCollection.arms)
                {
                    //head leg arm, take leg and arm movement and throwing
                    _bodyState = BodyState.ARM_LEG_HEAD;
                }
                else
                {
                    //only head and legs
                    _bodyState = BodyState.LEG;
                }
            }
            else if(playerInventory.bodyCollection.arms)
            {
                //only head and arm throwing and arm movement
                _bodyState = BodyState.ARM_HEAD;
            }
            else
            {
                //lmao you only head
                _bodyState = BodyState.HEAD;
            }
        }
    }

    void Start()
    {
        //_bodyState = BodyState.HEAD;
        mainBodyPart = gameObject.transform.GetChild(0).gameObject;
        _r2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        switch(_bodyState)
        {
            case BodyState.HEAD:
                mainBodyPart.GetComponent<BodyPartMovement>().HeadMovement();
                break;
            case BodyState.TORSO:
                mainBodyPart.GetComponent<BodyPartMovement>().TorsoMovement();
                break;
            case BodyState.LEG:
                foreach(Transform child in transform)
                {
                    if(child.name == "Legs")
                    {
                        mainBodyPart.GetComponent<BodyPartMovement>().SetLegAnimator(child.gameObject.GetComponent<Animator>());
                    }
                }
                mainBodyPart.GetComponent<BodyPartMovement>().LegMovement();
                break;
            case BodyState.ARM_HEAD:
                mainBodyPart.GetComponent<BodyPartMovement>().ArmMovement();
                ThrowHead();
                break;
            case BodyState.ARM_HEAD_TORSO:
                mainBodyPart.GetComponent<BodyPartMovement>().TorsoMovement();
                ThrowHead();
                break;
            case BodyState.ARM_LEG:
                foreach(Transform child in transform)
                {
                    if(child.name == "Legs")
                    {
                        mainBodyPart.GetComponent<BodyPartMovement>().SetLegAnimator(child.gameObject.GetComponent<Animator>());
                    }
                }
                mainBodyPart.GetComponent<BodyPartMovement>().ArmLegMovement();
                break;
            case BodyState.ALL_PARTS:
            case BodyState.ARM_LEG_HEAD:
                foreach(Transform child in transform)
                {
                    if(child.name == "Legs")
                    {
                        mainBodyPart.GetComponent<BodyPartMovement>().SetLegAnimator(child.gameObject.GetComponent<Animator>());
                    }
                }
                mainBodyPart.GetComponent<BodyPartMovement>().ArmLegMovement();
                ThrowHead();
                break;
        }
    }

    private void ThrowHead()
    {
        if(!mainBodyPart.GetComponent<BodyPartMovement>().carrying)
        {
            if(Input.GetKey(KeyCode.Q))
            {
                //removes all body parts that are not head.
                foreach(Transform child in transform)
                {
                    if(child.gameObject.name != "Head")
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                //launches head
                StartCoroutine(LaunchHead());
            }
        }
    }

    private IEnumerator LaunchHead()
    {
        _bodyState = BodyState.HEAD;
        if(facingRight)
        {
            _r2D.velocity = new Vector2(throwStrengthX, throwStrengthY);
        }
        else
        {
            _r2D.velocity = new Vector2(-throwStrengthX, throwStrengthY);
        }
        yield return new WaitForSeconds(1f);
    }
}
