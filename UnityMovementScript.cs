///////////////////////////////////////////////////
// Copyright(C) 2017-2018 Pi Programs            //
// See license file.                             //
// If the program did not come with a            //
// license file it is unusable and you           //
// should try to gather a correct copy           //
// from http://github/DragonMastur/RandomFiles/  //
///////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Globalization;

public class Movement : MonoBehaviour
{

	public static bool canjump;

	public static float jumpForce, speed;



	public AudioSource grassSounds;

	public static bool isGrounded, isGroundedLeft, isGroundedRight;

	public AudioSource jumpSounds;

	public Rigidbody2D player;

	public static bool sahenianCanJump = false;

	void Start () {

		player = GetComponent<Rigidbody2D> ();

		jumpForce = 30f;

		speed = 1f;



	}

	void Update () {

		speed = float.Parse (PauseConfig.speedValue, NumberStyles.Float, CultureInfo.InvariantCulture);

		jumpForce = float.Parse (PauseConfig.jumpValue, NumberStyles.Float, CultureInfo.InvariantCulture);


		isGrounded = Physics2D.Linecast(player.transform.position, new Vector3(player.transform.position.x,player.transform.position.y - 1, 0), 1 << LayerMask.NameToLayer("Ground"));

		isGroundedLeft = Physics2D.Linecast(player.transform.position, new Vector3(player.transform.position.x - 0.75f,player.transform.position.y - 1, 0), 1 << LayerMask.NameToLayer("Ground"));

		isGroundedRight = Physics2D.Linecast(player.transform.position, new Vector3(player.transform.position.x + 0.75f,player.transform.position.y - 1, 0), 1 << LayerMask.NameToLayer("Ground"));

		if (isGrounded || isGroundedLeft || isGroundedRight) {
			canjump = true;
		}

		Debug.DrawLine (player.transform.position, new Vector3(player.transform.position.x,player.transform.position.y - 1, 0), Color.green);

		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (Time.timeScale == 1) {
				player.AddForce (new Vector2 (-speed, 0), ForceMode2D.Impulse);
				player.transform.localScale = new Vector2(-0.3f, transform.localScale.y);

			}
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			if (Time.timeScale == 1) {
				player.AddForce (new Vector2 (speed, 0), ForceMode2D.Impulse);
				player.transform.localScale = new Vector2(0.3f, transform.localScale.y);

			}
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {

			if (isGrounded && canjump || isGroundedLeft && canjump || isGroundedRight && canjump || sahenianCanJump) {
				if (Time.timeScale == 1) {
					player.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
				}
			}

		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "ground" || coll.gameObject.tag == "wall" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Boss"){
			canjump = true;

		}

	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "ground" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "wall" || coll.gameObject.tag == "Boss"){
			canjump = false;
			grassSounds.Stop ();
			if (coll.gameObject.tag == "ground" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "wall" || coll.gameObject.tag == "Boss") {

				if (Input.GetKey (KeyCode.UpArrow)) {
					jumpSounds.Play ();
				}
			}

		}


	}

	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag == "ground" || coll.gameObject.tag == "wall"){
			if(Input.GetKeyDown(KeyCode.RightArrow)){
				grassSounds.Play();
			}

			if (Input.GetKeyUp (KeyCode.RightArrow)) {
				grassSounds.Stop ();
			}

			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				grassSounds.Play();
			}

			if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				grassSounds.Stop ();
			}


		}


	}








}
