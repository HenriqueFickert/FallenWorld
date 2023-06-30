using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthPotion : MonoBehaviour {

    private PlayerStats playerStatsScript;

    void Start()
    {
        playerStatsScript = FindObjectOfType<PlayerStats>();
    }

    public void UsePotion()
    {
        playerStatsScript.PotionHpMax();
        Destroy(gameObject);
    }

}
