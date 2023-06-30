using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private bool estaPausado;
    public GameObject menuPause;

	void Start () {
		
	}
	

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            estaPausado = !estaPausado;
        }
        if (estaPausado)
        {
            menuPause.SetActive(true);
            Time.timeScale = 0;
        }else
        {
            menuPause.SetActive(false);
            Time.timeScale = 1;
        }
	}

    public void Resume()
    {
        estaPausado = false;
    }

    public void ReloadGame()
    {
        SceneManager.GetActiveScene(); SceneManager.LoadScene("Jogo");
        Time.timeScale = 1;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Sair()
    {
        Application.Quit();
    }
}
