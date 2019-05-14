using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerActionButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (CommonData.StateFade==0)
	    {
	        transform.localScale=new Vector3(0,0,0);
	    }
        if (CommonData.StateFade == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
	}
}
