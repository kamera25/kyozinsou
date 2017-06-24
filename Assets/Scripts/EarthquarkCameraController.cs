using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EarthquarkCameraController : MonoBehaviour 
{
    [SerializeField]
    float fixEQTime = 10F;

    [SerializeField]
    float quarkLimit = 0.4F;

    ReactiveProperty<float> nextEarthQuark = new ReactiveProperty<float>();
    Transform thisTransform;

    Vector3 nowCamMovePos = Vector3.zero;
    Vector3 camMoveVec = Vector3.zero;
    Vector3 originCamPos;

	// Use this for initialization
	void Start () 
    {
        thisTransform = this.transform;
        nextEarthQuark.Value = fixEQTime;
        originCamPos = thisTransform.position;

        nextEarthQuark.Where( t => t < 0)
                      .Subscribe( _ => StartCoroutine("occurEarthQuark"))
                      .AddTo(this.gameObject);

		this.UpdateAsObservable()
            .Where( _ => nextEarthQuark.Value > 0)
	        .Subscribe(_ => nextEarthQuark.Value -= Time.deltaTime)
	        .AddTo(this.gameObject);

        this.UpdateAsObservable()
            .Subscribe( _ => SetCameraPos())
            .AddTo(this.gameObject);
	}

    void SetCameraPos()
    {
        thisTransform.position -= nowCamMovePos;
        nowCamMovePos = Vector3.Lerp( nowCamMovePos, camMoveVec, Time.deltaTime * 5F);
        thisTransform.position += nowCamMovePos;
    }

    IEnumerator occurEarthQuark()
    {

        for (int i = 0; i < 20; i++)
        {
            camMoveVec = new Vector3(   Random.Range(-quarkLimit, quarkLimit)
                                     ,  Random.Range(-quarkLimit, quarkLimit)
                                     ,  Random.Range(-quarkLimit, quarkLimit)
                                    ); 
            yield return new WaitForSeconds(0.05F);
        }

        camMoveVec = Vector3.zero;
        nextEarthQuark.Value = Random.Range(0, 5) + fixEQTime;
    }

}
