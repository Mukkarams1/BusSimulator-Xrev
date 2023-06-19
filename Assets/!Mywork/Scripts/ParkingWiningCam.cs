using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingWiningCam : MonoBehaviour
{

	public Transform Target;
	public float speed;
	void Update()
	{
			transform.LookAt(Target.transform);
			transform.Translate(Vector3.right * Time.deltaTime * speed);
	}
}
