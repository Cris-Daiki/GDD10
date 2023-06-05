using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public int rutina;
    public float Cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();  
        target = GameObject.Find("Personaje");
    }

    // Update is called once per frame

    public void Comportamiento_Enemigo(){
        if(Vector3.Distance(transform.position, target.transform.position)>10){
            ani.SetBool("run",false);
            Cronometro +=1*Time.deltaTime;
            if(Cronometro >= 4){
                rutina = Random.Range(0,2);
                Cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;
                case 1:
                    grado  = Random.Range(0,360);
                    angulo = Quaternion.Euler(0,grado,0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation , angulo , 0.5f);
                    transform.Translate(Vector3.forward*1*Time.deltaTime);
                    ani.SetBool("walk",true);
                    break;
            }

        }else{
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation,3);
            ani.SetBool("walk",false);
            ani.SetBool("run",true);
            transform.Translate(Vector3.forward *10*Time.deltaTime);

        }

    }
    void Update()
    {
        Comportamiento_Enemigo();
    }
}