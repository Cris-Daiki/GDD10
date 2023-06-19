using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public PlayerData _playerdata;
    public Proyectile _bullet;
    public Transform bullet_spawner;
    public float hp, maxhp,def ,fireDelay = 1.5f ,dmg,VelocidadMovimiento =5.0f,
    VelocidadRotacion =200.0f, ImpulsodDeGolpe = 10f, x, y, fuerzasalto = 8f;
    public int current_exp, lvl = 1;
    private Animator anim;
    public Rigidbody rb;
    public bool puedoSaltar, EstoyAtacando, AvanzoSolo,enable_attack = true;


    void FillData()
    {
        hp =_playerdata.hp;
        maxhp = _playerdata.maxhp;
        def = _playerdata.def;
        fireDelay = _playerdata.fireDelay;
        dmg = _playerdata.dmg;
        lvl = _playerdata.level;
        current_exp = _playerdata.experience;
    }
    void Start()
    {
        anim = GetComponent<Animator>();   
        puedoSaltar = false;
        FillData();
        StartCoroutine(AttackDelay());

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
    public void AddExp(int exp_val)
    {
        current_exp += exp_val;
        if(lvl <= 15)
        {
            if(current_exp >= (2*lvl+7))
            {
                current_exp -= (2*lvl+7);
                lvl++;
            }
        }
        else if(lvl > 16 && lvl <= 30)
        {
            if (current_exp >= (5 * lvl -38))
            {
                current_exp -= (5 * lvl -38);
                lvl++;
            }
        }
        else if(lvl > 30)
        {
            if (current_exp >= (9 * lvl -158))
            {
                current_exp -= (9 * lvl - 158);
                lvl++;
            }
        }
        else
        {
            Die();
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
    public void Die()
    {
        _playerdata.lives -= 1;
        Destroy(gameObject);
    }
    public void ChangeHp(float dmg)
    {
        hp = hp - (dmg - (dmg * (def/100.0f)));
        if(hp < 0)
        {
            StopCoroutine(AttackDelay());
            Destroy(gameObject);
        }
    }
    IEnumerator AttackDelay()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
            {
                SpawnBullet();
                yield return new WaitForSeconds(fireDelay);
            }
            yield return null;
        }
    }
    void SpawnBullet()
    {
        Vector3 S_position = bullet_spawner.position;
        Quaternion S_rotation = bullet_spawner.rotation;
        var Spawned_bullet = Instantiate(_bullet.bullet_prefab, S_position, S_rotation);
        Physics.IgnoreCollision(Spawned_bullet.transform.GetComponent<SphereCollider>(),transform.GetComponent<CapsuleCollider>());
        /*
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            print("raycast hit");
            Vector3 bulletDirection = hit.point - bullet_spawner.position;
            Spawned_bullet.GetComponent<Rigidbody>().velocity = bulletDirection*_bullet.bullet_velocity;
        }
        else
        {
            print("raycast no hit");
            Vector3 bulletDirection = (Camera.main.transform.position + Camera.main.transform.forward * 1000) - bullet_spawner.position;
            Spawned_bullet.GetComponent<Rigidbody>().velocity = bulletDirection * _bullet.bullet_velocity;
        }
        */
        Spawned_bullet.GetComponent<Rigidbody>().AddForce(bullet_spawner.transform.forward * _bullet.bullet_velocity, ForceMode.Impulse);
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
    /*
     private void Increase_EXP(int Exp_Gained)
    {
        var e = Exp_Gained + Current_Exp;
        if (e >= 150)
        {
            Current_Exp = e - 1000;
            Player_Lvl++;
            Skills_Points += 2;
            MaxHP += 10;
            HitPoints = MaxHP;
            Hp_bar.Update_healt();
            HP_Counter.text = MaxHP.ToString();
        }
        else
        {
            Current_Exp += Exp_Gained;
        }
        
    }
     */
}
