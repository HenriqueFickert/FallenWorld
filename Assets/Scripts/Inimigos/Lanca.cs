using UnityEngine;

public class Lanca : MonoBehaviour
{

    private Transform playerT;
    public Lizzard lizzardScript;

    private float tempoAtual;
    private float tempoDestruir = 2;

    private bool atingiu;
    void Start()
    {
        playerT = GameObject.Find("Player").GetComponent<Transform>();
        Vector3 posFinal = new Vector3(playerT.position.x, playerT.position.y + 1f, playerT.position.z);
        transform.LookAt(posFinal);
    }


    void Update()
    {
        if (!atingiu)
        {
            transform.Translate(Vector3.forward * 15 * Time.deltaTime);
        }

        if (atingiu)
        {
            tempoAtual += Time.deltaTime;
            if (tempoAtual < 0.1f)
            {
                transform.Translate(Vector3.forward * 15 * Time.deltaTime);
            }
            else if (tempoAtual >= tempoDestruir)
            {
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (lizzardScript != null)
            {
                lizzardScript.AtaqueDist();
            }
            atingiu = true;
            tempoDestruir = 1f;
            transform.SetParent(other.transform);
        }
        if (other.CompareTag("Terreno"))
        {
            atingiu = true;
            transform.SetParent(other.transform);
        }
    }
}
