using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerEvent_Muscule : MonoBehaviour {

	// Use this for initialization

	void Start () {
      // foreach (MaterialEntry entry in CommonData.prefabs.objectMaterials)
      // {
      //     foreach (Material materialEntry in entry.Materials)
      //     {
      //         materialEntry.SetColor("_Color", new Vector4(1, 1f, 1f, 1f));
      //     }
      // }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GetAnimationEvent(string message)
    {
        CommonData.AnimationCurrent = message;
        foreach (MaterialEntry entry in CommonData.prefabs.objectMaterials)
        {
            foreach (Material materialEntry in entry.Materials)
            {
                materialEntry.SetColor("_Color", new Vector4(1, 1f, 1f, 1f));
            }
        }
        var items = CommonData.prefabs.objectMaterials.Where(item => item.nameAnimation == CommonData.AnimationCurrent);
        foreach (MaterialEntry entry in items)
        {
            foreach (Material materialEntry in entry.Materials)
            {
                materialEntry.SetColor("_Color", new Vector4(0, 7f / 255f, 231f / 255f, 1f));
            }
        }
     
       Debug.Log(message);

    }

    public void GetAnimationEvent_Label(string message)
    {

    }
}
