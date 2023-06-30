using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{

    NavMeshAgent agent;
    private int running;

    private Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //faz o player seguir o objeto de intercao e rotacionar para sempre olhar o objeto
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    //faz o player andar
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    //Faz o player seguir ate um objeto para interagir
    public void FollowTargert(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 0.8f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
    }

    //Faz o player parar de seguir o objeto 
    public void StopFollowingTargert()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    //Mantem o jogador olhando para o objeto de intercao se ele estiver dentro do raio de interacao
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

}
