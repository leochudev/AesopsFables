using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		// Raycasting to determine what side of the brick the ball hits
//		Ray myRay = new Ray(transform.position, other.gameObject.transform.position);
//		RaycastHit myRayHit;
//		
//		Physics.Raycast(myRay, out myRayHit);
//		
//		Vector3 myNormal = myRayHit.normal;
//		myNormal = myRayHit.transform.TransformDirection(myNormal);
//		print (myNormal);

		if (other.gameObject.tag == "Player") { //eat sheep
			Destroy (other.gameObject);
			EnemyAI enemyAi = GetComponent<EnemyAI>();
			enemyAi.isEatingSheep = true;
		} else if (other.gameObject.tag == "Meat") { // eat meat
			Destroy (other.gameObject);
			EnemyAI enemyAi = GetComponent<EnemyAI>();
			enemyAi.isEatingMeat = true;
		}
	}
}
