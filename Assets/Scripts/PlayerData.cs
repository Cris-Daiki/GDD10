using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new player data")]
public class PlayerData : ScriptableObject
{
    public int lives;
    public int level;
    public int experience;
    public float hp;
    public float maxhp;
    public float def;
    //public Inventario _inventario;
    //public int municion;
    //stats
    public float fireDelay;
    public float dmg;
}
