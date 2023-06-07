using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionPuerta : MonoBehaviour

{
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void AbrirPuerta(){
        ani.SetBool("ApretoLetra",true);
    }
}
