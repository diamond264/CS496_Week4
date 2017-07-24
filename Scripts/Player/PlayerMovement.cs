using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

    Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
    PlayerHealth playerhealth;
    bool isScoped;
    public GameObject scope;
    public Camera mainCamera;

    public float scopedFOV = 15f;
    private float normalFOV;

	int floorMask;
	float camRayLength = 100f;

    void Awake()
	{
        floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
    }

	void FixedUpdate()
	{
        if (Input.GetButton("Fire2"))
        {
            scope.SetActive(true);
            normalFOV = mainCamera.fieldOfView;
            mainCamera.fieldOfView = scopedFOV;
        }
        else
        {
            scope.SetActive(false);
            mainCamera.fieldOfView = normalFOV;
        }
        float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
        Move (h, v);
		Turning ();
		Animating (h, v);
	}

	void Move(float h, float v)
	{
		movement.Set (h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

    void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}
}
