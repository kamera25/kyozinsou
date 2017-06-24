using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TsunamiController : MonoBehaviour 
{
    [SerializeField]
    float upDamp = 0.8F;

    Transform thisTrans;

    void Start()
    {
        thisTrans = this.transform;

        this.UpdateAsObservable()
            .Subscribe( _=> thisTrans.position += Vector3.up * upDamp * Time.deltaTime)
            .AddTo(this.gameObject);
    }

}
