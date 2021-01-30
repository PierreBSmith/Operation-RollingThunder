using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerManager playerManager;
    private Rigidbody2D _r2D;
    private GameObject[] collectedBodyParts;

    private enum BodyState
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
    private BodyState _bodyState;
    
    public void SetBodyState()
    {
        if(playerManager._bodyCollection.head)
        {
            if(playerManager._bodyCollection.torso)
            {
                if(playerManager._bodyCollection.legs)
                {
                    if(playerManager._bodyCollection.arms)
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
                else if(playerManager._bodyCollection.arms)
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
            else if (playerManager._bodyCollection.legs)
            {
                if(playerManager._bodyCollection.arms)
                {
                    //head leg arm, take leg and arm movement and throwing
                    _bodyState = BodyState.ARM_LEG_HEAD;
                }
                else
                {
                    //only head and legs
                    _bodState = BodyState.LEG;
                }
            }
            else if(playerManager._bodyCollection.arms)
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
        _bodyState = BodyState.HEAD;
    }

    void FixedUpdate()
    {
        switch(_bodyState)
        {
            case BodyState.HEAD:
                gameObject.transform.GetChild(0).gameObject.GetComponent<BodyPartMovement>().HeadMovement();
                break;
            case BodyState.TORSO:
                gameObject.transform.GetChild(0).gameObject.GetComponent<BodyPartMovement>().TorsoMovement();
                break;
            case BodyState.LEG:
                gameObject.transform.GetChild(0).gameObject.GetComponent<BodyPartMovement>().LegMovement();
                break;
            case BodyState.ARM_HEAD:
                gameObject.transform.GetChild(0).gameObject.GetComponent<BodyPartMovement>().ArmMovement();
                //add throwing
                break;
            case BodyState.ARM_HEAD_TORSO:
                gameObject.transform.GetChild(0).gameObject.GetComponent<BodyPartMovement>().TorsoMovement();
                //add throwing
                break;
            case BodyState.ARM_LEG:
                gameObject.transform.GetChild(0).gameObject.GetComponent<BodyPartMovement>().LegMovement();
                break;
            case BodyState.ALL_PARTS:
            case BodyState.ARM_LEG_HEAD:
                gameObject.transform.GetChild(0).gameObject.GetComponent<BodyPartMovement>().ArmMovement();
                gameObject.transform.GetChild(0).gameObject.GetComponent<BodyPartMovement>().LegMovement();
                //add throwing
                break;
        }
    }
}
