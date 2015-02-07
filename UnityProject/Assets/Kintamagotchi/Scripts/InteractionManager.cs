using UnityEngine;
using System.Collections;

public class InteractionManager : MonoBehaviour 
{
	public Vector3 test;

	public void Update()
	{
#if !FORCE_MOUSE
		if (Input.touchSupported)
		{
			if (Input.touchCount > 0)
			{
				Touch t = Input.GetTouch(0);
				Debug.Log(t.phase);
				if (t.phase == TouchPhase.Ended)
				{
					Tapped(t.position);
				}
			}
		}
		else
#endif
		{
			if(Input.GetMouseButtonDown(0))
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

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(test, 1);
	}
}
