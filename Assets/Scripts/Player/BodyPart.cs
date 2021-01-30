using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Body Part")]
public class BodyPart : ScriptableObject
{
    public enum PartType
    {
        Head,
        Torso,
        Legs,
        Arms
    }
    
    public PartType partType;
    public int weight;
    public Sprite partAppearance;
}
