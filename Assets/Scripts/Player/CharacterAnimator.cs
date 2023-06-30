using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CharacterAnimator : MonoBehaviour
{
    const float smooth = 0.1f;

    private Animator animator;
    private NavMeshAgent agent;
    private PlayerStats playerStats;

    private CapsuleCollider colliderPlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        playerStats = GetComponent<PlayerStats>();
        colliderPlayer = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, smooth, Time.deltaTime);

        if (playerStats.vidaAtual <= 0)
        {
            animator.SetBool("EstaMorto", true);
            colliderPlayer.enabled = false;
        }

    }

    public void Attacking()
    {
        animator.SetFloat("SpeedAnimation", playerStats.atqSpeed);
        animator.SetLayerWeight(1, 1.0F);
        animator.SetTrigger("Attack");
    }


    public void AnimMorte()
    {
        SceneManager.LoadScene("Jogo");
    }

}
