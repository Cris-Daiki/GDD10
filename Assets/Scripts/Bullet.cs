using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Proyectile bullet_properties;
    float b_dmg, b_vel, b_reach;
    Proyectile.BulletEffect[] b_effects;
    Vector3 initial_pos;
    Movimiento player;

    private void Start()
    {
        //b_dmg = bullet_properties.bullet_dmg;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movimiento>();
        b_dmg = player.dmg;
        b_vel = bullet_properties.bullet_velocity;
        b_effects = bullet_properties.bullet_effects;
        b_reach = bullet_properties.bullet_reach;
        initial_pos = transform.position;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, initial_pos) >= b_reach)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision hit)
    {
        print("Object hit = " + hit.gameObject);
        if (hit.transform.GetComponent<MovimientoEnemigo>() != null)
        {
            hit.transform.GetComponent<MovimientoEnemigo>().Change_HP(b_dmg);
        }
        Destroy(gameObject);
    }
}
