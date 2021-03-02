using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float velocity;
	public GameObject player;
	bool inGame = false;
	Vector3 movement = new Vector3 (0, 0, 0);
	public GameObject enemy;
	public GameObject particle;
	public GameObject sims;
	public GameObject beforeParticle;
	public GameObject beforeVortex;
	public GameObject haloParticle;

	public AudioSource swooshSource;
	public AudioSource startSource;
	public AudioSource wonSource;
	public AudioSource zapSource;

	int mode = 0;
	bool won = false;


	// Use this for initialization
	void Start () {
		particle.SetActive(false);
		startSource.Play ();
	}

	void  OnCollisionEnter(Collision other)
	{
		print (other.transform.name);
		if (other.transform.name == "Wall") {
			this.GetComponent<MeshRenderer> ().enabled = false;
			beforeParticle.SetActive (true);
			beforeParticle.transform.position = this.transform.position;
			particle.SetActive (true);
			particle.transform.position = this.transform.position;
			zapSource.Play ();
			wonSource.Play (20000);
			inGame = false;
		} else if (other.transform.name == "Enemy") {
			inGame = false;
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			beforeParticle.SetActive (true);
			beforeParticle.transform.position = enemy.transform.position;
			particle.SetActive (true);
			particle.transform.position = enemy.transform.position;
			haloParticle.SetActive (true);
			haloParticle.transform.position = this.transform.position;
			zapSource.Play ();
			wonSource.Play (20000);
			enemy.GetComponent<MeshRenderer> ().enabled = false;
			won = true;
		} else if (other.transform.name == "Obstacle") {
			inGame = false;
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		} else if (other.transform.name == "Vortex") {
			beforeVortex.SetActive (true);
			beforeVortex.transform.position = this.transform.position;
		}
	}

	
	// Update is called once per frame
	void Update () {
		if (!inGame) {
			if (Input.GetKey (KeyCode.W) && mode != 1) {
				GetComponent<Rigidbody> ().constraints = ~RigidbodyConstraints.FreezePositionZ;
				movement = new Vector3 (0, 0, velocity);
				mode = 1;
				swooshSource.Play ();
				inGame = true;
			} else if (Input.GetKey (KeyCode.S) && mode != 2) {
				GetComponent<Rigidbody> ().constraints = ~RigidbodyConstraints.FreezePositionZ;
				movement = new Vector3 (0, 0, velocity * -1);
				mode = 2;
				swooshSource.Play ();
				inGame = true;
			} else if (Input.GetKey (KeyCode.A) && mode != 3) {
				GetComponent<Rigidbody> ().constraints = ~RigidbodyConstraints.FreezePositionX;
				movement = new Vector3 (velocity * -1, 0, 0);
				mode = 3;
				swooshSource.Play ();
				inGame = true;
			} else if (Input.GetKey (KeyCode.D) && mode != 4) {
				GetComponent<Rigidbody> ().constraints = ~RigidbodyConstraints.FreezePositionX;
				movement = new Vector3 (velocity, 0, 0);
				mode = 4;
				swooshSource.Play ();
				inGame = true;
			}
		}

		if (inGame) {
			GetComponent<Rigidbody>().velocity += movement;
		}

		if (won) {
			sims.transform.position = Vector3.MoveTowards (sims.transform.position, transform.position, 0.1f);
		}
	}
}
