
var raycastDist : float = 100;

var tagCheck : String = "Level Parts";
var checkAllTags : boolean = false;

var cube : Transform;

// Use this for initialization
function Start () {
}

// Update is called once per frame
function Update () {
	var foundHit : boolean = false;
	
	var hit : RaycastHit;
	
	if (Input.GetButton("Fire1")){
		foundHit = Physics.Raycast(transform.position, transform.forward, hit);
	}
	
	if (foundHit){
		cube.position = hit.point;
		cube.rotation = Quaternion.LookRotation(hit.normal);
	}
}

