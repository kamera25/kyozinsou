using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using System.Linq;
using UniRx;

public class NPCController : IHuman
{
    enum THINK
    {
        STOP = 0,
        RUN,
        HEAR
    }

    NavMeshAgent navAgent; 

    Vector3 moveVec;
    bool knowFinalEscapePoint = false;
    ReactiveProperty<Transform> aimPoint = new ReactiveProperty<Transform>();
    ReactiveProperty<THINK> think = new ReactiveProperty<THINK>(THINK.RUN);

    static List<Transform> escapePoint = new List<Transform>();
    Transform finalEscapePoint;

	// Use this for initialization
	void Start () 
    {
        escapePoint = GameObject.FindGameObjectsWithTag("EscapePoint")
                                .Select( t => t.transform)
                                .ToList();
        finalEscapePoint = GameObject.Find("FinalEscapePoint").transform;

        aimPoint.Value = escapePoint[0];

        navAgent = this.GetComponent<NavMeshAgent>();
        aimPoint.Subscribe( t => navAgent.destination = t.position);

        think.Subscribe( _ => ChangedThink());
	}
	
	// Update is called once per frame
	void Update () 
    {
        //think.Value = THINK.HEAR;
	}

    void ChangedThink()
    {
        switch(think.Value)
        {
            case THINK.STOP:
                aimPoint.Value = this.transform;
                break;
            case THINK.HEAR:
                think.Value = THINK.STOP;
                Observable.Timer(TimeSpan.FromSeconds(5))
                          .Subscribe(_ => {
                              think.Value = THINK.RUN;
                              aimPoint.Value = finalEscapePoint;
                              knowFinalEscapePoint = true;
                }).AddTo(this.gameObject);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Speaker"))
        {
            think.Value = THINK.HEAR;
        }
    }
}
