using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public int rutina;
    public float Cronometro, grado;
    public Animator ani;
    public Quaternion angulo;
    //stats
    public float hp, attack;
    public int exp;
    bool enable_attack = true;
    public float VelocidadMovimiento;

    private GameObject target;

    public float playerInSightRange;

    public float timeBetweenAttacks;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();  
        target = GameObject.Find("Personaje");
        StartCoroutine(AttackDelay());
        VelocidadMovimiento = 5f;
    }

    // Update is called once per frame

    public void Comportamiento_Enemigo(){
        if(Vector3.Distance(transform.position, target.transform.position)> playerInSightRange){
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

        }
        else if(Vector3.Distance(transform.position, target.transform.position) <= 2f) {
            ani.SetBool("run", false);
            ani.SetBool("walk",false);
            if(enable_attack) {
                target.transform.GetComponent<Movimiento>().ChangeHp(attack);
                enable_attack = false;
                Invoke("ResetAttack", timeBetweenAttacks);
            }
        }
        else{
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation,3);
            ani.SetBool("walk",false);
            ani.SetBool("run",true);
            transform.Translate(Vector3.forward *VelocidadMovimiento*Time.deltaTime);
        }
    }

    private void ResetAttack() {
        enable_attack = true;
    }

    // void OnCollisionEnter(Collision hit)
    // {
    //     Debug.Log(hit.transform.GetComponent<Movimiento>() != null);
    //     if (hit.transform.GetComponent<Movimiento>() != null && enable_attack)
    //     {
    //         Debug.Log("el lobo atacó");
    //         hit.transform.GetComponent<Movimiento>().ChangeHp(attack);
    //         enable_attack = false;
    //     }
    // }
    public void Change_HP(float dmg)
    {
        hp -= dmg;
        if (hp < 0)
        {
            StopCoroutine(AttackDelay());
            target.GetComponent<Movimiento>().AddExp(exp);
            Destroy(gameObject);
        }
    }
    IEnumerator AttackDelay()
    {
        while(true)
        {
            if (!enable_attack)
            {
                Debug.Log("hola");
                yield return new WaitForSeconds(1.5f);
                enable_attack = true;
            }
            yield return null;
        }
    }
    void Update()
    {
        Comportamiento_Enemigo();
    }
}
