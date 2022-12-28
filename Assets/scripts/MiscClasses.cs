using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    /// <summary>
    /// amount of money the player has
    /// </summary>
    public float currency;

    /// <summary>
    /// how many modifiers can be equipped
    /// </summary>
    public int modifierSlots;
    ///<summary>
    ///modifiers equipped
    ///</summary>
    public List<Modifiers> equippedModifiers;

    /// <summary>
    /// time taken to relod shots
    /// </summary>
    public float reloadTime;
    /// <summary>
    /// bullet base Damage
    /// </summary>
    public float bulletDamage;

    public GameData()
    {
        currency = 0;
        modifierSlots = 1;
        reloadTime = 5;
        bulletDamage = 0.1f;
        equippedModifiers = new List<Modifiers>();

        //for testing
        equippedModifiers.Add(Modifiers.homing);
    }
}

public enum Modifiers
{
    explodeOnContact,
    explodeOnDeath,
    homing,
    vacummOnLife,
    Pierce,
    vacummOnDeath
}