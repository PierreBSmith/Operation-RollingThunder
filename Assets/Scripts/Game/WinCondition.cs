using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public AudioSource youWin;
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
        youWin.Play();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        Application.Quit();
    }
}
