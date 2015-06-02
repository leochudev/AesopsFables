using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour {
	public Vector3 position = new Vector3(1000f, 1000f, 1000f);         // The last global sighting of the player.
	public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);    // The default position if the player is not in sight.
}
