using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame_Position : MonoBehaviour {
    public Transform targetPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(targetPosition.position.x, targetPosition.position.y, targetPosition.position.z);
      
	}
}
