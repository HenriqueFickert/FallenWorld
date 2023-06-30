using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePotion : MonoBehaviour
{

    private PlayerStats playerStatsScript;

    void Start()
    {
        playerStatsScript = FindObjectOfType<PlayerStats>();
    }

    public void UsePotion()
    {
        playerStatsScript.PotionDano();
        Destroy(gameObject);
    }
}
