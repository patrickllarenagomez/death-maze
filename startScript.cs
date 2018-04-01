using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class startScript : MonoBehaviour {

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadStart);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadStart()
    {
        SceneManager.LoadScene(2);
    }
}
