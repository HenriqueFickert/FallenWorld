using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemiAttackFireSet : MonoBehaviour {
    private PlayerCombat playerCombat;
    private  float tempo;
    private void Start()
    {
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tempo += Time.deltaTime;
            if (tempo > 0.05f)
            {
                playerCombat.TomaDano(3);
                tempo = 0;
            }
           
        }
    }
}
