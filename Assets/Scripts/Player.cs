﻿/* 
 * ANTON JOHN B. PASIGADO
 * ComSc 4A
 */

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
			GameManager.instance.life -= 1;
			Destroy(GameObject.Find("Image(Clone)"));

			print (GameManager.instance.life);
			int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex;
			if (GameManager.instance.life == 0) {
				print ("Life is 0");
				nextSceneIndex = 6;
				Destroy (GameManager.instance);
				Destroy (GameObject.Find ("Canvas"));
			}
            StartCoroutine("LoadScene", nextSceneIndex);
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
			int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex;

            if (nextSceneIndex != 5) {
                nextSceneIndex++;
            } else {
                nextSceneIndex = 7;
                Destroy(GameManager.instance);
                Destroy(GameObject.Find("Canvas"));
            }
            StartCoroutine("LoadScene", nextSceneIndex);
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

    IEnumerator LoadScene(int nextIndex) {
        yield return new WaitForSeconds(1); //Count is the amount of time in seconds that you want to wait.

        string nextPath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(nextIndex);
        Initiate.Fade(nextPath, Color.black, 1.0f);             

        yield return null;
    }
}
