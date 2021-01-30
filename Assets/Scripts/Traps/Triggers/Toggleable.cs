using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Toggleable : MonoBehaviour
{
    public float activationDelay;
    public float deactivationDelay;

    public IEnumerator Activate()
    {
        yield return new WaitForSeconds(activationDelay);
        ActivatedFunction();
    }

    public abstract void ActivatedFunction();

    public IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(deactivationDelay);
        DeactivatedFunction();
    }
    
    public abstract void DeactivatedFunction();
}
