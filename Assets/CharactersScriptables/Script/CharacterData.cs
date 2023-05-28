using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactersData", menuName = "ScriptableObjects/CharactersData", order = 1)]
public class CharacterData : ScriptableObject
{
    [Range(10, 500)]
    public int StartingCount;

    [Range(1f, 100f)]
    public float DriveFactor;
    [Range(1f, 100f)]
    public int MaxSpeed;

    [Range(1f, 10f)]
    public float NeighBorRadius;

    [Range(0f, 1f)]
    public float AvoidanceRadiusMultiplier;

    [Range(0, 100)]
    public int Health;

    [Range(0, 100)]
    public int DMG;


}
