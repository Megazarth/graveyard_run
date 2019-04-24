using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {

	public int maxPlatforms = 20;
	public GameObject platform;
	public float horizontalMin = 6.5f;
	public float horizontalMax = 14f;
	public float verticalMin = -6f;
	public float verticalMax = 6f;

	public float lengthMin = 2.0f;
	public float lengthMax = 10.0f;

	private SimplePlatformController playerScript;

	private AudioSource audioSource;

	public Text gameText;

	private GameObject temporaryPlatform;

	private enum gameState{
		titleState,
		playState,
		overState
	}

	private gameState currentState;


	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		playerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<SimplePlatformController>();
		temporaryPlatform = GameObject.Find ("TemporaryPlatform");
		currentState = gameState.titleState;
		StartCoroutine ("Blinking");
	}

	void Update() {

		switch(currentState) {
		case gameState.titleState:
			{
				if(Input.GetMouseButtonDown(0)) {
					InvokeRepeating ("Spawn", 0f, 1f);
					temporaryPlatform.GetComponent<Rigidbody2D>().velocity = new Vector2 (-1 * 7f, 0.0f);
					StopCoroutine ("Blinking");
					gameText.enabled = false;
					currentState = gameState.playState;
				}
			}
			break;
		case gameState.playState:
			{
				if (!playerScript.isAlive) {
					CancelInvoke ("Spawn");
					audioSource.Play ();
					currentState = gameState.overState;
				}
			}
			break;
		case gameState.overState:
			{

				gameText.text = "Touch Screen to Restart!";
				gameText.enabled = true;
				if(Input.GetMouseButtonDown(0)) {
					SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
				}
			}
			break;
		}

	}

	void Spawn() {
		print ("Spawn Platforms");
		Vector2 randomPosition = new Vector2 (
			Random.Range (horizontalMin, horizontalMax), 
			Random.Range (verticalMin, verticalMax));
		Instantiate (platform, randomPosition, Quaternion.identity);
	}

	IEnumerator Blinking() {
		while(true) {
			gameText.enabled = false;
			yield return new WaitForSeconds(0.5f);
			gameText.enabled = true;
			yield return new WaitForSeconds(0.5f);

		}
	}
		

}
