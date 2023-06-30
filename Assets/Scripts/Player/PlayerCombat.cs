using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    private PlayerStats playerStatsScript;

    public Enemies inimigoScript;
    private CharacterAnimator animScript;

    void Start()
    {
        playerStatsScript = GetComponent<PlayerStats>();
        animScript = GetComponent<CharacterAnimator>();
    }


    public void Ataque()
    {
        if (playerStatsScript.vidaAtual > 0)
        {
            if (inimigoScript != null)
            {
                animScript.Attacking();
            }
        }
    }

    public void TomaDano(int dano)
    {
        if (playerStatsScript.vidaAtual > 0)
        {
            playerStatsScript.vidaAtual -= dano;
        }
        else
        {
            playerStatsScript.vidaAtual = 0;
        }
    }

    public void Experiencia(int exp)
    {
        playerStatsScript.expAtual += exp;
    }

    public void DanoInimigoAnim()
    {
        inimigoScript.TomarDano(playerStatsScript.danoJogador);
    }

}

