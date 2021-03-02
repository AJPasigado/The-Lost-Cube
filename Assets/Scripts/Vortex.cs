using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour {
	public GameObject destination;
	public GameObject player;
	public GameObject particle;

	public AudioSource zapSource;

	// Use this for initialization
	void Start () {
		
	}

	void  OnCollisionEnter(Collision other)
	{
		if (other.transform.name == "Player") { 
			player.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionY;
			zapSource.Play ();
			zapSource.Play (1000);
			player.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y - 1.2f, destination.transform.position.z);
			particle.transform.position = player.transform.position;
			particle.SetActive (true);
		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}
