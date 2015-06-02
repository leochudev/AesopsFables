using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogHandler : MonoBehaviour {
	public List<string>	opening = new List<string>();
	public List<string>	ending = new List<string>();

	public UILabel label;
	public UILabel button_label;

	public float textSpeed = 10.0f;

	private int index = 0;
	private float second;
	private bool start, isFinished, isWin;

	public EventDelegate onFinish, disableWindow, showGameButtons, hideGameButtons, onQuit;

	private List<string> contents;

	// Use this for initialization
	void Start () {
		second = 0;
		index = 0;
		start = true;
		isFinished = false;

		duplicatedList (opening);
	}

	void duplicatedList(List<string> l){
		contents = new List<string> ();
		foreach (string s in l) {
			contents.Add(s);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			if(index == contents.Count-1){
				if (contents[0].Equals(opening[0]))
					button_label.text = "Start Game";
				else
					button_label.text = "Quit Game";
			} else {
				button_label.text = "Next";
			}

			second = second + Time.deltaTime * textSpeed;
			if ((int)second <= contents[index].Length){
				isFinished = false;
				label.text = contents[index].Substring (0, (int)second);
			}else{
				isFinished = true;
			}
		}
	}
	
	public void startLabelAnimation(){
		start = true;
	}

	public void startNextContent(){
		if (isFinished && (index + 1) < contents.Count) {
			index ++;
			second = 0;
		} else {
			if (isFinished){
				OnFinish();
			}
		}
	}

	public void OnFinish(){
		if (contents [0].Equals (opening [0])) {
			UIPlayTween tween = new UIPlayTween ();
			tween.tweenTarget = gameObject;
			tween.ifDisabledOnPlay = AnimationOrTween.EnableCondition.EnableThenPlay;
			tween.playDirection = AnimationOrTween.Direction.Reverse;
			tween.onFinished.Add (disableWindow);
			tween.onFinished.Remove (hideGameButtons);
			tween.Play (true);
		} else {
			QuitGame();
		}
	}

	public void QuitGame (){
		UIPlayTween tween = new UIPlayTween ();
		tween.tweenTarget = GameObject.FindGameObjectWithTag("Background");
		tween.playDirection = AnimationOrTween.Direction.Reverse;
		tween.ifDisabledOnPlay = AnimationOrTween.EnableCondition.EnableThenPlay;
		tween.onFinished.Add (onQuit);
		tween.Play (true);
	}

	public void DisableWindow(){
		gameObject.SetActive (false);
		showGameButtons.Execute ();
	}

	public void startGame(){
		GameObject frontground = GameObject.FindGameObjectWithTag("Frontground");
		TweenAlpha alpha = frontground.GetComponent<TweenAlpha> ();
		alpha.PlayReverse ();
		alpha.SetOnFinished (showGameButtons);
	}

	public void onGameFinish(){
		print ("on game finish");
		hideGameButtons.Execute ();
		UIPlayTween tween = new UIPlayTween ();
		tween.tweenTarget = gameObject;
		tween.ifDisabledOnPlay = AnimationOrTween.EnableCondition.EnableThenPlay;
		tween.playDirection = AnimationOrTween.Direction.Forward;
		tween.onFinished.Remove (disableWindow);
		tween.Play (true);
	}

	public void showDialogWindow(){
		GameObject frontground = GameObject.FindGameObjectWithTag("Frontground");
		UIPlayTween tween = new UIPlayTween ();
		tween.tweenTarget = frontground;
		tween.ifDisabledOnPlay = AnimationOrTween.EnableCondition.EnableThenPlay;
		tween.playDirection = AnimationOrTween.Direction.Reverse;
		tween.onFinished.Add (onFinish);
		tween.Play (true);
	}

	public void showGameOverDialog(){
		duplicatedList (ending);
		index = 0;
		second = 0;
		start = true;
		isFinished = false;

		showDialogWindow ();
	}
}
