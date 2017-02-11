using UnityEngine;
using System.Collections;

public class warpPlane : MonoBehaviour {
	
	[SerializeField]private GameObject warpField;
	public float countdown = 0.3f;
	// Use this for initialization
	public void Start () {
		Update ();
	}

	public void resetTimer () {
		countdown = 3.0f;
	}

	void Update() {
		
		Debug.Log ("start warp routine");

		countdown -= Time.deltaTime;
		Debug.Log (countdown);

		if (countdown > 0.0f) {
			Debug.Log ("activating warp plane");
			warpField.SetActive (true);
			//countdown -= Time.deltaTime;
		}
		if (countdown <= 0.0f) {
			Debug.Log ("timer up - turn off warp plane");
			warpField.SetActive (false);
			resetTimer ();
			//countdown -= Time.deltaTime;
		}
	}
		
}
