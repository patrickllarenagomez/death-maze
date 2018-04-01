using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour {

    GameObject spear;
    Animation spearAnim;
    float origin;    
	// Use this for initialization
	void Start () {
        spear = GameObject.FindGameObjectWithTag("spearTrap");
        spearAnim = spear.GetComponent<Animation>();
        origin = transform.position.z;
    }

    // Update is called once per frame
    void Update ()
    {
        var zposition = transform.position.z;

        if (Mathf.Abs(origin - zposition) > 5f)
            spearAnim.Stop();
        spearAnim.Play();


    }


}
