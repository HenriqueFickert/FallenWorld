using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{

    private PlayerStats playerStatsScript;

    public Slider hpSlider;
    public Text hpText;

    public Text lvlText;
    public Slider expSlider;
    public Text expText;
    public Text pontosLevel;

    public Text vidaInvetText;
    public Text danoInventTex;
    public Text attackSInventText;

    public Text pontosVida;
    public Text pontosDano;
    public Text pontosASpeed;

    private int contador = 0;
    public Image[] buffsImages;
    Vector3 buffPos0;
    Vector3 distanciaPos;


    void Start()
    {
        playerStatsScript = FindObjectOfType<PlayerStats>();
        buffPos0 = buffsImages[0].transform.position;
        distanciaPos = (buffsImages[1].transform.position - buffPos0);

        for (int i = 0; i < buffsImages.Length; i++)
        {
            buffsImages[i].enabled = false;
        }
    }


    void Update()
    {


        expSlider.maxValue = playerStatsScript.expMaxima;
        expSlider.value = playerStatsScript.expAtual;
        expText.text = Mathf.Floor(playerStatsScript.expAtual) + " / " + Mathf.Floor(playerStatsScript.expMaxima);
        lvlText.text = playerStatsScript.levelAtual.ToString();
        pontosLevel.text = "Pontos: " + playerStatsScript.pontosLevel;


        hpText.text = playerStatsScript.vidaAtual + " / " + playerStatsScript.vidaMaxima;
        hpSlider.value = playerStatsScript.vidaAtual;
        hpSlider.maxValue = playerStatsScript.vidaMaxima;



        pontosVida.text = playerStatsScript.vidaPontos.ToString();
        pontosDano.text = playerStatsScript.danoPontos.ToString();
        pontosASpeed.text = playerStatsScript.atSpeedPontos.ToString();

        vidaInvetText.text = "Hp Base: " + playerStatsScript.vidaBase + " + Stats: " + playerStatsScript.vidaStatus + " + Buff: " + playerStatsScript.vidaPocao;
        danoInventTex.text = "Dmg Base : " + playerStatsScript.danoBase + " + Stats: " + playerStatsScript.danoStatus + " + Buff: " + playerStatsScript.danoPocao;
        attackSInventText.text = "VAtq Base: " + playerStatsScript.speedBase + " + Stats: " + playerStatsScript.speedStatus + "  + Buff: " + (playerStatsScript.speedPocao - 1) * 100;

        // buffs
        contador = 0;
        if (playerStatsScript.estaAtivoPocaoHpMax)
        {
            buffsImages[0].enabled = true;
            buffsImages[0].transform.position = buffPos0;
            contador++;
        }
        else
        {
            buffsImages[0].enabled = false;
        }

        if (playerStatsScript.estaAtivoPotionDano)
        {
            buffsImages[1].enabled = true;
            buffsImages[1].transform.position = buffPos0 + distanciaPos * contador;
            contador++;
        }
        else
        {
            buffsImages[1].enabled = false;
        }

        if (playerStatsScript.estaAtivoPocaoSpeed)
        {
            buffsImages[2].enabled = true;
            buffsImages[2].transform.position = buffPos0 + distanciaPos * contador;
            contador++;
        }
        else
        {
            buffsImages[2].enabled = false;
        }


    }




}
