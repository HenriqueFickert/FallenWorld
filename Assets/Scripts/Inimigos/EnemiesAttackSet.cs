using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttackSet : MonoBehaviour {

    private PlayerCombat playerCombat;
    private Enemies enemiesScript;

    private void Start()
    {
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        enemiesScript = GetComponentInParent<Enemies>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCombat.TomaDano(enemiesScript.danoAtaque);
        }
    }
}
