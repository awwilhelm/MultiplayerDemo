using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour
{

    private float lookSensitivity = 5.0f;
    private float lookSmoothDamp = 0.08f;

    private Vector2 thisRotation = Vector2.zero;
    private Vector2 currRotation = Vector2.zero;
    private Vector2 thisRotationV = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        thisRotation.x -= Input.GetAxis("Mouse Y") * lookSensitivity;
        thisRotation.y += Input.GetAxis("Mouse X") * lookSensitivity;

        thisRotation.x = Mathf.Clamp(thisRotation.x, -90, 90);

        currRotation.x = Mathf.SmoothDamp(currRotation.x, thisRotation.x, ref thisRotationV.x, lookSmoothDamp);
        currRotation.y = Mathf.SmoothDamp(currRotation.y, thisRotation.y, ref thisRotationV.y, lookSmoothDamp);

        transform.rotation = Quaternion.Euler(currRotation.x, currRotation.y, 0);
        transform.parent.rotation = Quaternion.Euler(0, currRotation.y, 0);

        //transform.rotation = Quaternion.Euler(currRotation.x, 0, 0);
        //transform.parent.Find("Mesh").rotation = Quaternion.Euler(0, currRotation.y, 0);
    }
}
