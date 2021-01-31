using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartsSpawner : MonoBehaviour
{
    [SerializeField] private BodyPart[] parts;

    void Start()
    {
        if (parts.Length != 0)
        {
            GameObject[] pickups = GameObject.FindGameObjectsWithTag("BodyPart");
            Queue<BodyPart> shuffledParts = new Queue<BodyPart>(parts.OrderBy( x => Random.value).ToList());
            foreach (GameObject pickup in pickups)
            {
                PartPickup partPickup = pickup.GetComponent<PartPickup>();
                partPickup.bodyPart = shuffledParts.Dequeue();
                shuffledParts.Enqueue(partPickup.bodyPart);
                partPickup.ReloadSprite();
            }
        }
    }
}
