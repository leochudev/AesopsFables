using UnityEngine;
using System.Collections;

public class itemMovement : MonoBehaviour {	
	public Transform target;
	public float speed = 1.0f;

	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
	}
}
