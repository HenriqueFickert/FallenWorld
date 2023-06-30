using UnityEngine;

public class Enemies : Interactable
{
    PlayerCombat playerCombat;

    public float vidaAtual;
    public float vidaMax;
    public int danoAtaque;
    public int levelMonstro;
    public int vidaBase = 30;
    public int danoBase = 1;
    public int expMonstro;
    public int expMonstroBase = 40;

    public GameObject[] itens;

    public bool minionDeSpawn;

    public int levelMinionNdeSpawn;

    private SpawnPoint spawnPointScript;
    private bool estaMorto;

    private void Awake()
    {

        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
    }

    private void Start()
    {
        //playerCombat = FindObjectOfType<PlayerCombat>();

        if (minionDeSpawn)
        {
            spawnPointScript = GetComponentInParent<SpawnPoint>();
            levelMonstro = spawnPointScript.levelArea;
        }else
        {
                levelMonstro = levelMinionNdeSpawn;
        }

        if (this.gameObject.name == "Spider")
        {
            vidaMax = vidaBase + (levelMonstro * 2);
            danoAtaque = danoBase + (levelMonstro / 3);
            expMonstro = expMonstroBase + (levelMonstro * 2);
        }
        else if (this.gameObject.name == "Skeleton")
        {
            vidaMax = vidaBase + (levelMonstro * 3);
            danoAtaque = danoBase + (levelMonstro / 2);
            expMonstro = expMonstroBase + (levelMonstro * 3);
        }
        else if (this.gameObject.name == "Lizzard")
        {
            vidaMax = vidaBase + (levelMonstro * 4);
            danoAtaque = danoBase + (levelMonstro / 2);
            expMonstro = expMonstroBase + (levelMonstro * 3);
        }
        else if (this.gameObject.name == "Ente")
        {
            vidaMax = vidaBase + (levelMonstro * 6);
            danoAtaque = danoBase + (levelMonstro / 1);
            expMonstro = expMonstroBase + (levelMonstro * 6);
        }
        else if (this.gameObject.name == "Dragon")
        {
            vidaMax = vidaBase + (levelMonstro * 6);
            danoAtaque = danoBase + (levelMonstro / 2);
            expMonstro = expMonstroBase + (levelMonstro * 6);
        }
        if (this.gameObject.name == "GiantSpider")
        {
            vidaMax = vidaBase + (levelMonstro * 5);
            danoAtaque = danoBase + (levelMonstro / 1);
            expMonstro = expMonstroBase + (levelMonstro * 6);
        }

        vidaAtual = vidaMax;
    }

    public override void Interact()
    {
        if (vidaAtual > 0)
        {
            playerCombat.inimigoScript = GetComponent<Enemies>();
            playerCombat.Ataque();
        }
    }

    private new void Update()
    {
        base.Update();

        if (minionDeSpawn && spawnPointScript != null && !estaMorto)
        {
            if (this.gameObject.name == "Spider")
            {
                if (vidaAtual <= 0)
                {
                    spawnPointScript.atualQuatidadeSpider--;
                    estaMorto = true;
                }
            }
            else if (this.gameObject.name == "Skeleton")
            {
                if (vidaAtual <= 0)
                {
                    spawnPointScript.atualQuatidadeSkeleton--;
                    estaMorto = true;
                }
            }
            else if (this.gameObject.name == "Lizzard")
            {
                if (vidaAtual <= 0)
                {
                    spawnPointScript.atualQuatidadeLizzard--;
                    estaMorto = true;
                }
            }
            else if (this.gameObject.name == "Ente")
            {
                if (vidaAtual <= 0)
                {
                    spawnPointScript.atualQuatidadeEnt--;
                    estaMorto = true;
                }
            }
            else if (this.gameObject.name == "GiantSpider")
            {
                if (vidaAtual <= 0)
                {
                    spawnPointScript.atualQuatidadeGiantSpider--;
                    estaMorto = true;
                }
            }
        }
    }

    public void TomarDano(int dano)
    {
        vidaAtual -= dano;
    }



    public void DropItens(Vector3 pos)
    {
        Vector3 posF = new Vector3(Random.Range(1, 3), transform.position.y, Random.Range(1, 3)) + pos;
        if (Random.Range(1, 100) < 20)
        {
            Instantiate(itens[0], posF, Quaternion.identity);
        }
        posF = new Vector3(Random.Range(1, 3), transform.position.y, Random.Range(1, 3)) + pos;
        if (Random.Range(1, 100) < 10)
        {
            Instantiate(itens[1], posF, Quaternion.identity);
        }
        posF = new Vector3(Random.Range(1, 3), transform.position.y, Random.Range(1, 3)) + pos;
        if (Random.Range(1, 100) < 10)
        {
            Instantiate(itens[2], posF, Quaternion.identity);
        }
        posF = new Vector3(Random.Range(1, 3), transform.position.y, Random.Range(1, 3)) + pos;
        if (Random.Range(1, 100) < 10)
        {
            Instantiate(itens[3], posF, Quaternion.identity);
        }

    }
}
