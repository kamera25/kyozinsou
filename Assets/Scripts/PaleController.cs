using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaleController : MonoBehaviour {

    float time = 0;
    Rigidbody rig;

	// Use this for initialization
	void Start () 
    {
        rig = this.GetComponent<Rigidbody>();
        time = Random.Range(0F, 10F);
	}
	
	// Update is called once per frame
	void Update () 
    {
        time -= Time.deltaTime;
        if( time < 0F)
        {
            //rig.AddForce(Vector3.up * 5F, ForceMode.Impulse);
            rig.AddTorque( new Vector3( Random.Range(0F,20F), Random.Range(0F, 20F), Random.Range(0F, 20F)), ForceMode.Impulse);
            this.enabled = false;
        }
	}
}
