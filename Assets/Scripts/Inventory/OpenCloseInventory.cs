using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseInventory : MonoBehaviour {


    private bool inventarioAtivo;
    public GameObject invetario;


    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            InventarioManager();
        }
    }

    public void InventarioManager()
    {
        inventarioAtivo = !inventarioAtivo;
        invetario.SetActive(inventarioAtivo);
    }
}
