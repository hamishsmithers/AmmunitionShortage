using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringBullet : MonoBehaviour {

	//canBreak is whether or not the bullet can break
	private bool canBreak = true;

	//shotBulletPrefab is the prefab of the shot bullet that will take it's place
	public GameObject shotBulletPrefab;

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
		if (canBreak) {
			Instantiate (shotBulletPrefab, transform.position, Quaternion.identity);
			Destroy (this.gameObject);
			canBreak = false;
		}
	}



}
