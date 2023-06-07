using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Proyectile")]
public class Proyectile : ScriptableObject
{
    public GameObject bullet_prefab;
    //public int bullet_dmg;
    public float bullet_velocity;
    public float bullet_reach;
    public BulletEffect[] bullet_effects;
    public enum BulletEffect
    {
        normal,
        big
    }
}
