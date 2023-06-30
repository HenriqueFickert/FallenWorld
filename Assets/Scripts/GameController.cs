using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int stones = 0;

    public bool bossDead = false;

    public GameObject menuVitoria;

    public GameObject[] gemImages;

	void Update () {
		if (stones == 3 && bossDead)
        {
            menuVitoria.SetActive(true);
            Time.timeScale = 0;
        }


	}

    public void PegaIndex(int valor) {
        gemImages[valor].SetActive(true);
    }
}
