using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterInfo", menuName = "MovilesHipercasual1/Information", order = 0)]
public class CharacterInfo : ScriptableObject {
    
    //Unit Stats
    [Tooltip("Health point variable for unit, how much damage can the unit take.")]
    [Range(1, 20)]
    public int baseHealth;

    [Tooltip("Attack Speed variable for unit, how many seconds it takes for the unit to fire")]
    [Range(1, 20)]
    public int baseAttackSpeed;

    [Tooltip("Movement speed variable for unit, how fast does the unit go")]
    [Range(0, 20)]
    public float baseMovementSpeed;

    [Tooltip("Rotation speed variable for unit, how fast does the unit rotate")]
    [Range(10, 20)]
    public float baseRotationSpeed;

}

