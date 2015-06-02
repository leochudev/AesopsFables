using UnityEngine;
using System.Collections;

public class PlayerAI : MonoBehaviour {
	
	public Transform[] waypoints;
	public float pauseDuration = 0.0f;  // the duration while standing at the waypoint
	public int distance = 4;
	public float resetRate = 20.0f;

	public int currentWaypoint = 0;
	private float distanceToPlayer;
	private float curTime;
	private float timeReset;
	private Animator animator;
	private NavMeshAgent mesh;

	private PickupObjectController pickupObj;
	private bool isRunning = false;
	private bool isStopMoving = true;
	private bool isWin = false;
	private GameObject enemy;
	private bool isPause = false;

	public void setIsPause(bool b){
		isPause = b;
	}

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		mesh = GetComponent<NavMeshAgent> ();

		enemy =  GameObject.FindWithTag("Enemy");
		pickupObj = GameObject.FindWithTag("GameController").GetComponent<PickupObjectController>();
		setCurrentWaypoint();
	}
	
	// Update is called once per frame
	void Update () {
		if (isPause){
			mesh.SetDestination (mesh.transform.position);
			return;
		}

		if (isWin) {
			animator.SetFloat("Speed", 0);
			if (isStopMoving == false){
				mesh.Stop();
				isStopMoving = true;
			}
			return;
		}

		if (mesh != null)
			resetInvalidPath ();

		if (Time.time > timeReset) {
			timeReset = Time.time + resetRate;
			setCurrentWaypoint ();
		}

		if (pickupObj!=null && pickupObj.carrying && this.GetComponent<PickupObject>().isPicked) {
			if (isStopMoving == false){
				animator.SetBool ("is_picked_up", true);
				mesh.Stop (true);
			}
			isStopMoving = true;
			return;
		} else {
			if (isStopMoving == true){
				animator.SetBool ("is_picked_up", false);
				mesh.Resume();
			}
			isStopMoving = false;
		}

		if (animator) {
			animator.SetFloat("Speed", mesh.speed);
		}

		bool isSave = this.gameObject.GetComponent<SheepTriggerObject>().isSave;
		if(currentWaypoint < waypoints.Length && !isSave){
			patrol();
		}else{
			mesh.SetDestination (mesh.transform.position);
		}
	}

	void resetInvalidPath(){
		Vector3 target = waypoints [currentWaypoint].position;
		NavMeshPath path = new NavMeshPath();
		mesh.CalculatePath (target, path);
		if (path.status == NavMeshPathStatus.PathPartial) {
			setCurrentWaypoint ();
		}
	}

	void patrol() {
		isStopMoving = false;

		Vector3 target = waypoints [currentWaypoint].position;
		target.y = transform.position.y; // Keep waypoint at character's height
		Vector3 moveDirection = target - transform.position;

		if (enemy != null) {
			distanceToPlayer = Vector3.Distance (enemy.transform.position, transform.position);
			if (distanceToPlayer < distance && !isRunning) {
				isRunning = true;
				setEscapeWaypoint();
			}
		}

		if(moveDirection.magnitude < 0.5){
			if (curTime == 0)
				curTime = Time.time; // Pause over the Waypoint
			if ((Time.time - curTime) >= pauseDuration){
				setCurrentWaypoint();
				curTime = 0;
			}
		}else{
			if (mesh != null){
				mesh.SetDestination (target);
			}
		}
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
	
	void setEscapeWaypoint(){
		int[] arr = {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};
		reshuffle (arr);

		int target_waypoint = 0, target_distance = 0;
		int i = 0;
		for (i = 0; i < arr.Length; i++) {
			Vector3 target = waypoints [arr[i]].position;
			target.y = transform.position.y; // Keep waypoint at character's height
			Vector3 distance = target - enemy.transform.position;

			if (target_distance < distance.magnitude){
				target_distance = Mathf.FloorToInt(distance.magnitude);
				target_waypoint = i;
			}
		}
		currentWaypoint = target_waypoint;
	}

	void reshuffle(int[] indexs)
	{
		// Knuth shuffle algorithm :: courtesy of Wikipedia :)
		for (int t = 0; t < indexs.Length; t++ )
		{
			int tmp = indexs[t];
			int r = Random.Range(t, indexs.Length);
			indexs[t] = indexs[r];
			indexs[r] = tmp;
		}
	}

	public void GameWin(){
		isWin = true;
	}
}
