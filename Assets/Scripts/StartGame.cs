/* 
 * ANTON JOHN B. PASIGADO
 * ComSc 4A
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {
    public AudioSource backgroundMusc;
	// Use this for initialization
	void Start () {
        backgroundMusc.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 6 ||
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 7) {
            transform.Rotate(Vector3.up * 0.5f);
        } 
    }

	public void LoadLevel() {
		string nextPath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex (1);
		Initiate.Fade (nextPath, Color.black, 1.0f);
        GameManager.instance.Canvas.SetActive(true);
    }

	public void StartLevel() {
		string nextPath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex (0);
		Initiate.Fade (nextPath, Color.black, 1.0f);
    }
}
