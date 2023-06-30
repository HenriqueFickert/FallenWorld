using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{

    private NavMeshAgent agent;
    public enum Estado { Patrulha = 1, Seguindo, Atacando, Morto }
    public Estado estado;

    //Patrulha
    public Transform[] alvosDePatrulha;
    private bool estaPatrulhando;
    private int destPoint;

    //Seguindo
    public float raioDeVisao = 5;
    private Transform alvo;
    public LayerMask layerPlayer;

    //Ataque
    private PlayerCombat playerCombat;
    public float raioDeAtaque = 1.8f;
    private BoxCollider colliderAttack;

    //Status Ente
    private Enemies enemiesScript;
    private float timer;
    private bool estaMorto;
    public int expMonstro;
    private CapsuleCollider colliderSkeleton;

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
        colliderSkeleton = GetComponent<CapsuleCollider>();
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
            else if (distanciaParaOJogador <= raioDeVisao && distanciaParaOJogador > raioDeAtaque)
            {
                estado = Estado.Seguindo;
            }
            else if (distanciaParaOJogador <= raioDeAtaque)
            {
                OlhaOJogador();
                estado = Estado.Atacando;
            }
        }


        if (enemiesScript.vidaAtual <= 0)
        {
            estado = Estado.Morto;
        }
    }

    void Patrulhando()
    {
        if (alvosDePatrulha.Length > 1)
        {
            agent.isStopped = false;
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsAttacking", false);
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
                anim.SetBool("IsAttacking", false);
            }
            else
            {
                agent.isStopped = false;
                anim.SetBool("IsWalking", true);
                anim.SetBool("IsAttacking", false);
                agent.destination = alvosDePatrulha[destPoint].position;
            }
        }
    }


    void Seguindo()
    {
        agent.isStopped = false;
        anim.SetBool("IsWalking", true);
        anim.SetBool("IsAttacking", false);

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
            colliderSkeleton.enabled = false;
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
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDeVisao);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raioDeAtaque);
    }

}
