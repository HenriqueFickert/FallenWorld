using UnityEngine;
using UnityEngine.AI;

public class Lizzard : MonoBehaviour
{

    private NavMeshAgent agent;
    public enum Estado { Patrulha = 1, Seguindo, Atacando, AtacandoDistancia, Fugindo, Morto }
    public Estado estado;

    //Patrulha
    public Transform[] alvosDePatrulha;
    private bool estaPatrulhando;
    private int destPoint;

    //Seguindo
    public float raioDeVisao = 10;
    private Transform alvo;
    public LayerMask layerPlayer;

    //Ataque
    private PlayerCombat playerCombat;
    public float raioDeAtaqueD = 7f;
    public float raioDeAtaque = 2f;
    public Transform lancaPosition;
    public GameObject lanca;
    private BoxCollider colliderAttack;

    //Fuga
    public float raioDeFuga = 5f;
    private bool correndo;
    private float tempoCorrendo;

    //Status
    private Enemies enemiesScript;
    private float timer;
    private bool estaMorto;
    public int expMonstro;
    private CapsuleCollider colliderLizzard;

    //Animacao
    private Animator anim;

    //Canvas
    public GameObject hpBar;



    void Start()
    {
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        alvo = GameObject.Find("Player").GetComponent<Transform>();
        enemiesScript = GetComponent<Enemies>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        colliderLizzard = GetComponent<CapsuleCollider>();
        colliderAttack = GetComponentInChildren<BoxCollider>();
    }

    void Update()
    {
        //Estados
        switch (estado)
        {
            case Estado.Patrulha:
                Patrulhando();
                break;
            case Estado.Seguindo:
                Seguindo();
                break;
            case Estado.Atacando:
                Atacando();
                break;
            case Estado.AtacandoDistancia:
                AtacandoDistancia();
                break;
            case Estado.Fugindo:
                Fugindo();
                break;
            case Estado.Morto:
                Morto();
                break;
        }


        if (!estaMorto)
        {
            float distanciaParaOJogador = Vector3.Distance(alvo.position, transform.position);

            if (distanciaParaOJogador > raioDeVisao)
            {
                estado = Estado.Patrulha;
            }
            else if (distanciaParaOJogador <= raioDeVisao && distanciaParaOJogador > raioDeAtaqueD)
            {
                estado = Estado.Seguindo;
            } /*
            else if (distanciaParaOJogador <= raioDeFuga && (enemiesScript.vidaAtual / enemiesScript.vidaMax) <= (25.0f / 100.0f) && !correndo)
            {
                OlhaOJogador();
                estado = Estado.Fugindo;
            }*/
            else if (distanciaParaOJogador > raioDeAtaque && distanciaParaOJogador <= raioDeAtaqueD && estado != Estado.Fugindo)
            {
                OlhaOJogador();
                estado = Estado.AtacandoDistancia;
            }
            else if (distanciaParaOJogador <= raioDeAtaque /*&& (enemiesScript.vidaAtual / enemiesScript.vidaMax) > (25.0f / 100.0f) && estado != Estado.Fugindo*/)
            {
                OlhaOJogador();
                estado = Estado.Atacando;
            }

        }


        //Fugindo
        /*
        if (estado == Estado.Fugindo)
        {
            tempoCorrendo += Time.deltaTime;
            if (tempoCorrendo >= 5)
            {
                correndo = true;
                tempoCorrendo = 0;
            }
        }

        if (correndo)
        {
            tempoCorrendo += Time.deltaTime;
            if (tempoCorrendo >= 3.5f)
            {
                correndo = false;
                tempoCorrendo = 0;
            }
        }*/
        //Ate aqui

        if (enemiesScript.vidaAtual <= 0)
        {
            estado = Estado.Morto;
        }
    }

    void Patrulhando()
    {
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsThrowingSpear", false);

        if (alvosDePatrulha.Length > 1)
        {
            agent.isStopped = false;
            anim.SetBool("IsWalking", true);
            //Anim Ataq False
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                agent.destination = alvosDePatrulha[destPoint].position;
                destPoint = (destPoint + 1) % alvosDePatrulha.Length;
            }
        }
        else
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                agent.isStopped = true;
                anim.SetBool("IsWalking", false);
            }
            else
            {
                agent.isStopped = false;
                anim.SetBool("IsWalking", true);

                agent.destination = alvosDePatrulha[destPoint].position;
            }
        }
    }


    void Seguindo()
    {

        agent.isStopped = false;
        anim.SetBool("IsWalking", true);
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsThrowingSpear", false);

        Ray ray = new Ray(transform.position, alvo.transform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 25, layerPlayer))
        {
            agent.SetDestination(alvo.transform.position);
            Debug.DrawLine(ray.origin, hit.point);
        }
        else
        {
            estado = Estado.Patrulha;
        }

    }


    void Atacando()
    {
        anim.SetLayerWeight(1, 1.0f);
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsAttacking", true);
        anim.SetBool("IsThrowingSpear", false);
    }

    void AtacandoDistancia()
    {
        agent.isStopped = true;
        anim.SetLayerWeight(1, 1.0f);
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsThrowingSpear", true);
    }

    void Fugindo()
    {
        /*
        agent.isStopped = false;
        agent.speed = 7f;
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsThrowingSpear", false);
        anim.SetBool("IsWalking", true);
        Vector3 runTo = transform.position + ((transform.position - alvo.position) * 2);
        agent.SetDestination(runTo);*/
    }

    void Morto()
    {
        timer += Time.deltaTime;
        if (timer >= 10)
        {
            Destroy(transform.parent.gameObject);
        }

        if (!estaMorto)
        {
            Destroy(hpBar);
            agent.isStopped = true;
            anim.SetLayerWeight(1, 0.0f);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsAttacking", false);
            anim.SetBool("IsDead", true);
            estaMorto = true;
            agent.enabled = false;
            colliderLizzard.enabled = false;
            enemiesScript.DropItens(transform.position);
            playerCombat.Experiencia(enemiesScript.expMonstro);
        }
    }

    void OlhaOJogador()
    {
        Vector3 direction = (alvo.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    public void AnimAtaque()
    {
        colliderAttack.enabled = true;
    }

    public void AnimCancelaAtaque()
    {
        colliderAttack.enabled = false;
    }

    public void AnimAtaqueD()
    {
        lanca.GetComponent<Lanca>().lizzardScript = GetComponent<Lizzard>();
        Instantiate(lanca, lancaPosition.position, Quaternion.identity);
    }

    public void AtaqueDist() //Puxa na lanca
    {
        if (playerCombat != null)
        {
            playerCombat.TomaDano(enemiesScript.danoAtaque);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDeVisao);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raioDeAtaqueD);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raioDeAtaque);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, raioDeFuga);
    }

}
