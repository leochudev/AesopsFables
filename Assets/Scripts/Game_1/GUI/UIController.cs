using UnityEngine;
using AnimationOrTween;
using System.Collections;

public class UIController : MonoBehaviour {
	public UISprite background;
	public UISprite frontground;

	public GameObject gamePlayLayout;
	public GameObject modeSelectionWindow;
	public GameObject LevelSelectionWindow;

	public EventDelegate setDuration, startChallenge, startNormal;

	private int gameLevel;

	void Awake(){
		gameLevel = 0;

		int width = GetComponent<UIRoot> ().manualWidth;
		int height = GetComponent<UIRoot> ().manualHeight;

		background.SetRect (-width, -height, width*2, height*2);
		frontground.SetRect (-width, -height, width*2, height*2);
	}

	public void showModeDialog(){
		UIPlayTween tween = new UIPlayTween ();
		tween.tweenTarget = modeSelectionWindow;
		tween.ifDisabledOnPlay = EnableCondition.EnableThenPlay;
		tween.Play (true);
	}

	public void SetDuration() {
		LevelSelectionWindow.GetComponent<TweenScale> ().duration = 0.2f;
	}

	public void showLevelDialog(){
		modeSelectionWindow.SetActive (false);

		LevelSelectionWindow.GetComponent<TweenScale> ().duration = 0.0f;
		UIPlayTween tween = new UIPlayTween ();
		tween.tweenTarget = LevelSelectionWindow;
		tween.onFinished.Remove (startNormal);
		tween.playDirection = Direction.Forward;
		tween.disableWhenFinished = DisableCondition.DisableAfterForward;
		tween.ifDisabledOnPlay = EnableCondition.EnableThenPlay;
		tween.Play (true);
	}

	public void backModeDialog(){
		modeSelectionWindow.SetActive (true);
		LevelSelectionWindow.SetActive (false);
	}

	public void startLevel1(){
		gameLevel = 0;
		closeLevelWindow ();
	}
	public void startLevel2(){
		gameLevel = 1;
		closeLevelWindow ();
	}
	public void startLevel3(){
		gameLevel = 2;
		closeLevelWindow ();
	}

	private void closeLevelWindow(){
		LevelSelectionWindow.GetComponent<TweenScale> ().duration = 0.2f;

		UIPlayTween tween = new UIPlayTween ();
		tween.tweenTarget = LevelSelectionWindow;
		tween.onFinished.Add (startNormal);
		tween.playDirection = Direction.Reverse;
		tween.disableWhenFinished = DisableCondition.DisableAfterReverse;
		tween.Play (true);
	}

	public void startNormalMode(){
		LevelSelectionWindow.GetComponent<TweenScale> ().duration = 0.0f;
		LevelSelectionWindow.SetActive (false);

		ShowGamePlayButtons ();

		GameObject gameControll = GameObject.FindGameObjectWithTag ("GameController");
		GameController control = gameControll.GetComponent<GameController> ();
		control.GameStart (gameLevel);
	}

	public void startChallengeMode(){

	}

	public void onChallengeModeClick(){
		UIPlayTween tween = new UIPlayTween ();
		tween.tweenTarget = modeSelectionWindow;
		tween.ifDisabledOnPlay = EnableCondition.EnableThenPlay;
		tween.disableWhenFinished = DisableCondition.DisableAfterReverse;
		tween.playDirection = Direction.Reverse;
		tween.onFinished.Add(startChallenge);
		tween.Play (true);
	}

	public void ShowGamePlayButtons(){
//		UIPlayTween tween = new UIPlayTween ();
//		tween.tweenTarget = gamePlayLayout;
//		tween.playDirection = Direction.Forward;
//		tween.ifDisabledOnPlay = EnableCondition.EnableThenPlay;
//		tween.Play(true);
		gamePlayLayout.SetActive (true);
	}
	public void hideGamePlayButtons(){
//		UIPlayTween tween = new UIPlayTween ();
//		tween.tweenTarget = gamePlayLayout;
//		tween.playDirection = Direction.Reverse;
//		tween.ifDisabledOnPlay = EnableCondition.EnableThenPlay;
//		tween.Play(true);
		gamePlayLayout.SetActive (false);
	}
}
