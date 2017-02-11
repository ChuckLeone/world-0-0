using UnityEngine;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples {

	public class ballActions : MonoBehaviour {

		[SerializeField] public GameObject player;
		[SerializeField] public GameObject ballObject;
		[SerializeField] public GameObject activateBlueLight;
		[SerializeField] public GameObject activatePinkLight;
		[SerializeField] public GameObject activateGreenLight;
		[SerializeField] public GameObject activateYellowLight;
		[SerializeField] public GameObject deActivate;
		[SerializeField] public AudioClip activateSound;
		[SerializeField] public GameObject activateWarp;
		[SerializeField] public AudioClip hitSound;
		[SerializeField] public AudioSource source;
		[SerializeField] public Camera m_camera;
		[SerializeField] private Material m_NormalMaterial;                
		[SerializeField] private Material m_OverMaterial;                  
		[SerializeField] private Material m_ClickedMaterial;               
		[SerializeField] private Material m_DoubleClickedMaterial;         
		[SerializeField] private VRInteractiveItem m_InteractiveItem;
		[SerializeField] private Renderer m_Renderer;
		[SerializeField] public GameObject warpField;

		public startGame resetBall;
		public warpPlane warpNow;
		public gameLoop updateScoreP;
		public gameLoop updateScoreCPU;
		public block activateBlock;


		void Start () {
			source = GetComponent<AudioSource>();
		}

		private void Awake ()
		{
			m_Renderer.material = m_NormalMaterial;
		}

		private void OnEnable()
		{
			m_InteractiveItem.OnOver += HandleOver;
			m_InteractiveItem.OnOut += HandleOut;
			m_InteractiveItem.OnClick += HandleClick;
			m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
			m_InteractiveItem.OnDown += HandleDown;
			m_InteractiveItem.OnUp += HandleUp;
		}

		private void OnDisable()
		{
			m_InteractiveItem.OnOver -= HandleOver;
			m_InteractiveItem.OnOut -= HandleOut;
			m_InteractiveItem.OnClick -= HandleClick;
			m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
		}

		//Handle the Over event
		private void HandleOver()
		{
			Debug.Log("Show over state");
			m_Renderer.material = m_OverMaterial;
		}


		//Handle the Out event
		private void HandleOut()
		{
			Debug.Log("Show out state");
			m_Renderer.material = m_NormalMaterial;
			//Invoke("forceBall", 0);
		}


		//Handle the Click event
		private void HandleClick()
		{
			Debug.Log("Show click state");
			m_Renderer.material = m_ClickedMaterial;
			//ballObject.transform.parent = m_camera;
			//var player = m_camera;
		}

		//Handle the DoubleClick event
		private void HandleDoubleClick()
		{
			Debug.Log("Show double click");
			m_Renderer.material = m_DoubleClickedMaterial;
		}

		// Handle the up event
		private void HandleUp () {
			Invoke("forceBall", 0);
		}

		//Handle the down event 
		private void HandleDown () {
			Invoke("grabBall", 0);

		}

		//ball collision/scoring
		void OnTriggerEnter (Collider other)
		{    
			if (other.tag == "Player")	
			{
				Invoke ("grabBall", 0);
			}
			if (other.tag == "ScorePlayer")	
			{
				Invoke ("warpLaunch", 0);
				activatePinkLight.SetActive (true);
				source.PlayOneShot(activateSound, 0.7f);
				updateScoreP.updatePlayerScore ();
				resetBall.dropBall();

			}
			if (other.tag == "ScoreCPU")	
			{
				activateBlueLight.SetActive (true);
				activateYellowLight.SetActive (true);
				source.PlayOneShot(activateSound, 0.7f);
				updateScoreCPU.updateCpuScore ();
				resetBall.dropBall(); 
			}
			if (other.tag == "Block")	
			{
				//change material color
				activateBlock.ActivateThis();

				//add multiplier/points
			}
			if (other.tag == "Bounds")	
			{
				resetBall.dropBall();
			}
			if (other.tag == "Plane")	
			{
				deActivate.SetActive (false);
			}
		}

		// grab the ball
		void grabBall () {

			// establish the front position of the player
			//Vector3 distance = new Vector3(0, 2, 5);
			//var playerFront = player.transform.position + distance;
			Vector3 distance = new Vector3(0, 2, 5);


			// get the player's rotation
			float yRotation = 5.0F;
			yRotation += Input.GetAxis("Horizontal");
			transform.eulerAngles = new Vector3(10, yRotation, 0);

			var playerFront = player.transform.position + distance;

			// calc the angle to place the ball

			// make player the parent of the ball
			ballObject.transform.position = playerFront;

			ballObject.transform.parent = player.transform;

			Debug.Log ("grabbing ball...");

		}
			

		// apply force to ball
		void forceBall() {
			source.PlayOneShot(hitSound, 0.8f);
			Debug.Log ("applying force to ball");
			Vector3 dir = new Vector3(0, 0, 10);
			dir.Normalize ();
			this.ballObject.GetComponent<Rigidbody>().AddForce (dir * 10000);
		}


		void warpLaunch() {	
			warpNow.Start ();
		}
			
	}
}
	