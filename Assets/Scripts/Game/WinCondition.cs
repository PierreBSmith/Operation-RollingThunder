using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            if(collider.gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>()._bodyState == 
            PlayerMovement.BodyState.ALL_PARTS)
            {
                StartCoroutine(Win());
            }
        }
    }

    private IEnumerator Win()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealTime(3);
        Time.timeScale = 1;
        Application.Quit();
    }
}
