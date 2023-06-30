using UnityEngine;
using UnityEngine.AI;

public class Teia : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Aranha"))
        {
            NavMeshAgent agent = other.transform.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.speed = 6f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NavMeshAgent agent = other.transform.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.speed = 1.5f;
            }
        }

        if (other.CompareTag("Aranha"))
        {
            NavMeshAgent agent = other.transform.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.speed = 6f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NavMeshAgent agent = other.transform.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.speed = 5f;
            }
        }

        if (other.CompareTag("Aranha"))
        {
            NavMeshAgent agent = other.transform.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.speed = 3f;
            }
        }
    }

}
