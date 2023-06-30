using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public int quatidadeSpider;
    public int quatidadeSkeleton;
    public int quatidadeLizzard;
    public int quatidadeEnt;
    public int quatidadeGiantSpider;

    public int atualQuatidadeSpider;
    public int atualQuatidadeSkeleton;
    public int atualQuatidadeLizzard;
    public int atualQuatidadeEnt;
    public int atualQuatidadeGiantSpider;

    public GameObject spider;
    public GameObject skeleton;
    public GameObject lizzard;
    public GameObject ent;
    public GameObject giantSpider;

    private float tempoAtualSpawnSpider;
    private float tempoAtualSpawnSkeleton;
    private float tempoAtualSpawnLizzard;
    private float tempoAtualSpawnEnt;
    private float tempoAtualSpawnGiantSpider;

    public float tempoSpawn;

    public int levelArea;
    public float areaSpawn;
    
    private void Awake()
    {
        for (int i = 0; i < quatidadeSpider; i++)
        {
            SpawEnemy(spider);
        }
        for (int i = 0; i < quatidadeSkeleton; i++)
        {
            SpawEnemy(skeleton);
        }
        for (int i = 0; i < quatidadeLizzard; i++)
        {
            SpawEnemy(lizzard);
        }
        for (int i = 0; i < quatidadeEnt; i++)
        {
            SpawEnemy(ent);
        }
        for (int i = 0; i < quatidadeGiantSpider; i++)
        {
            SpawEnemy(giantSpider);
        }
        atualQuatidadeEnt = quatidadeEnt;
        atualQuatidadeLizzard = quatidadeLizzard;
        atualQuatidadeSkeleton = quatidadeSkeleton;
        atualQuatidadeSpider = quatidadeSpider;
        atualQuatidadeGiantSpider = quatidadeGiantSpider;
    }


    void Update()
    {
        if ( atualQuatidadeSpider < quatidadeSpider)
        {
            tempoAtualSpawnSpider += Time.deltaTime;
            if (tempoAtualSpawnSpider > tempoSpawn)
            {
                SpawEnemy(spider);
                tempoAtualSpawnSpider = 0;
                atualQuatidadeSpider++;
            }
        }

        if (atualQuatidadeSkeleton < quatidadeSkeleton)
        {
            tempoAtualSpawnSkeleton += Time.deltaTime;
            if (tempoAtualSpawnSkeleton > tempoSpawn)
            {
                SpawEnemy(skeleton);
                tempoAtualSpawnSkeleton = 0;
                atualQuatidadeSkeleton++;
            }
        }

        if (atualQuatidadeLizzard < quatidadeLizzard)
        {
            tempoAtualSpawnLizzard += Time.deltaTime;
            if (tempoAtualSpawnLizzard > tempoSpawn)
            {
                SpawEnemy(lizzard);
                tempoAtualSpawnLizzard = 0;
                atualQuatidadeLizzard++;
            }
        }

        if (atualQuatidadeEnt < quatidadeEnt)
        {
            tempoAtualSpawnEnt += Time.deltaTime;
            if (tempoAtualSpawnEnt > tempoSpawn)
            {
                SpawEnemy(ent);
                tempoAtualSpawnEnt = 0;
                atualQuatidadeEnt++;
            }
        }

        if (atualQuatidadeGiantSpider < quatidadeGiantSpider)
        {
            tempoAtualSpawnGiantSpider+= Time.deltaTime;
            if (tempoAtualSpawnGiantSpider > tempoSpawn)
            {
                SpawEnemy(giantSpider);
                tempoAtualSpawnGiantSpider = 0;
                atualQuatidadeGiantSpider++;
            }
        }

    }


    void SpawEnemy(GameObject EnemyPrefab)
    {
        Vector3 RandomRespaw = new Vector3(this.transform.position.x + Random.Range(-areaSpawn, areaSpawn), this.transform.position.y, this.transform.position.z + Random.Range(-areaSpawn, areaSpawn));
        RaycastHit hit;

        if (Physics.Raycast(RandomRespaw, -Vector3.up, out hit))
        {
            if (hit.transform.CompareTag("Terreno"))
            {
                GameObject clone;
                clone = Instantiate(EnemyPrefab, RandomRespaw, Quaternion.Euler(0, Random.Range(0, 360), 0));
                clone.transform.parent = transform;
                clone.transform.position = new Vector3(clone.transform.position.x, hit.point.y + 20, clone.transform.position.z);
            }
            
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
       // Gizmos.DrawWireSphere(transform.position, areaSpawn);
        Gizmos.DrawWireCube(transform.position, new Vector3 (2*areaSpawn, areaSpawn, 2*areaSpawn));
    }
}
