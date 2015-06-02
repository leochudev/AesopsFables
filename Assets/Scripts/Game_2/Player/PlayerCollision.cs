using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
	public int type;
	private bool isEnter;
	private Vector3 vector;

	public EventDelegate onGameOver;

	void Start(){
		isEnter = false;
	}

	void OnTriggerEnter(Collider other){
		Vector3 vector = new Vector3(0,0,0);

		if (other.gameObject.tag == "Door"){
			GameObject door2 = GameObject.FindGameObjectWithTag("Door2");
			vector.x = door2.transform.position.x;
			vector.y = 0;
			vector.z = door2.transform.position.z+150.5f;
			this.transform.position = vector;
		} else if (other.gameObject.tag == "Door2"){
			GameObject door1 = GameObject.FindGameObjectWithTag("Door");
			vector.x = door1.transform.position.x;
			vector.y = 0;
			vector.z = door1.transform.position.z-150.5f;
			this.transform.position = vector;
		} else if (other.gameObject.tag == "End"){
			if (onGameOver != null) {
				onGameOver.Execute ();
			}
		}
	}



	// void OnTriggerExit(Collider other){
	// 	if (other.gameObject.tag == "Door"){
	// 		if(isEnter){
	// 			isEnter = !isEnter;
	// 		}
	// 	}
	// }
}
