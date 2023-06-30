using UnityEngine;


public class PlayerStats : MonoBehaviour
{

    //Vida
    public int vidaAtual;
    public int vidaMaxima;
    public int vidaStatus;
    public int vidaBase = 100;
    public int vidaPocao;

    public int vidaPontos = 1;

    private float tempoRegenHP = 0;

    //Dano
    public int danoJogador;
    public int danoBase = 5;
    public int danoStatus;
    public int danoPocao = 0;

    public int danoPontos = 1;

    //Ataque Speed
    public float atqSpeed;
    public int speedBase = 1;
    public float speedStatus;
    public float speedPocao = 1;

    public int atSpeedPontos = 1;

    //Leveling
    public float expAtual;
    public float expMaxima;
    private float expExtra;
    public int levelAtual;
    public int pontosLevel;
    public int pontosRequesitos;

    //Pocao de Dano
    private float timerPotionDano;
    public bool estaAtivoPotionDano;

    //Pocao de HP Max
    private float timerPotionHpMax;
    public bool estaAtivoPocaoHpMax;

    //Pocao de Speed
    private float timerPotionSpeed;
    public bool estaAtivoPocaoSpeed;

    private void Start()
    {
        danoJogador = danoBase + danoStatus + danoPocao;
        vidaMaxima = vidaBase + vidaStatus + vidaPocao;
        atqSpeed = ((speedBase + speedStatus) * speedPocao);
        vidaAtual = vidaMaxima;
    }

    private void Update()
    {

        danoJogador = danoBase + danoStatus + danoPocao;
        vidaMaxima = vidaBase + vidaStatus + vidaPocao;
        atqSpeed = ((speedBase + (speedStatus / 20)) * speedPocao);

        //Pocao de Dano
        if (estaAtivoPotionDano)
        {
            if (timerPotionDano < 10)
            {
                danoPocao = 5;
                timerPotionDano += Time.deltaTime;
            }
            else
            {
                estaAtivoPotionDano = false;
                danoPocao = 0;
                timerPotionDano = 0;
            }
        }
        //Pocao de HP Max
        if (estaAtivoPocaoHpMax)
        {
            if (timerPotionHpMax < 10)
            {
                vidaPocao = 5;
                timerPotionHpMax += Time.deltaTime;
            }
            else
            {
                if (vidaAtual == vidaMaxima)
                {
                    vidaAtual -= 5;
                }
                estaAtivoPocaoHpMax = false;
                vidaPocao = 0;
                timerPotionHpMax = 0;
            }
        }
        //Pocao de Speed
        if (estaAtivoPocaoSpeed)
        {
            if (timerPotionSpeed < 10)
            {
                speedPocao = 1.2f;
                timerPotionSpeed += Time.deltaTime;
            }
            else
            {
                estaAtivoPocaoSpeed = false;
                speedPocao = 1;
                timerPotionSpeed = 0;
            }
        }


        //Regen de HP
        tempoRegenHP += Time.deltaTime;
        if (tempoRegenHP >= 15)
        {

            if (vidaAtual <= vidaMaxima - (3 + levelAtual / 2))
            {
                vidaAtual += (3 + levelAtual / 2);
            }
            else
            {
                vidaAtual += (vidaMaxima - vidaAtual);
            }

            tempoRegenHP = 0;
        }

        LevelUP();

        if (Input.GetKeyDown(KeyCode.M))
        {
            expAtual += 400;

        }
    }

    public void LevelUP()
    {
        if (expAtual >= expMaxima)
        {
            expExtra = expAtual - expMaxima;
            levelAtual++;
            expMaxima = expMaxima * 1.3f;
            expAtual = 0 + expExtra;
            pontosLevel += 5;
        }
    }


    public void PotionDano()
    {
        estaAtivoPotionDano = true;
        timerPotionDano = 0;
    }

    public void PotionHpMax()
    {
        estaAtivoPocaoHpMax = true;
        timerPotionHpMax = 0;
    }

    public void PotionSpeed()
    {
        estaAtivoPocaoSpeed = true;
        timerPotionSpeed = 0;
    }
}
