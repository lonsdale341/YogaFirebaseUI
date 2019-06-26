using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public bool isPose;
    public bool noVert;
    public GameObject Girl;
    public float Speed = 0.1f;
    Vector3 CentreAround;
    public GameObject MHuman_Pivot;
    private float Y_pos;
    private float Scale_Lim;
    void Start()
    {
        Debug.Log(Girl.transform.up);
        if (Girl != null)
        {
            Y_pos = Girl.transform.localPosition.y;
            Scale_Lim = Girl.transform.localScale.x;
            Debug.Log(Y_pos);
        }

    }
    void Update()
    {
        if (isPose && Girl != null && MHuman_Pivot != null)
        {
            CentreAround = new Vector3(MHuman_Pivot.transform.position.x, Y_pos, MHuman_Pivot.transform.position.z);
            if (Girl.transform.localScale.x > 2f*Scale_Lim)
            {
                Girl.transform.localScale = new Vector3(1.25f * Scale_Lim, 1.25f * Scale_Lim, 1.25f * Scale_Lim);
            }
            if (Girl.transform.localScale.x < 0.25f * Scale_Lim)
            {
                Girl.transform.localScale = new Vector3(0.75f * Scale_Lim, 0.75f * Scale_Lim, 0.75f * Scale_Lim);
            }
            if (Girl.transform.eulerAngles.z <-90)
            {
               // Girl.transform.eulerAngles = new Vector3(Girl.transform.eulerAngles.x, Girl.transform.eulerAngles.y, -90);
            }
            if (Girl.transform.eulerAngles.z >90)
            {
               // Girl.transform.eulerAngles = new Vector3(Girl.transform.eulerAngles.x, Girl.transform.eulerAngles.y, 90);
            }
            Girl.transform.localPosition = new Vector3(0, -Y_pos, 0);
        }
#if ((UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR)

        //Touch
        int touchCount = Input.touchCount;
        
        if (touchCount == 1)
        {
            
            Touch t = Input.GetTouch(0);
            if(EventSystem.current.IsPointerOverGameObject(t.fingerId))return;
            
            switch (t.phase)
            {
            case TouchPhase.Moved:
                
                float xAngle = t.deltaPosition.y * Speed;
                float yAngle = -t.deltaPosition.x * Speed;
                float zAngle = 0;
                
              //  Cube.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
               if (Girl!=null)
            {
               // Girl.transform.RotateAround(MHuman_Pivot.transform.position, Girl.transform.up, -yAngle);
                Girl.transform.RotateAround(Girl.transform.position, Girl.transform.up, -yAngle);
                Girl.transform.RotateAround(Girl.transform.position, Girl.transform.forward, xAngle);
               // Girl.transform.Rotate(zAngle, yAngle, xAngle, Space.World);
            }
                break;
            }
            
        }

#else
        //Mouse
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            float xAngle = Input.GetAxis("Mouse Y") * Speed * 80;
            float yAngle = -Input.GetAxis("Mouse X") * Speed * 80;
            float zAngle = 0;

            // Cube.transform.Rotate (xAngle, yAngle, zAngle, Space.World);
            if (Girl != null)
            {
               // Girl.transform.RotateAround(MHuman_Pivot.transform.position, Girl.transform.up, -yAngle);

                Girl.transform.RotateAround(Girl.transform.position, Girl.transform.up, -yAngle);
                if(!noVert)
                {
                    Girl.transform.RotateAround(Girl.transform.position, Girl.transform.forward, xAngle);
                }
               // 

               // Girl.transform.Rotate(zAngle, yAngle, xAngle, Space.World);

            }
        }
#endif
    }

    public void ResetPosition()
    {
        if (Girl != null)
        {
            
            Girl.transform.localEulerAngles=new Vector3(0,0,0);
            Girl.transform.localScale = new Vector3( Scale_Lim,  Scale_Lim, Scale_Lim);
        }
    }
}
