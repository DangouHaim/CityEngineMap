using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class KeyPress : MonoBehaviour {
    private AsyncOperation _load;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey(KeyCode.G))
        {
            _load = SceneManager.LoadSceneAsync("StartCity");
        }
	}
}
