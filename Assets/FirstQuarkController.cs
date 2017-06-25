using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class FirstQuarkController : MonoBehaviour 
{
    float timer = 0F;
    bool timerStart = false;
    float quarkLimit = 3F;

    List<Rigidbody> rigBody = new List<Rigidbody>();

	// Use this for initialization
	void Start () 
    {
        EarthquarkCameraController earthQuarkCamCtr = GameObject.FindWithTag("MainCamera").GetComponent<EarthquarkCameraController>();
        VRLookAtBoard vrBoard = GameObject.Find("Canvas").GetComponent<VRLookAtBoard>();

        rigBody = GameObject.FindGameObjectsWithTag("PhysicsObject")
                            .Select(o => o.GetComponent<Rigidbody>())
                            .ToList();
        rigBody.Select( r => r.isKinematic = true)
               .ToList();

        List<GameObject> lights = GameObject.FindGameObjectsWithTag("Light")
                                            .ToList();

        vrBoard.timer.Where(t => t < 0)
               .Subscribe(_ =>
               {
                   timerStart = true;
                   earthQuarkCamCtr.enabled = true;
                   rigBody.Select(r => r.isKinematic = false)
                          .ToList();
                   Vector3 rigMoveVec = new Vector3(Random.Range(-quarkLimit, quarkLimit)
											, Random.Range(-quarkLimit, quarkLimit)
											, Random.Range(-quarkLimit, quarkLimit)
										   );
                   rigBody.ForEach(r => r.AddForce(rigMoveVec, ForceMode.Impulse));
                lights.ForEach( l => l.SetActive(false));
        } );


	}
	
	// Update is called once per frame
	void Update () 
    {
        if(timerStart)
        {
            timer += Time.deltaTime;
            if(timer > 10F)
            {
                SceneManager.LoadScene("mainStage");
            }
        }
	}
}
