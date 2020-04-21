
//NORMAL//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDPlayerLooking : MonoBehaviour
{
  
    public float mouseSnesy = 100f;
    public Transform playerBody;
    private float xRotation = 0f;
    public float minRotationHor;
    public float MaxRotationHor;
    public bool grabbing;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

   


    void Update()
    {
        grabbing = GetComponentInParent<Grabbing>().isGrabbing;


        if (Time.timeScale != 0.0f&&!grabbing)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSnesy;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSnesy;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);


            
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
       
    }
}

