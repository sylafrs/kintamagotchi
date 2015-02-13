using UnityEngine;
using System.Collections;
using System;

public enum InteractionType
{
	MOVED,
	TAPPED
}

public class InteractionManager : MonoBehaviour
{
	private Vector3 gizmosPosition;

	private GameObject __objectTouched;

	private Vector3 prevMousePos;
	private bool moved;
	public float SqrTapPrecision = 0.01f;
	private float Timer = 0;
	public float MaxTapTime = 0.3f;

	public static event Action<InteractionType> OnInteraction;

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
				{
					Timer = 0;
					moved = false;
					GetObjectTouched(t.position);
				}
				else
				{
					Timer += Time.deltaTime;
					moved = moved || t.deltaPosition.sqrMagnitude > SqrTapPrecision
								  || Timer > MaxTapTime;
					
					if (t.phase == TouchPhase.Ended && !moved)
					{
						Tapped(t.position);
					}
					else if (t.phase == TouchPhase.Moved && moved)
					{
						Moved(t.position);
					}
				}
			}
		}
		else
#endif
		{
			if (Input.GetMouseButtonDown(0))
			{
				moved = false;
				Timer = 0;
				GetObjectTouched(Input.mousePosition);
			}
			else
			{
				Timer += Time.deltaTime;
				moved = moved || (prevMousePos - Input.mousePosition).sqrMagnitude > SqrTapPrecision
							  || Timer > MaxTapTime;
				
				if (Input.GetMouseButtonUp(0) && !moved)
					Tapped(Input.mousePosition);
				else if (Input.GetMouseButton(0) && moved)
					Moved(Input.mousePosition);
			}

			prevMousePos = Input.mousePosition;
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
				gizmosPosition = hit.point;

				if (__objectTouched == hit.collider.gameObject)
				{
					hit.collider.gameObject.SendMessage("OnTapped", hit.point, SendMessageOptions.DontRequireReceiver);
					
					if (OnInteraction != null)
						OnInteraction(InteractionType.TAPPED);
				}
			}
	}

	void Moved(Vector3 pMousePos)
	{
		if (__objectTouched)
		{
			pMousePos.z = this.camera.nearClipPlane;
			pMousePos = this.camera.ScreenToWorldPoint(pMousePos);

			Ray ray = new Ray(this.transform.position, pMousePos - this.transform.position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, this.camera.farClipPlane, ~(1<<8)))
			{
				Vector3 point = hit.point;
				point.y = 0;

				gizmosPosition = point;
				//if (hit.transform.CompareTag("Ground"))
				//{
					__objectTouched.SendMessage("OnMoved", point, SendMessageOptions.DontRequireReceiver);
			
					if (OnInteraction != null)
						OnInteraction(InteractionType.MOVED);
				//}
			}
		}
	}

	public GameObject FindObjectTouched(Vector3 pMousePos)
	{
		GameObject ret = null;

		pMousePos.z = this.camera.nearClipPlane;
		pMousePos = this.camera.ScreenToWorldPoint(pMousePos);

		Ray ray = new Ray(this.transform.position, pMousePos - this.transform.position);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
			ret = hit.collider.gameObject;
		else
			ret = null;
		return ret;
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
		Gizmos.DrawSphere(gizmosPosition, 0.1f);
	}
}
