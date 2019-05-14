using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame_Rotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rotationVector = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
        transform.rotation = Quaternion.Euler(rotationVector);
	}
}
