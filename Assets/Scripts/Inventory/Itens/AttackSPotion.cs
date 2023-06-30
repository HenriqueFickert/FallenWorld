using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSPotion : MonoBehaviour {

    private PlayerStats playerStatsScript;

    void Start()
    {
        playerStatsScript = FindObjectOfType<PlayerStats>();
    }

    public void UsePotion()
    {
        playerStatsScript.PotionSpeed();
        Destroy(gameObject);
    }
}
