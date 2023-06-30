using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject item;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnItem() {
        Vector3 playerPos = new Vector3(player.position.x + Random.Range(-4,4), player.position.y, player.position.z + Random.Range(-4, 4));
        Instantiate(item, playerPos, Quaternion.identity);
    }

}
