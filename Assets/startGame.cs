using UnityEngine;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples {

	public class startGame : MonoBehaviour {

		[SerializeField] public GameObject objToDeactivate;
		[SerializeField] public GameObject warpDeactivate;
		[SerializeField] public GameObject ball;
		[SerializeField] public GameObject objToActivate;
		[SerializeField] public AudioClip activateSound;
		[SerializeField] public AudioSource source;
		[SerializeField] private GameObject startMusic;
		[SerializeField] private GameObject gameLevel;

		[SerializeField] private Material m_NormalMaterial;                
		[SerializeField] private Material m_OverMaterial;                  
		[SerializeField] private Material m_ClickedMaterial;               
		[SerializeField] private Material m_DoubleClickedMaterial;         
		[SerializeField] private VRInteractiveItem m_InteractiveItem;
		[SerializeField] private Renderer m_Renderer;

		//public gameLoop activateLevel;
		//public Rigidbody ball;
		public float speed = 10f;

		void Start () {
			source = GetComponent<AudioSource>();
			startMusic.SetActive (true);
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
		}


		//Handle the Click event
		public void HandleClick()
		{
			Debug.Log("Show click state");
			m_Renderer.material = m_ClickedMaterial;
			objToDeactivate.SetActive (false);
			gameLevel.SetActive(true);
			source.PlayOneShot(activateSound, 0.7f);
			Invoke ("dropBall", 0);
		}

		public void dropBall() {
			objToActivate.SetActive (true);
			warpDeactivate.SetActive (false);
			ball.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			ball.transform.position = new Vector3(0, 60, 0);

		}

		//Handle the DoubleClick event
		private void HandleDoubleClick()
		{
			Debug.Log("Show double click");
			m_Renderer.material = m_DoubleClickedMaterial;
		}
	}
}