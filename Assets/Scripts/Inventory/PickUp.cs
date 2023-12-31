﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    Inventory inventory;
    public GameObject itemButton;

	void Start () {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (!inventory.isFull[i]) {

                    inventory.isFull[i] = true;
                    Instantiate(itemButton,inventory.slots[i].transform,false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
