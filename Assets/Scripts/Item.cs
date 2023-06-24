using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public Sprite itemIcon;
    public int amount;

    public void Interact(Movimiento player)
    {
        player.AddToInventory(this);
        Destroy(this.gameObject);
    }
}
