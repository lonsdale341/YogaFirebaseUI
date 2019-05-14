using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabPosition : MonoBehaviour
{
    private Vector3 pos;
	// Use this for initialization
	void Start ()
	{
	    pos = gameObject.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    gameObject.transform.localPosition = pos;
	}
}
