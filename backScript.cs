using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(Back);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Back()
    {
        SceneManager.LoadScene(0);
    }
}
