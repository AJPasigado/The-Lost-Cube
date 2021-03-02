/* 
 * ANTON JOHN B. PASIGADO
 * ComSc 4A
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject cube;
	public GameObject Canvas;
	public GameObject HUD;
	public int life;

	public static GameManager instance = null;

	public void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
        DontDestroyOnLoad(Canvas);
    }

	// Use this for initialization
	void Start () {
		for (int i = 0; i < instance.life; i++) {
			Instantiate (cube, HUD.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 5);
	}
}
