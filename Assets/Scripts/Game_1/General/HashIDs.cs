using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour {
	// Here we store the hash tags for various strings used in our animators.
	public int eatingSheepState;
	public int eatingMeatState;
	public int locomotionState;
	public int pickingState;
	public int isPickBool;
	public int isEatingSheepBool;
	public int isEatingMeatBool;
	public int speedFloat;

	void Awake ()
	{
		eatingMeatState = Animator.StringToHash("Base Layer.EatingMeat");
		locomotionState = Animator.StringToHash("Base Layer.Locomotion");
		pickingState = Animator.StringToHash("Base Layer.Pickup");
		isPickBool = Animator.StringToHash("is_picked_up");
		speedFloat = Animator.StringToHash("Speed");
		isEatingMeatBool = Animator.StringToHash("is_eating_meat");
		isEatingSheepBool = Animator.StringToHash("is_eating_sheep");
	}
}
