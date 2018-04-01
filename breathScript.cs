using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breathScript : MonoBehaviour {

    private readonly int totalBreath = 1;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void updateBreathBar(float breathValue)
    {
        transform.localScale = new Vector3((breathValue / totalBreath), 1, 1);
    }
}
