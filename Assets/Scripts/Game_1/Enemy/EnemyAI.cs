using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public Transform[] waypoints;
	public bool loop = true; 			// keep repeating the waypoints
	public float pauseDuration = 0.0f;  // the duration while standing at the waypoint
	public float eatingTime = 4.0f;
	public bool isEatingMeat = false;
	public bool isEatingSheep = false;

	private float distanceToPlayer;
	private float curTime;
	public int currentWaypoint = 0;
	private NavMeshAgent character;
	private Animator animator;
	
	private HashIDs hash;
	private bool isPause = false;

	public void setIsPause(bool b){
		isPause = b;
	}

	// Use this for initialization
	void Start () {
		character = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isPause){
			print ("wolf pause");
			character.SetDestination (character.transform.position);
			return;
		}

		if (isEatingMeat) {
			eatMeat();
			return;
		}
		
		if (isEatingSheep) {
			eatSheep();
			return;
		}

		//distanceToPlayer = Vector3.Distance (enemy.transform.position, transform.position);
		if (animator) {
			animator.SetFloat("Speed", character.speed);
		}


		GameObject meat = GameObject.FindGameObjectWithTag("Meat");
		if (meat != null) {
			character.SetDestination (meat.transform.position);
			return;
		}

		if(currentWaypoint < waypoints.Length && !isEatingMeat && !isEatingSheep){
			patrol();
		}
	}
	
	void patrol() {
		Vector3 target = waypoints [currentWaypoint].position;
		target.y = transform.position.y; // Keep waypoint at character's height
		Vector3 moveDirection = target - transform.position;

		if(moveDirection.magnitude < 0.5){
			if (curTime == 0)
				curTime = Time.time; // Pause over the Waypoint
			if ((Time.time - curTime) >= pauseDuration){
				setCurrentWaypoint();
				curTime = 0;
			}
		}else{        
			character.SetDestination (target);
		}
	}

	void eatMeat(){
		character.SetDestination(character.transform.position);
		animator.SetFloat(hash.speedFloat, 0);

		if (curTime == 0)
			curTime = Time.time; // Pause over the Waypoint
		if ((Time.time - curTime) >= eatingTime){
			curTime = 0;
			isEatingMeat = false;
		}
		animator.SetBool(hash.isEatingMeatBool, isEatingMeat);
	}
	
	void eatSheep(){
		character.SetDestination(character.transform.position);
		animator.SetFloat(hash.speedFloat, 0);
		
		if (curTime == 0)
			curTime = Time.time; // Pause over the Waypoint
		if ((Time.time - curTime) >= eatingTime){
			curTime = 0;
			isEatingSheep = false;
		}
		animator.SetBool(hash.isEatingSheepBool, isEatingSheep);
	}
	
	void setCurrentWaypoint(){
		Random.seed = System.Guid.NewGuid ().GetHashCode ();
		int i = Random.Range (0, waypoints.Length);
		currentWaypoint = i;

		Vector3 target = waypoints [currentWaypoint].position;
		BoxCollider collider = GameObject.Find("Raillings").GetComponent<BoxCollider>();
		if (collider != null){
			if (collider.bounds.Contains(target)){
				setCurrentWaypoint();
			}
		}
	}
}
