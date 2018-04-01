using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuScript : MonoBehaviour {

    public GameObject response;
    public GameObject backgroundmusic;

    private TypeOutScript responseScript;
    private static menuScript _instance;

    // Use this for initialization
    void Start ()
    {
        backgroundmusic = GameObject.FindGameObjectWithTag("bgm");
        response = GameObject.FindGameObjectWithTag("responseScreen");
        responseScript = response.GetComponent<TypeOutScript>();
        showTitle();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Awake()
    {
        if (!_instance)
            _instance = this;
        else
            Destroy(this.backgroundmusic);

        DontDestroyOnLoad(backgroundmusic);
    }

    void showTitle()
    {
        responseScript.FinalText = "DEATH MAZE";
        responseScript.TotalTypeTime = 2f;
        responseScript.TypeRate = .4f;
        responseScript.On = true;
    }

}
