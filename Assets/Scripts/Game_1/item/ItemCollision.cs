using UnityEngine;
using System.Collections;

public class ItemCollision : MonoBehaviour {
	public int type;

	void OnTriggerEnter(Collider other){
		switch (type) {
			default:
			case 0:
				if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
					Destroy(this.gameObject);
				break;
			case 1:
				if (other.gameObject.tag == "Enemy")
					Destroy(this.gameObject);
				break;
			case 2:
				if (other.gameObject.tag == "Player")
					Destroy(this.gameObject);
				break;
		}
	}
}
