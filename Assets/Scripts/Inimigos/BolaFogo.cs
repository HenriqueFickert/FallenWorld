using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFogo : MonoBehaviour {

    public GameObject particula;

    private Transform playerT;
    public Dragon dragonScript;

    void Start()
    {
        playerT = GameObject.Find("Player").GetComponent<Transform>();
        Vector3 posFinal = new Vector3(playerT.position.x, playerT.position.y + 1f, playerT.position.z);
        transform.LookAt(posFinal);
    }


    void Update()
    {
            transform.Translate(Vector3.forward * 15 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dragonScript != null)
            {
                dragonScript.AtaqueDist();
            }
        }
        Instantiate(particula, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
