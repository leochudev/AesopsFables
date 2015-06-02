using UnityEngine;
using System.Collections;
using AnimationOrTween;

public class GameHandler : MonoBehaviour {
	public UILabel timer_label;
	public float timeLimit = 180.0f;
	public GameObject EndingWindow;
	public Transform[] waypoints;
	
	private float timeLeft;
	private bool isPlaying;
	
	public void startGame(){
		isPlaying = true;
		timeLeft = timeLimit;

		GameObject control = GameObject.FindGameObjectWithTag("GameController");
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject startPoint = GameObject.FindGameObjectWithTag("Start");

		Vector3 vector = new Vector3(0,0,0);
		vector.x = startPoint.transform.position.x;
		vector.y = 0;
		vector.z = startPoint.transform.position.z;

		player.transform.position = vector;
		control.GetComponent<Canvas>().enabled = true;

		GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
		
		int i = 0;
		foreach (GameObject enemy in enemys) {
			enemy.transform.position = waypoints[i%enemys.Length].position;
			i++;
		}
	}

	public void restartGame(){
		startGame();	
	}

	public void stopGame(){
		isPlaying = false;

		GameObject control = GameObject.FindGameObjectWithTag("GameController");
		control.GetComponent<Canvas>().enabled = false;
	}
	
	public void pauseGame(){
		isPlaying = false;

		GameObject control = GameObject.FindGameObjectWithTag("GameController");
		control.GetComponent<Canvas>().enabled = false;
	}

	public void resumeGame(){
		isPlaying = true;

		GameObject control = GameObject.FindGameObjectWithTag("GameController");
		control.GetComponent<Canvas>().enabled = true;
	}

	public void gameOver(bool isWin){
		pauseGame();
		showEndingDialog();
	}

	public void winGame(){
		gameOver(true);
	}

	public void showEndingDialog(){
		EndingWindow.GetComponent<TweenScale> ().duration = 0.5f;
		UIPlayTween tween = new UIPlayTween ();
		tween.tweenTarget = EndingWindow;
		tween.playDirection = Direction.Forward;
		// tween.disableWhenFinished = DisableCondition.DisableAfterForward;
		tween.ifDisabledOnPlay = EnableCondition.EnableThenPlay;
		tween.Play (true);
	}

	public void ExitGame(){
		Application.LoadLevel(1);
	}

	void printTime(int time){
		int min = time / 60;
		int sec = time % 60;

		timer_label.text = min.ToString("00")+":"+sec.ToString("00");
	}

	// Use this for initialization
	void Start () {
		isPlaying = false;

		GameObject control = GameObject.FindGameObjectWithTag("GameController");
		control.GetComponent<Canvas>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		int timer_i = Mathf.FloorToInt (timeLeft);
		printTime (timer_i);

		if (timer_i > 0 && isPlaying) {
			timeLeft-=Time.deltaTime;
		} else if (timer_i <= 0 && isPlaying) { 
			gameOver(false);
		}
	}
}
