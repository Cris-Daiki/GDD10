using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity_Controller : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject Room_Prefab;
    public RoomEnemy RoomInfo;
    GameObject[] Spawns;
    [SerializeField] Transform p_reference;
    private void Start()
    {
        FindPlayer();
        
    }
    public void killplayer()
    {
        player.GetComponent<Movimiento>().Die();
    }
    private void Update()
    {
        if(player == null)
        {
            GameObject  dungeon_delete = GameObject.FindGameObjectWithTag("Dungeon");
            Destroy(dungeon_delete);
            foreach(Transform x in p_reference)
            {
                if(x != p_reference)
                {
                    Destroy(x.gameObject);
                }
            }
            Instantiate(Room_Prefab);
            FindPlayer();
            SpawnEnemies();
        }
    }
    void SpawnEnemies()
    {
        int n_spawns = Spawns.Length-1;
        foreach(var x in RoomInfo.enemigos)
        {
            Vector3 pos = Spawns[Random.Range(0, n_spawns)].transform.position;
            var temp2 = Instantiate(x);
            temp2.transform.parent = p_reference;
            temp2.transform.position = pos;
            
        }
    }
    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Spawns = GameObject.FindGameObjectsWithTag("Spawner");
    }
}
