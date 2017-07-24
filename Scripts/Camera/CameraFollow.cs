using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject target = GameObject.FindGameObjectWithTag("Player");
	public float smoothing = 5f;

	Vector3 offset;

	void Start()
	{
		offset = transform.position - target.transform.position;
	}

	void FixedUpdate()
	{
        Vector3 targetCamPos = target.transform.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
