using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsScript : MonoBehaviour {

    // Use this for initialization

    public GameObject conqueredText;
    public GameObject thankText;
    public GameObject developerText;

    private TypeOutScript conquerScript;
    private TypeOutScript thankScript;
    private TypeOutScript developerScript;

    void Start ()
    {
        conquerScript = conqueredText.GetComponent<TypeOutScript>();
        thankScript = thankText.GetComponent<TypeOutScript>();
        developerScript = developerText.GetComponent<TypeOutScript>();
        showTexts();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    void showTexts()
    {

        conquerScript.FinalText = "YOU HAVE CONQUERED \n THE DEATH MAZE!";
        conquerScript.TotalTypeTime = 2f;
        conquerScript.TypeRate = .4f;
        conquerScript.On = true;

        StartCoroutine(waitTwoSeconds());



    }

    IEnumerator waitTwoSeconds()
    {
        yield return new WaitForSeconds(2);
        thankScript.FinalText = "THANK YOU FOR PLAYING!";
        thankScript.TotalTypeTime = 2f;
        thankScript.TypeRate = .4f;
        thankScript.On = true;

        StartCoroutine(waitTwoMoreSeconds());
    }


    IEnumerator waitTwoMoreSeconds()
    {
        yield return new WaitForSeconds(2);
        developerScript.FinalText = "DEVELOPED BY: \n MARC RENDELL CHING \n PATRICK GOMEZ";
        developerScript.TotalTypeTime = 2f;
        developerScript.TypeRate = .4f;
        developerScript.On = true;
    }
}
