using UnityEngine;
using System.Collections;

public class PickupObjectController : MonoBehaviour {
	GameObject camera;
	GameObject carriedObject;

	public float distance = 3;
	public float smooth = 4;
	public bool carrying;

	public int x = 0, y = 180, z = 0;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (carrying) {
			carry(carriedObject);
//			rotateGameObject();
		} else {
			// TODO
		}
	}

	void rotateGameObject() {
		carriedObject.transform.Rotate(x,y,z);
	}

	void carry(GameObject o){
		o.transform.position = Vector3.Lerp (o.transform.position, camera.transform.position + camera.transform.forward * distance, Time.deltaTime * smooth);
	}

	public void pickup(){
		if (!carrying) {
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = camera.GetComponent<Camera>().ScreenPointToRay (new Vector3 (x, y));

			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
					PickupObject p = hit.collider.GetComponent<PickupObject> ();
					if (p != null) {
							p.isPicked = true;
							carrying = true;
							carriedObject = p.gameObject;
							//p.gameObject.rigidbody.isKinematic = true;
					}
			}
		} else {
			dropObject();
		}
	}

	void dropObject(){
		carrying = false;
		carriedObject.GetComponent<PickupObject>().isPicked = false;
		//carriedObject.gameObject.rigidbody.isKinematic = false;
		carriedObject = null;
	}

}
