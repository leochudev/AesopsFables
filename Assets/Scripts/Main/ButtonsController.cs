using UnityEngine;
using System.Collections;

public class ButtonsController : MonoBehaviour {
	private FableController script;

	public void Awake(){

	}

	public void onLoadFinished(){
		script = gameObject.GetComponent<FableController> ();
//		print (script.currentPage);
		Application.LoadLevel ("game_1");
	}
}
