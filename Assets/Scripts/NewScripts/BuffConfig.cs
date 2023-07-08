using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuff", menuName = "Data/Buff", order = 52)]
public class BuffConfig : ScriptableObject
{
    public float healthChange;
    public float armorCnahge;
    public float vampChange;

    public float damageHealthChange=1;
    public float damageArmorChange;
    public float damageVampChange;

    public float duration;

    public PlayerController _player;

    void Start () 
    {
        _player.health += healthChange;
        _player.armor += armorCnahge;
        _player.vamp+= vampChange;

        _player._damageHealth*= damageHealthChange;
        _player._damageArmor+= damageArmorChange;
        _player._damageVamp+= damageVampChange;
    }

}
