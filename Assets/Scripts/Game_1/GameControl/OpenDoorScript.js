#pragma strict

//Make an empty game object and call it "Door"
//Rename your 3D door model to "Body"
//Parent a "Body" object to "Door"
//Make sure thet a "Door" object is in left down corner of "Body" object. The place where a Door Hinge need be
//Add a box collider to "Door" object and make it much bigger then the "Body" model, mark it trigger
//Assign this script to a "Door" game object that have box collider with trigger enabled
//Press "f" to open the door and "g" to close the door
//Make sure the main character is tagged "player"

// Smothly open a door
var smooth = 2.0;
var DoorOpenAngle = 90.0;
public var door : GameObject;
private var open : boolean;
// private var enter : boolean;

private var defaultRot : Vector3;
private var openRot : Vector3;

function Start(){
	open = false;
	defaultRot = door.transform.eulerAngles;
	openRot = new Vector3 (defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z/2);
}

//Main function
function Update (){
	if(open){
		//Open door
		door.transform.eulerAngles = Vector3.Slerp(door.transform.eulerAngles, openRot, Time.deltaTime * smooth);
	}else{
		//Close door
		door.transform.eulerAngles = Vector3.Slerp(door.transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
	}
}

function OpenDoor (){
	open = !open;
}


//Activate the Main function when player is near the door
//function OnTriggerEnter (other : Collider){
//	if (other.gameObject.tag == "Player") {
//		enter = true;
//	}
//}

//Deactivate the Main function when player is go away from door
//function OnTriggerExit (other : Collider){
//	if (other.gameObject.tag == "Player") {
//		enter = false;
//	}
//}