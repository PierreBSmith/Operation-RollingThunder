using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private Rigidbody2D _r2D;
    private GameObject mainBodyPart;

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
                mainBodyPart.GetComponent<BodyPartMovement>().LegMovement();
                break;
            case BodyState.ARM_HEAD:
                mainBodyPart.GetComponent<BodyPartMovement>().ArmMovement();
                //add throwing
                break;
            case BodyState.ARM_HEAD_TORSO:
                mainBodyPart.GetComponent<BodyPartMovement>().TorsoMovement();
                //add throwing
                break;
            case BodyState.ARM_LEG:
                mainBodyPart.GetComponent<BodyPartMovement>().LegMovement();
                break;
            case BodyState.ALL_PARTS:
            case BodyState.ARM_LEG_HEAD:
                mainBodyPart.GetComponent<BodyPartMovement>().ArmMovement();
                mainBodyPart.GetComponent<BodyPartMovement>().LegMovement();
                //add throwing
                break;
        }
    }
}
