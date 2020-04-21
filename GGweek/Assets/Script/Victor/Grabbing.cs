using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Grabbing : MonoBehaviour
{
   
    public LayerMask grabable;
    public bool isGrabbing=false;
    public GameObject grabed;
    public GameObject blur;
    public Transform objPos;
    private float xRotation = 0f;
    private float zRotation = 0f;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
   // public Slider zoom;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
       // Camera.main.fieldOfView = zoom.value;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        if(Input.GetKeyDown(KeyCode.Mouse0)&& Physics.Raycast(ray, out RaycastHit hit, 3, grabable)&&!isGrabbing)
        {
            blur.SetActive(true);
            grabed = Grab(hit.transform.gameObject);
        }

        if(isGrabbing&&Input.GetKeyDown(KeyCode.Escape))
        {
            blur.SetActive(false);
            Drop(grabed);
        }
        float mouseX = Input.GetAxis("Mouse X") * 20;
        float mouseY = Input.GetAxis("Mouse Y") * 20;

        if (isGrabbing)
        {
           

            if (Input.GetKey(KeyCode.Mouse0))
            {
                mPosDelta = Input.mousePosition - mPrevPos;
                if(Vector3.Dot(transform.up,Vector3.up)>=0)
                {
                    grabed.transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
                }
                else
                {
                    grabed.transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
                }

                grabed.transform.Rotate(Camera.main.transform.right,Vector3.Dot(mPosDelta,Camera.main.transform.up),Space.World);
                //xRotation = mouseY;
                //zRotation = mouseX;
               // grabed.transform.localRotation = Quaternion.Euler(xRotation, 0f,zRotation);
               
               

            }
            mPrevPos = Input.mousePosition;
        }
    }

    public GameObject Grab(GameObject grabbed)
    {
        GetComponent<RigidBodyMouvement>().canMove = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Cursor.lockState = CursorLockMode.None;
        grabbed.GetComponent<Rigidbody>().isKinematic = true;
        grabbed.GetComponent<Collider>().isTrigger = true;
        Camera.main.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
       // grabbed.transform.parent = Camera.main.transform;
        grabbed.transform.position = objPos.position;
      //  grabbed.transform.localPosition = new Vector3(0, grabbed.transform.localPosition.y, 1);
        isGrabbing = true;
        return grabbed;
    }

    public void Drop(GameObject grabbed)
    {
        GetComponent<RigidBodyMouvement>().canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
       // grabbed.transform.parent = null;
        grabbed.GetComponent<Rigidbody>().isKinematic = false;
        grabbed.GetComponent<Collider>().isTrigger = false;
        isGrabbing = false;
       // zoom.value = 60;
    }
}
