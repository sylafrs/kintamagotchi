﻿using UnityEngine;
using System.Collections;

public class InteractionManager : MonoBehaviour
{
	public Vector3 test;

	private GameObject __objectTouched;

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
				GetObjectTouched(Input.mousePosition);
			}
			else if (Input.GetMouseButtonUp(0))
				Tapped(Input.mousePosition);
			else if (Input.GetMouseButton(0))
				Moved(Input.mousePosition);
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
			pMousePos.z = this.camera.nearClipPlane;
			pMousePos = this.camera.ScreenToWorldPoint(pMousePos);

			Ray ray = new Ray(this.transform.position, pMousePos - this.transform.position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, this.camera.farClipPlane, ~(1 << 8)))
			{
				test = hit.point;
				__objectTouched.SendMessage("OnMoved", hit.point, SendMessageOptions.DontRequireReceiver);
			}
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
