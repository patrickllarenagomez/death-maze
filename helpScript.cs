using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class helpScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadHelp);
    }
	
	// Update is called once per frame
	void Update ()
    {

    }   

    void LoadHelp()
    {
        SceneManager.LoadScene(1);
    }
}
