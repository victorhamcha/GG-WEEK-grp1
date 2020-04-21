using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Grabbing : MonoBehaviour
{
    
    public LayerMask grabable;
    public bool isGrabbing = false;
    private GameObject grabed;
    public GameObject blur;
    public Transform objPos;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    public List<Item> items = new List<Item>();
    private bool inventaireOn=false;
    public List<Button> icons = new List<Button>();
    // public Slider zoom;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        for(int i =0;i<items.Count;i++)
        {
            
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!inventaireOn)
            {
                OpenInv();
            }
            else
            {
                CloseInv();
            }
        }
        // Camera.main.fieldOfView = zoom.value;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);


        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, out RaycastHit hit, 3, grabable) && items.Count<7)
        {
            
            addItem(hit.transform.gameObject.GetComponent<Item>());
        }
        //if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, out RaycastHit hit, 3, grabable) && !isGrabbing)
        //{
        //    blur.SetActive(true);
        //    grabed = Grab(hit.transform.gameObject);
        //}

        if (isGrabbing && Input.GetKeyDown(KeyCode.K))
        {
           
            HideObject(grabed);
          
        }
        float mouseX = Input.GetAxis("Mouse X") * 20;
        float mouseY = Input.GetAxis("Mouse Y") * 20;

        if (isGrabbing)
        {


            if (Input.GetKey(KeyCode.Mouse0))
            {
                mPosDelta = Input.mousePosition - mPrevPos;
                if (Vector3.Dot(transform.up, Vector3.up) >= 0)
                {
                    grabed.transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
                }
                else
                {
                    grabed.transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
                }

                grabed.transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up), Space.World);
                //xRotation = mouseY;
                //zRotation = mouseX;
                // grabed.transform.localRotation = Quaternion.Euler(xRotation, 0f,zRotation);



            }
            mPrevPos = Input.mousePosition;
        }
    }


    public void OpenInv()
    {
        GetComponent<RigidBodyMouvement>().canMove = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Cursor.lockState = CursorLockMode.None;
        blur.SetActive(true);
        Camera.main.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
        inventaireOn = true;
    }
    public void CloseInv()
    {
        GetComponent<RigidBodyMouvement>().canMove = true;   
        Cursor.lockState = CursorLockMode.Locked;
        blur.SetActive(false);
        inventaireOn = false;
    }

    public GameObject ShowObject(GameObject grabbed)
    {
        if(isGrabbing)
        {
            HideObject(grabed);
        }
        grabbed.SetActive(true);
        grabbed.transform.position = objPos.position;
        isGrabbing = true;
        return grabbed;
    }


    public void HideObject(GameObject grabbed)
    {
        grabbed.SetActive(false);
        //grabbed.GetComponent<Rigidbody>().isKinematic = false;
        //grabbed.GetComponent<Collider>().isTrigger = false;
        isGrabbing = false;
    }

    public void addItem(Item item)
    {
        items.Add(item);
        item.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        item.gameObject.GetComponent<Collider>().isTrigger = true;
        item.gameObject.SetActive(false);
        UpdateIcons(item);
    }

    public void DropItem(Item item)
    {
        item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        item.gameObject.GetComponent<Collider>().isTrigger = false;
        item.gameObject.SetActive(true);
        items.Remove(item);
        UpdateIcons(item);
       
    }
  
    public void UpdateIcons(Item item)
    {
       //icons[items.Count-1].sprite=item.icon;
    }

    public void ActivObject(int i)
    {
       grabed= ShowObject(items[i].gameObject);
    }
}
