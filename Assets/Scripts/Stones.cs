using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stones : MonoBehaviour {

    private GameController gameScript;
    public int index;

    private void Start()
    {
        gameScript = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameScript.stones++;
            gameScript.PegaIndex(index);
            Destroy(gameObject);
        }
    }
}
