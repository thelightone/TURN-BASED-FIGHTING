using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Data/Unit", order = 51)]
public class UnitConfig : ScriptableObject
{
    public float health = 100;
    public float armor = 0;
    public float vamp = 0;

    public float damageHealth = 15;
    public float damageArmor = 0;
    public float damageVamp = 0;
}
