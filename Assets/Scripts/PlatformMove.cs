using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour {

	public float speed = 13f;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector2 (-1 * speed, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		rb2d.AddForce (new Vector2 (-1 * speed * Time.time, 0.0f));
	}

	void OnBecameVisible() {
		print ("Object is visible");
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
