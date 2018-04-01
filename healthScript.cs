using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthScript : MonoBehaviour {

    private int totalHealth= 1;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void updateHealthBar(float healthValue)
    {
        transform.localScale = new Vector3((healthValue / totalHealth), 1, 1);
    }
}
