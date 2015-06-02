using UnityEngine;
using System.Collections;

public class enemyCollision : MonoBehaviour {
	public int type;
	public EventDelegate onGameOver;

	void OnTriggerEnter(Collider other){
		switch (type) {
			default:
			case 0:
				if (other.gameObject.tag == "Player"){
					if (onGameOver != null){
						onGameOver.Execute ();
					}
				}
				break;
		}
	}

	public void resetPosition(){
		GameObject control = GameObject.FindGameObjectWithTag("GameController");
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject startPoint = GameObject.FindGameObjectWithTag("Start");

		Vector3 vector = new Vector3(0,0,0);
		vector.x = startPoint.transform.position.x;
		vector.y = 0;
		vector.z = startPoint.transform.position.z;

		player.transform.position = vector;;
	}
}
