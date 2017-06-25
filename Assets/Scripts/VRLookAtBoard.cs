using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class VRLookAtBoard : MonoBehaviour 
{
	public float startTime = 3F;

	Camera mainCamera;
	public Collider boardCol;
	public GameObject canvas;

    [SerializeField]
    private Text timerText;

    public ReactiveProperty<float> timer = new ReactiveProperty<float>();

	// Use this for initialization
	void Start()
	{
        timer.Value = startTime;

        timer.Where( t => t < 0)
             .Subscribe(_ => Destroy(canvas.gameObject, 1F))
             .AddTo(this.gameObject);

        this.UpdateAsObservable()
            .Subscribe( _ => RayCheck())
            .AddTo(this.gameObject);

		mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}

    void RayCheck()
    {
		RaycastHit hit;

        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
		if (Physics.Raycast(ray, out hit, 1000F))
		{
			if (hit.collider == boardCol)
			{
				timer.Value -= Time.deltaTime;
                timerText.text = (timer.Value + 1).ToString("0");
			}
		}
    }
}
