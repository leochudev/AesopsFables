using UnityEngine;
using System.Collections;

public class ThrowMeat : MonoBehaviour {
	GameObject camera;

	public Rigidbody projectile;
	
	public float fireRate;
	public float distance = 0.2f;
	public float calibration = 10.0f;
	
	private float nextFire;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindWithTag("MainCamera");
	}

	void Update(){
		nextFire = Time.time + fireRate;
//		if (Input.GetButton("Fire1")&&Time.time > nextFire)
//		{
//			Instantiate(Meat, shotSpawn.position, shotSpawn.rotation);
//		}
	}

	public void DropMeat ()
	{
		int x = Screen.width / 2;
		int y = Screen.height / 2;

		RaycastHit hit;
		Ray ray = camera.GetComponent<Camera>().ScreenPointToRay (new Vector3 (x, y));

		if (Physics.Raycast (ray, out hit)) {
			Rigidbody clone;	
			Vector3 spawn = hit.point;
			spawn.y = spawn.y + distance;
			clone = Instantiate (projectile, spawn, Quaternion.LookRotation (hit.normal)) as Rigidbody;
			clone.velocity = transform.TransformDirection(Vector3.forward * calibration);
		}
	}
}
