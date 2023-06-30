using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotoesStatus : MonoBehaviour {

    private PlayerStats playerStatsScript;

    void Start () {
        playerStatsScript = FindObjectOfType<PlayerStats>();
    }


    public void UpHp()
    {
        if (playerStatsScript.pontosLevel > 0 && playerStatsScript.vidaPontos < 60)
        {
            playerStatsScript.pontosLevel--;
            playerStatsScript.vidaStatus+=12;
            playerStatsScript.vidaAtual += 12;
            playerStatsScript.vidaPontos++;
        }
    }

    public void UpDano()
    {
        if (playerStatsScript.pontosLevel > 0 && playerStatsScript.danoPontos < 60)
        {
            playerStatsScript.pontosLevel--;
            playerStatsScript.danoStatus += 3;
            playerStatsScript.danoPontos++;
        }
    }

    public void UpSpeed()
    {
        if (playerStatsScript.pontosLevel > 0 && playerStatsScript.atSpeedPontos < 60)
        {
            playerStatsScript.pontosLevel--;
            playerStatsScript.speedStatus += 1f;
            playerStatsScript.atSpeedPontos++;
        }
    }
}
