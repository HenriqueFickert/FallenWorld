using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {

    private Camera cam;

	void Start () {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}
	

	void Update () {
        transform.LookAt(cam.transform.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	}
}
