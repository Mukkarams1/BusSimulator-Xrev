using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation1 : MonoBehaviour {

	public bool x, y, z;
	
	public float speed = 5f;
	void Start () {
		
	}
	
	
	void Update () {
		if (x)
		{
			transform.Rotate(speed * Time.deltaTime,0,0);
		}
		if (y)
		{
			transform.Rotate(0, speed * Time.deltaTime, 0);
		}
		if (z)
		{
			transform.Rotate(0, 0, speed * Time.deltaTime);
		}
	}
}
