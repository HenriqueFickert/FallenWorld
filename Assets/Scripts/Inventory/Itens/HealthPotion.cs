using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour {

    private PlayerStats playerStatsScript;

	void Start () {
        playerStatsScript = FindObjectOfType<PlayerStats>();
	}

    public void UsePotion(int valor) {
        if (playerStatsScript.vidaAtual > 0 && playerStatsScript.vidaAtual < playerStatsScript.vidaMaxima)
        {
            playerStatsScript.vidaAtual += valor;

            if (playerStatsScript.vidaAtual > playerStatsScript.vidaMaxima)
            {
                playerStatsScript.vidaAtual = playerStatsScript.vidaMaxima;

            }
            Destroy(gameObject);
        }
    }
}
