using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactuableanimacion : Interactuable
{
    AnimacionPuerta ONOFF;
    public override void Interact(){
         
        base.Interact();
        ONOFF = GameObject.FindGameObjectWithTag("RetoJugador").GetComponent<AnimacionPuerta>();
        ONOFF.AbrirPuerta();
        print("ASKDJLAKSDJLASD");
        
    }
}
