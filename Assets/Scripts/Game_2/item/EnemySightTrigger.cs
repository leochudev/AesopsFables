using UnityEngine;
using System.Collections;

public class EnemySightTrigger : MonoBehaviour {
	public bool inSight = false;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player")
			inSight = true;
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player")
			inSight = false;
	}
}
