using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour 
{
    [SerializeField]
    Transform waterTrans;
    Transform mainCameraTrans;

    void Start()
    {
        mainCameraTrans = GameObject.FindWithTag("MainCamera").transform;
    }

	// Use this for initialization
	void Update () 
    {
        if( mainCameraTrans.position.y - waterTrans.position.y < 0.2F)
        {
            SceneManager.LoadScene("mainStage");
        }
	}
	
}
