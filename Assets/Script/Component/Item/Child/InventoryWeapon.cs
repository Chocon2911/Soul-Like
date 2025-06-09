using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWeapon : InventoryItems
{
    [Header("===Weapon===")]
    public WeaponCode code;
    public int physicsDamage;
    public int magicDamage;
    public float strengthScale;
    public float inteligenceScale;
    public float attackSpeek;
}
