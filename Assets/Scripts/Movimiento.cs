using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float VelocidadMovimiento =5.0f;
    public float VelocidadRotacion =200.0f;
    private Animator anim;
    public float x,y;
    public Rigidbody rb;
    public float fuerzasalto = 8f;
    public bool puedoSaltar;

    public bool EstoyAtacando;
    public bool AvanzoSolo;
    public float ImpulsodDeGolpe =10f;






    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
        puedoSaltar = false; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!EstoyAtacando){
            transform.Rotate(0,x*Time.deltaTime * VelocidadRotacion,0);
            transform.Translate (0,0,y*Time.deltaTime *VelocidadMovimiento);
        }
        if(AvanzoSolo){
            rb.velocity = transform.forward* ImpulsodDeGolpe;
        }
        
    }
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if(Input.GetMouseButtonDown(1)&& puedoSaltar && !EstoyAtacando){
            anim.SetTrigger("Hechizo1 ");
            EstoyAtacando = true;
        }
        transform.Rotate(0, x * Time.deltaTime *VelocidadRotacion , 0 );
        transform.Translate(0, 0, y * Time.deltaTime *VelocidadMovimiento);
        anim.SetFloat("Velx",x);
        anim.SetFloat("Vely",y);
        if(puedoSaltar){
            if(!EstoyAtacando){
                if(Input.GetKeyDown(KeyCode.Space)){
                    anim.SetBool("Salte",true);
                    rb.AddForce(new Vector3(0, fuerzasalto,0),ForceMode.Impulse);
                }
                anim.SetBool("TocarSuelo",true);
            }
            
        }
        else{
            EstoyCayendo();
        }
    }
    public void EstoyCayendo(){
        anim.SetBool("TocarSuelo",false);
        anim.SetBool("Salte",false);
    }
    public void DejeDeGolpear(){
        EstoyAtacando = false;
    }
    public void AvanzoSoloS(){
        AvanzoSolo = true;
    }
    public void DejoDeAvanzar(){
        AvanzoSolo = false;
    }
}
