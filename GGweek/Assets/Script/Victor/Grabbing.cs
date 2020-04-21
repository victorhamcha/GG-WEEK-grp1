using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grabbing : MonoBehaviour
{
   
    public LayerMask grabable;
    public bool isGrabbing=false;
    public GameObject grabed;
    public GameObject blur;
    public Transform objPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        if(Input.GetKeyDown(KeyCode.Mouse0)&& Physics.Raycast(ray, out RaycastHit hit, 8, grabable)&&!isGrabbing)
        {
            blur.SetActive(true);
            grabed = Grab(hit.transform.gameObject);
        }

        if(isGrabbing&&Input.GetKeyDown(KeyCode.Escape))
        {
            blur.SetActive(false);
            Drop(grabed);
        }
    }

    public GameObject Grab(GameObject grabbed)
    {
        Cursor.lockState = CursorLockMode.None;
        Camera.main.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
        grabbed.transform.parent = Camera.main.transform;
        grabbed.transform.position = new Vector3(0, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, blur.transform.position.z);
        grabbed.transform.localPosition = new Vector3(0, grabbed.transform.localPosition.y, 1);
      
        grabbed.GetComponent<Rigidbody>().isKinematic = true;
        grabbed.GetComponent<Collider>().isTrigger = true;
        isGrabbing = true;
        return grabbed;
    }

    public void Drop(GameObject grabbed)
    {
        Cursor.lockState = CursorLockMode.Locked;
        grabbed.transform.parent = null;
        grabbed.GetComponent<Rigidbody>().isKinematic = false;
        grabbed.GetComponent<Collider>().isTrigger = false;
        isGrabbing = false;
    }
}
