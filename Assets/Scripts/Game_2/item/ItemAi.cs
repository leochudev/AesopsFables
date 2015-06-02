using UnityEngine;
using System.Collections;

public class ItemAi : MonoBehaviour {
	public Transform waypoint;
	
	private GameObject player;
	private GameObject target;
	private NavMeshAgent character;


	
	// Use this for initialization
	void Start () {
		character = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag("Player");
		target = GameObject.FindGameObjectWithTag("Target");
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null){
			if (target.GetComponent<EnemySightTrigger>().inSight){
				character.SetDestination (player.transform.position);
				return;
			}
		}
		character.SetDestination (waypoint.position);
	}
}
