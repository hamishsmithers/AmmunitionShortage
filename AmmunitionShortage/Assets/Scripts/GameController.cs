using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	//player1 is a reference to the first player
	public PlayerController player1;
	//player2 is a reference to the second player
	public PlayerController player2;

	//player1Text is a reference to the text used to display the first player's ammo
	public Text player1Text;
	//player2Text is a reference to the text used to display the secondplayer's ammo
	public Text player2Text;

	//----------------------------------------------------------------------------------------------------
	//Update()
	//	Called once per frame.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	void Update () {
		if (player1) {
			player1Text.text = player1.currentAmmo.ToString();
		}
		if (player2) {
			player2Text.text = player2.currentAmmo.ToString();
		}
	}
}
