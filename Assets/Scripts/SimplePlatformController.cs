using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	 public bool jump = true;

	public float jumpForce;
	public Transform groundCheck;

	private bool grounded = false;
	private Rigidbody2D rb2d;

	public bool isAlive = true;
	private bool addScore;
	private int score = 0;

	public Text scoreText;
	public GameObject scoreObject;
	public GameObject titleText;

	public AudioClip jumpAudioClip;
	public AudioClip scoreAudioClip;

	private AudioSource audioSource;

	private enum gameState{
		titleState,
		playState,
		overState
	}

	private gameState currentState;

	// Use this for initialization
	void Awake () {
		scoreObject.SetActive (false);
		addScore = false;
		audioSource = GetComponent<AudioSource> ();
		rb2d = GetComponent<Rigidbody2D> ();
		currentState = gameState.titleState;
	}
	
	// Update is called once per frame
	void Update() {

		switch(currentState) {
		case gameState.titleState:
			{
				if(Input.GetMouseButtonDown(0)) {
					scoreObject.SetActive (true);
					titleText.SetActive (false);
					currentState = gameState.playState;
				}
			}
			break;
		case gameState.playState:
			{
				if (isAlive)
					MovementFunc ();
				else
					currentState = gameState.overState;
			}
			break;
		case gameState.overState:
			{
				
			}
			break;
		}


	}

	void FixedUpdate() {
		Movement ();
	}

	void MovementFunc() {
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if (grounded && Input.GetButtonDown("Jump") || grounded && Input.GetMouseButtonDown(0)) {
			audioSource.PlayOneShot (jumpAudioClip, 0.5f);
			print ("Grounded " + grounded);
			jump = true;
		}

		if (gameObject.transform.position.y < -16f) {
			isAlive = false;
			Destroy (gameObject);
		}

		if (grounded) {
			if (addScore) {
				audioSource.PlayOneShot (scoreAudioClip);
				score++;
				scoreText.text = score.ToString();
				addScore = false;
			}
		}

		else if (!grounded) {
			addScore = true;
		}
	}


	void Movement() {
		if (jump) {
			//rb2d.AddForce (new Vector2 (0f, jumpForce));
			rb2d.velocity = new Vector2 (0, jumpForce);
			jump = false;
		}
	}
}
