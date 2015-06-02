using UnityEngine;
using System.Collections;

public class MenuButtonController : MonoBehaviour {
	public void onBackButtonClicked() {
		Application.LoadLevel ("main");
	}
}
