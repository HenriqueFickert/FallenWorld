using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    private Transform camTransform;
    public Vector3 offset;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float yawSpeed = 100;

    private float currentZoom = 10;
    public float pitch = 2;
    private float yawInput = 0;

	void Start () {
        camTransform = GetComponent<Transform>();
	}

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        if (Input.GetMouseButton(1))
        {
            yawInput += Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
        }
    }

    void LateUpdate () {
        camTransform.position = Vector3.Lerp(camTransform.position,target.position - offset * currentZoom, 15);
        camTransform.LookAt(target.position + Vector3.up * pitch);
        camTransform.RotateAround(target.position, Vector3.up, yawInput);
	}
}
