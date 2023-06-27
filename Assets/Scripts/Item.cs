using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public Sprite itemIcon;
    public int amount;
    public float alter_hp;
    public float alter_maxhp;
    public float alter_def;
    public float alter_fireDelay;
    public float alter_dmg;

    public void Interact(Movimiento player)
    {
        player.AddToInventory(this);
        Destroy(this.gameObject);
    }
}
