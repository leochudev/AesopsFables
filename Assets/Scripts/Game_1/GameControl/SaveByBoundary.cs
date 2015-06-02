using UnityEngine;
using System.Collections;

public class SaveByBoundary : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			SheepTriggerObject obj = other.gameObject.GetComponent<SheepTriggerObject>();
			if (obj != null)
				obj.isSave = true;
		}

		if (other.gameObject.tag == "Meat") {
			Destroy (other.gameObject);
		}

			
	}
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			SheepTriggerObject obj = other.gameObject.GetComponent<SheepTriggerObject>();
			if (obj != null)
				obj.isSave = false;
		}
	}
}
