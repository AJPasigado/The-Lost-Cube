/* 
 * ANTON JOHN B. PASIGADO
 * ComSc 4A
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sims : MonoBehaviour {
	public GameObject player;
	public GameObject enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float newY = Mathf.Sin(Time.time * 2) * 0.3f;
		newY += enemy.transform.position.y + 3;
		transform.position = new Vector3(transform.position.x, newY, transform.position.z);

		transform.Rotate (Vector3.forward * Time.deltaTime * 200.0f);
	}
}
