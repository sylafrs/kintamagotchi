using UnityEngine;
using System.Collections;

public class InteractionManager : MonoBehaviour
{
	private Vector3 test;

	private GameObject __objectTouched;

	public static InteractionManager instance { get; private set; }

	public void Awake()
	{
		instance = this;
	}

	public void Update()
	{
#if !FORCE_MOUSE
		if (Input.touchSupported)
		{
			if (Input.touchCount > 0)
			{
				Touch t = Input.GetTouch(0);
				Debug.Log(t.phase);
				if (t.phase == TouchPhase.Began)
					GetObjectTouched(t.position);
				else if (t.phase == TouchPhase.Moved)
					Moved(t.position);
				else if (t.phase == TouchPhase.Ended)
					Tapped(t.position);
			}
		}
		else
#endif
		{
			if (Input.GetMouseButtonDown(0))
			{
				Tapped(Input.mousePosition);
			}
		}
	}

	void Tapped(Vector3 mousePos)
	{
		mousePos.z = this.camera.nearClipPlane;
		mousePos = this.camera.ScreenToWorldPoint(mousePos);

		Ray ray = new Ray(this.transform.position, mousePos - this.transform.position);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			test = hit.point;
			hit.collider.gameObject.SendMessage("OnTapped", hit.point, SendMessageOptions.DontRequireReceiver);
		}
	}

	void Moved(Vector3 pMousePos)
	{
		if (__objectTouched)
		{
			pMousePos.z = __objectTouched.transform.position.z;
			pMousePos = this.camera.ScreenToWorldPoint(pMousePos);
			//GameObject.FindGameObjectWithTag("Player").SendMessage("OnTapped", Vector3.zero, SendMessageOptions.DontRequireReceiver);
			__objectTouched.SendMessage("OnMoved", pMousePos, SendMessageOptions.DontRequireReceiver);
		}
	}

	void GetObjectTouched(Vector3 pMousePos)
	{
		pMousePos.z = this.camera.nearClipPlane;
		pMousePos = this.camera.ScreenToWorldPoint(pMousePos);

		Ray ray = new Ray(this.transform.position, pMousePos - this.transform.position);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
			__objectTouched = hit.collider.gameObject;
		else
			__objectTouched = null;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(test, 1);
	}
}
