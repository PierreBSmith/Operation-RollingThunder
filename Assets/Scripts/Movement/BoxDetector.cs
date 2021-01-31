using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetector : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Box"))
        {
            Debug.Log("Found object");
            transform.parent.GetComponent<BodyPartMovement>().canCarryObject = collider.gameObject;
        }
        else if (collider.gameObject.CompareTag("Vine"))
        {
            transform.parent.GetComponent<BodyPartMovement>().canClimb = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Box"))
        {
            Debug.Log("Leaving Object");
            transform.parent.GetComponent<BodyPartMovement>().canCarryObject = null;
        }
        else if (collider.gameObject.CompareTag("Vine"))
        {
            transform.parent.GetComponent<BodyPartMovement>().canClimb = false;
        }
    }
}
