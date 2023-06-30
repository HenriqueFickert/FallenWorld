using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonoBehaviour {

    private GameController gameScript;

    private NavMeshAgent agent;
    public enum Estado { Patrulha = 1, Seguindo, Atacando, AtacandoBolaDeFogo, AtacandoFlames, Morto }
    public Estado estado;
    const float smooth = 0.1f;

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
    public float raioDeAtaqueBolaDeFogo = 7f;
    public float raioDeAtaque = 2f;
    public BoxCollider colliderAttack;
    public BoxCollider colliderAttackFogo;
    private float tempoAtaqueFogo;
    public GameObject coneFogo;
    public GameObject bolaFogo;
    public Transform posicaoFogo;


    //Status
    private Enemies enemiesScript;
    private float timer;
    private bool estaMorto;
    public int expMonstro;
    private CapsuleCollider colliderDragon;

    //Animacao
    private Animator anim;

    //Canvas
    public GameObject hpBar;

    void Start()
    {
        gameScript = GameObject.Find("GameController").GetComponent<GameController>();
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        alvo = GameObject.Find("Player").GetComponent<Transform>();
        enemiesScript = GetComponent<Enemies>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        colliderDragon = GetComponent<CapsuleCollider>();
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
            case Estado.AtacandoBolaDeFogo:
                AtaqueBolaFogo();
                break;
            case Estado.AtacandoFlames:
                AtaqueFogo();
                break;
            case Estado.Morto:
                Morto();
                break;
        }

        float speedPercent = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speedPercent, smooth, Time.deltaTime);

        if (!estaMorto)
        {
            float distanciaParaOJogador = Vector3.Distance(alvo.position, transform.position);

            if (distanciaParaOJogador > raioDeAtaqueBolaDeFogo)
            {
                estado = Estado.Patrulha;
            }
            else if (distanciaParaOJogador > raioDeVisao && distanciaParaOJogador <= raioDeAtaqueBolaDeFogo)
            {
                estado = Estado.AtacandoBolaDeFogo;
            }
            else if (distanciaParaOJogador <= raioDeVisao && distanciaParaOJogador > raioDeAtaque && estado != Estado.AtacandoFlames)
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
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsAttackingFireBall", false);
        anim.SetBool("IsAttackingFire", false);

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
        anim.SetBool("IsAttackingFireBall", false);
        anim.SetBool("IsAttackingFire", false);

        Ray ray = new Ray(transform.position, alvo.transform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raioDeVisao, layerPlayer))
        {
            agent.SetDestination(alvo.transform.position);
            Debug.DrawLine(ray.origin, hit.point);
        }
        else
        {
            estado = Estado.Patrulha;
        }

        tempoAtaqueFogo += Time.deltaTime;
        if (tempoAtaqueFogo > 2)
        {
            estado = Estado.AtacandoFlames;
            tempoAtaqueFogo = 0;
        }


    }


    void Atacando()
    {
        anim.SetLayerWeight(1, 1.0f);
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsAttacking", true);
        anim.SetBool("IsAttackingFire", false);
        anim.SetBool("IsAttackingFireBall", false);
    }

    void AtaqueBolaFogo()
    {
        agent.isStopped = true;
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsAttackingFire", false);
        anim.SetBool("IsAttackingFireBall", true);
    }

    void AtaqueFogo()
    {
        anim.SetLayerWeight(1, 0f);
        agent.isStopped = true;
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsAttackingFireBall", false);
        anim.SetBool("IsAttackingFire", true);
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
            anim.SetBool("IsAttackingFireBall", false);
            anim.SetBool("IsAttackingFire", false);
            anim.SetBool("IsDead", true);

            estaMorto = true;
            agent.enabled = false;
            colliderDragon.enabled = false;
            enemiesScript.DropItens(transform.position);
            playerCombat.Experiencia(enemiesScript.expMonstro);
            gameScript.bossDead = true;
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

    public void AnimAtaqueBolaDeFogo()
    {
        bolaFogo.GetComponent<BolaFogo>().dragonScript = GetComponent<Dragon>();
        Instantiate(bolaFogo, new Vector3 (posicaoFogo.position.x - 0.87f,posicaoFogo.position.y, posicaoFogo.position.z), Quaternion.identity);
    }

    public void AtaqueDist() //Puxa na lanca
    {
        if (playerCombat != null)
        {
            playerCombat.TomaDano(enemiesScript.danoAtaque);
        }
    }

    public void animFogo()
    {
       colliderAttackFogo.enabled = true;
       GameObject clone = Instantiate(coneFogo, posicaoFogo.position, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - 20));
       clone.transform.SetParent(posicaoFogo);
    }


    public void CancelaAnimFogo()
    {
        colliderAttackFogo.enabled = false;
        estado = Estado.Patrulha;
        anim.SetBool("IsAttackingFire", false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDeVisao);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raioDeAtaqueBolaDeFogo);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raioDeAtaque);
    }

}
