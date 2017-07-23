using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraFollow : NetworkBehaviour {

	public GameObject target;
	public float smoothing = 5f;

	Vector3 offset;

	void Start()
	{
		offset = transform.position;
	}

	void FixedUpdate()
	{
        Vector3 targetCamPos = target.transform.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}

    public void setTarget(GameObject gameobject)
    {
        target = gameobject;
    }
}
