using UnityEngine;
using Vuforia;
using System.Collections;
using AnimationOrTween;

public class GameController : MonoBehaviour {
	public GameObject player;
	public GameObject enemy;
	
	public GameObject item_0;
	public GameObject item_1;
	public GameObject item_2;

	// public UIButton doorButton;

	public Transform SheepStart;
	public Transform WolfStart;

	public Vector3 position = new Vector3(1000f, 1000f, 1000f);         // The last global sighting of the player.
	public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);    // The default position if the player is not in sight.
	public float lightHighIntensity = 0.25f;                            // The directional light's intensity when the alarms are off.
	public float lightLowIntensity = 0f;                                // The directional light's intensity when the alarms are on.
	public float fadeSpeed = 7f;                                        // How fast the light fades between low and high intensity.
	public float musicFadeSpeed = 1f;                                   // The speed at which the 

	public GameObject EndingWindow;

	public bool isPlaying = false;
	public UILabel timer_label;
	public Transform[] waypoints;
	public float TimeLimit = 30.0f;

	public EventDelegate onGameOver;
	private float timeLeft;
	private bool isPause = false;
	private int currentLevel = 0;
	
	private Transform[] new_waypoints;
	public EventDelegate onFinishEvent;

	private int sheepNum;

	void Awake ()
	{
		new_waypoints = new Transform[waypoints.Length-1];
		for(int i = 0; i < waypoints.Length-2; i++){
			new_waypoints[i] = waypoints [i];
		}
	}

	void Start(){
		sheepNum = 0;
		currentLevel = 1;
		// GameStart (1);
	}

	public void StartGameplay(){
		GameStart(currentLevel);
	}

	public void StartGame(int i){
		GameStart(i);
	}

	public void nextLevel(){
		currentLevel ++;
		GameStart(currentLevel);
	}

	public void restartGame (){
		GameStart(currentLevel);
	}

	public void pauseGameplay(){
		isPause = true;
		pauseGame(true);
	}

	public void resumeGameplay(){
		isPause = false;
		pauseGame(false);
	}

	public void pauseGame(bool b){
		GameObject[] sheeps = GameObject.FindGameObjectsWithTag("Player"); 
		GameObject[] wolfs = GameObject.FindGameObjectsWithTag("Enemy"); 


		foreach (GameObject sheep in sheeps) {
			sheep.GetComponent<PlayerAI>().setIsPause(b);
		}
		
		foreach (GameObject wolf in wolfs) {
			wolf.GetComponent<EnemyAI>().setIsPause(b);
		}
	}

	void destroyAllGameObject(){
		GameObject[] sheeps = GameObject.FindGameObjectsWithTag("Player"); 
		GameObject[] wolfs = GameObject.FindGameObjectsWithTag("Enemy"); 
		GameObject[] items = GameObject.FindGameObjectsWithTag("Item"); 
		GameObject[] meats = GameObject.FindGameObjectsWithTag("Meat"); 

		foreach (GameObject sheep in sheeps) {
			Destroy(sheep);
		}
		
		foreach (GameObject wolf in wolfs) {
			Destroy(wolf);
		}

		foreach (GameObject item in items) {
			Destroy(item);
		}

		foreach (GameObject meat in meats) {
			Destroy(meat);
		}
	}

	void Update ()
	{
		if (isImageTargetShown()){
			if (!isPause)
				pauseGame(false);

			// if (isPlaying == false){
			// 	GameStart(1);
			// }
		} else {
			pauseGame(true);

			return;
		}

		// doorButton.isEnabled = isDoorButtonEnable ();
		if (isGameOver ()) {
			GameOver (isWin());
		}

		int timer_i = Mathf.FloorToInt (timeLeft);
		printTime (timer_i);

		if (timer_i > 0 && isPlaying) {
			timeLeft-=Time.deltaTime;
		} else if (timer_i <= 0) { 
			GameOver(false);
		}
	}

	public bool isGameOver (){
		if (isLose() || (isWin()&&isDoorButtonEnable()) ){
			return true;
		}
		return false;
	}

	bool isImageTargetShown(){
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

		foreach (GameObject target in targets) {
			if (!target.GetComponent<MyTrackableEventHandler>().isShown){
				return false;
			}
		}
		return true;
	}

	bool isDoorButtonEnable (){
		GameObject[] items = GameObject.FindGameObjectsWithTag("Item"); 
		if (items != null)
			if (items.Length == 0) {
				return true;
			}
		return false;
	}


	bool isWin (){
		GameObject[] sheeps = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject sheep in sheeps) {
			SheepTriggerObject obj = sheep.GetComponent<SheepTriggerObject>();
			if(obj != null)
				if (!obj.isSave)
					return false;
		}
		return true;
	}

	bool isLose (){
		GameObject[] sheeps = GameObject.FindGameObjectsWithTag("Player"); 
		if (sheeps.Length < sheepNum) {
			return true;
		}
		return false;
	}

	void printTime(int time){
		int min = time / 60;
		int sec = time % 60;

		timer_label.text = min.ToString("00")+":"+sec.ToString("00");
	}

	void reshuffle(int[] indexs)
	{
		for (int t = 0; t < indexs.Length; t++ )
		{
			int tmp = indexs[t];
			int r = Random.Range(t, indexs.Length);
			indexs[t] = indexs[r];
			indexs[r] = tmp;
		}
	}

	int getRandomNum(int start, int end){
		Random.seed = System.Guid.NewGuid ().GetHashCode ();
		int i = Random.Range (start, end);
		return i;
	}

	void initiateItem (int amount, int level){
		if (level == 0)
			return;

		int [] order = new int[new_waypoints.Length-1];
		for (int i = 0; i < order.Length; i++) {
			order[i] = i;
		}
		reshuffle (order);

		for (int i = 0; i < amount; i++) {
			if (level == 1){
				Instantiate(item_0, waypoints[order[i]].position, waypoints[order[i]].rotation);
			} else {
				int num = getRandomNum(0, 3);
				print (num);
				switch (num){
					default:
					case 0:
						Instantiate(item_1, waypoints[order[i]].position, waypoints[order[i]].rotation);
						break;
					case 1:
						Instantiate(item_2, waypoints[order[i]].position, waypoints[order[i]].rotation);
						break;
					case 2:
						Instantiate(item_0, waypoints[order[i]].position, waypoints[order[i]].rotation);
						break;
				}
			}
		}
	}

	void initiateSheep (int amount){
		for (int i = 0; i < amount; i++) {
			GameObject sheep = (GameObject) Instantiate(player, SheepStart.position, SheepStart.rotation);
			PlayerAI sheep_ai = sheep.GetComponent<PlayerAI>();
			sheep_ai.waypoints = waypoints;
		}
	}

	void initiateWolf (int amount){
		for (int i = 0; i < amount; i++) {
			GameObject wolf = (GameObject) Instantiate(enemy, WolfStart.position, WolfStart.rotation);
			EnemyAI wolf_ai = wolf.GetComponent<EnemyAI> ();
			wolf_ai.waypoints = waypoints;
		}
	}

	public void GameStart (int level){
		print(level);
		destroyAllGameObject ();
		isPlaying = true;
		timeLeft = TimeLimit/level;
		// currentLevel = level;

		int wolf_num = 1;
		int sheep_num = getRandomNum(2, 4);
		int item_num = getRandomNum (1, 3)*level;
		sheepNum = sheep_num;

		initiateWolf (wolf_num);
		initiateSheep (sheep_num);
		initiateItem (item_num, level);
	}

	public void ExitGame(){
		Application.LoadLevel(1);
	}


	public void showEndingDialog(){
		EndingWindow.GetComponent<TweenScale> ().duration = 0.5f;
		UIPlayTween tween = new UIPlayTween ();
		tween.tweenTarget = EndingWindow;
		tween.onFinished.Remove (onFinishEvent);
		tween.playDirection = Direction.Forward;
		// tween.disableWhenFinished = DisableCondition.DisableAfterForward;
		tween.ifDisabledOnPlay = EnableCondition.EnableThenPlay;
		tween.Play (true);
	}

	void GameOver(bool isWin){
		GameObject[] sheeps = GameObject.FindGameObjectsWithTag("Player"); 
		GameObject[] wolfs = GameObject.FindGameObjectsWithTag("Enemy"); 

		if (!isWin) {
			foreach (GameObject sheep in sheeps) {
				Destroy(sheep);
			}

		} else {
			foreach (GameObject sheep in sheeps) {
				PlayerAI sheepAi = sheep.GetComponent<PlayerAI>();
				sheepAi.GameWin();
			}

			foreach (GameObject wolf in wolfs) {
				Destroy(wolf);
			}
		}

		if (onGameOver != null && isPlaying) {
			showEndingDialog();
			onGameOver.Execute ();
			isPlaying = false;
		}
	}
}
