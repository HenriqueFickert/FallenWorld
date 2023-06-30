using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPCanvas : MonoBehaviour {

    public Slider inimigoSlider;
    public Enemies inimigoScript;

	void Start () {
		
	}

	void Update () {
        inimigoSlider.maxValue = inimigoScript.vidaMax;
        inimigoSlider.value = inimigoScript.vidaAtual;
	}
}
