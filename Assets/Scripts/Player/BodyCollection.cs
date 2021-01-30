using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Body Collection")]
public class BodyCollection : ScriptableObject
{
    public BodyPart head;
    public BodyPart torso;
    public BodyPart arms;
    public BodyPart legs;
}
