using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {

	//controller is a reference to the XInput controller this player will use
	public XboxController controller;

	//head is a reference to the head of the player
	public GameObject head;

	//barrel is a reference to the barrel of the player's gun
	public GameObject barrel;

	//firingBulletPrefab is a reference to the bullet the player will fire
	public GameObject firingBulletPrefab;

	//bulletSpeed indicated how fast the bullets shot will move
	public float bulletSpeed;

	//currentAmmo tracks the player's current ammo
	public int currentAmmo = 2;



	//speed is a reference to how fast this player moves;
	public float speed;
	//rotationSpeedX is a reference to how fast this player rotates on the X axis;
	public float rotationSpeedX;
	//rotationSpeedY is a reference to how fast this player rotates on the Y axis;
	public float rotationSpeedY;
	//maxSpeed is a reference to the fastest this character can move
	public float maxSpeed;

	//minVerticalRotation is how far the player can aim up
	public float minVerticalRotation;
	//maxVerticalRotation is how far the player can aim down
	public float maxVerticalRotation;
	//rotationY tracks the player's current vertical rotation
	private float rotationY = 0f;

	//moveX stores the left joysticks X axis
	private float moveX;
	//moveY stores the left joysticks Y axis
	private float moveY;
	//moveX stores the Right joysticks X axis
	private float aimX;
	//moveX stores the Right joysticks Y axis
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
	
	//----------------------------------------------------------------------------------------------------
	//FixedUpdate()
	//	Called once per frame.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	void FixedUpdate () {

		Move ();
		Fire ();
	}

	//----------------------------------------------------------------------------------------------------
	//Fire()
	//	Called from update. Fires bullets if allowed.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	private void Fire(){
		if (XCI.GetButtonDown (XboxButton.RightBumper, controller) && currentAmmo>0) {
			//GO is the bullet the player has created
			GameObject GO = Instantiate (firingBulletPrefab, barrel.transform.position+(barrel.transform.forward/5), barrel.transform.rotation) as GameObject;
			GO.GetComponent<Rigidbody> ().AddForce (barrel.transform.forward * bulletSpeed, ForceMode.Impulse);
			currentAmmo--;
		}
	}

	//----------------------------------------------------------------------------------------------------
	//Move()
	//	Called from update. Moves the player.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
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

	//----------------------------------------------------------------------------------------------------
	//OnCollisionEnter()
	//	Called on collisions
	// 
	//	Param:
	//		Collision other - the other object that has been collided with
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "ShotBullet") {
			if (currentAmmo < 3) {
				Destroy (other.gameObject);
				currentAmmo++;
			}
		}
		if (other.gameObject.tag == "FiringBullet") {
			Destroy (this.gameObject);
		}
	}


}
