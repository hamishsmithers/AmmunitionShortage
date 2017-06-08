using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {

	//controller is a reference to the XInput controller this player will use
	public XboxController controller;

	public GameObject head;

	public GameObject barrel;

	public GameObject firingBulletPrefab;

	public float bulletSpeed;

	//speed is a reference to how fast this player moves;
	public float speed;
	//rotationSpeedX is a reference to how fast this player rotates on the X axis;
	public float rotationSpeedX;
	//rotationSpeedY is a reference to how fast this player rotates on the Y axis;
	public float rotationSpeedY;
	//maxSpeed is a reference to the fastest this character can move
	public float maxSpeed;

	public float minVerticalRotation;
	public float maxVerticalRotation;


	private float rotationY = 0f;
	private float moveX;
	private float moveY;
	private float aimX;
	private float aimY;

	//rigidBody is a reference to this player's rigidbody
	private Rigidbody rigidBody;

	//----------------------------------------------------------------------------------------------------
	//Start()
	//	Called on instantiation. Instantiate this instance.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Move ();
		Fire ();
	}


	private void Fire(){
		if (XCI.GetButtonDown (XboxButton.RightBumper, controller)) {
			GameObject GO = Instantiate (firingBulletPrefab, barrel.transform.position+(barrel.transform.forward/5), barrel.transform.rotation) as GameObject;
			GO.GetComponent<Rigidbody> ().AddForce (barrel.transform.forward * bulletSpeed, ForceMode.Impulse);
		}
	}

	private void Move(){
		moveX = XCI.GetAxis (XboxAxis.LeftStickX, controller);
		moveY = XCI.GetAxis (XboxAxis.LeftStickY, controller);
		aimX = XCI.GetAxis (XboxAxis.RightStickX, controller);
		aimY = XCI.GetAxis (XboxAxis.RightStickY, controller);

		rigidBody.AddForce (transform.forward *moveY * speed);
		rigidBody.AddForce (transform.right * moveX * speed);
		if (rigidBody.velocity.magnitude > maxSpeed) {
			rigidBody.velocity = rigidBody.velocity.normalized*maxSpeed;
		}

		transform.Rotate (0, aimX * rotationSpeedX, 0);

		rotationY +=aimY * rotationSpeedY;
		rotationY = Mathf.Clamp (rotationY, minVerticalRotation, maxVerticalRotation);

		head.transform.localEulerAngles = new Vector3(-rotationY,0,0);


	}

}
