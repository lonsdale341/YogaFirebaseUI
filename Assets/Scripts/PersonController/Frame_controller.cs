using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame_controller : MonoBehaviour
{
    public Transform targetPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        Vector3 rotationVector = new Vector3(targetPosition.eulerAngles.x, targetPosition.eulerAngles.y+90f, targetPosition.eulerAngles.z);
        transform.rotation = Quaternion.Euler(rotationVector);
      
	}
}
