using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Grabbing : MonoBehaviour
{
    
    public LayerMask grabable;
    public bool isGrabbing = false;
    public GameObject grabed;
    public GameObject inHand;
    public GameObject blur;
    public Transform objPos;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    public List<Item> items = new List<Item>();
    public bool inventaireOn=false;
    public List<Button> icons = new List<Button>();
    private Item prevItem;
    private Item currentItem;
    public GameObject inventaire;
    bool fromInvi=false;
    public bool _using;
    // public Slider zoom;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
       for(int i=0;i<icons.Count;i++)
       {
            UpdateIcons(i);
       }
      

        if(inHand!=null)
        {
            inHand.transform.position = objPos.position;

        }

        if (grabed != null)
        {
            grabed.transform.position = objPos.position;

        }

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        HandleLookAtRay(ray);

        if ( Physics.Raycast(ray, out RaycastHit hit, 3, grabable) && items.Count<7&&!isGrabbing&&!inventaireOn&&!_using)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                inHand=Use(hit.transform.gameObject);
            }
            else if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                addItem(hit.transform.gameObject.GetComponent<Item>());
            }
           
        }
     
        else if (isGrabbing)
        {
           if (Input.GetKeyDown(KeyCode.K))
           {
                HideObject(grabed);
           }
           else if (Input.GetKeyDown(KeyCode.E))
           {
                DropItem(grabed.GetComponent<Item>());
           }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                fromInvi = true;
                _using = true;
               inHand= Use(grabed);
                
            }
        }
        else if(_using && !inventaireOn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                DropObject(inHand);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1)&&items.Count < 7)
            {
                addItem(inHand.GetComponent<Item>());
            }
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inventaireOn)
            {
                OpenInv();
                if (inHand != null&& items.Count >= 7)
                {
                    DropObject(inHand);
                }
                else if(inHand!=null)
                {
                    bool verify = true;
                    for(int i=0;i<items.Count;i++)
                    {
                        if(inHand.GetComponent<Item>()==items[i])
                        {
                            verify = false;
                            break;
                        }
                        
                    }
                    if(verify)
                    {
                        addItem(inHand.GetComponent<Item>());
                        grabed = ShowObject(items[items.Count - 1].gameObject);
                    }
                   
                }
            }
            else
            {
                CloseInv();
            }
        }
    }


    private void HandleLookAtRay(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 3,grabable))
        {
            if (!isGrabbing && !inventaireOn&&!_using)
            {
                currentItem = hit.collider.GetComponent<Item>();

                if (prevItem != currentItem)
                {
                    HideOutline();
                    ShowOutline();
                }
                prevItem = currentItem;
            }
            else
            {
                HideOutline();
            }
        }
        else
        {
            HideOutline();
        }
    }

    private void ShowOutline()
    {
        if (currentItem != null)
        {
            currentItem.ShouOutline();
        }
    }

    private void HideOutline()
    {
        if (prevItem != null)
        {
            prevItem.HideOutline();
            prevItem = null;
        }
    }


    public void OpenInv()
    {
        GetComponent<RigidBodyMouvement>().canMove = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Cursor.lockState = CursorLockMode.None;
        blur.SetActive(true);
        inventaire.SetActive(true);
        Camera.main.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
        inventaireOn = true;
    }
    public void CloseInv()
    {
        if(isGrabbing&&!_using)
        {
            HideObject(grabed);
        }

        GetComponent<RigidBodyMouvement>().canMove = true;   
        Cursor.lockState = CursorLockMode.Locked;
        inventaire.SetActive(false);
        blur.SetActive(false);
        inventaireOn = false;
    }

    public GameObject ShowObject(GameObject grabbed)
    {
        if(isGrabbing)
        {
            HideObject(grabed);
        }
        else if(_using)
        {
            DropObject(inHand);
        }
        grabbed.SetActive(true);
        
        grabbed.transform.position = objPos.position;
        grabbed.GetComponent<Rigidbody>().isKinematic = true;
        grabbed.GetComponent<Collider>().isTrigger = true;
        isGrabbing = true;
        return grabbed;
    }

    public void PuzzlePiece(GameObject grabbed)
    {
        grabbed.SetActive(true);
        grabbed.transform.position = objPos.position;
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
        if(_using)
        {
            _using = false;
        }
        item.gameObject.transform.parent = null;
        item.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        item.gameObject.GetComponent<Collider>().isTrigger = true;
        item.gameObject.SetActive(false);        
    }

    public void DropItem(Item item)
    {
        item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        item.gameObject.GetComponent<Collider>().isTrigger = false;
        item.gameObject.SetActive(true);
        grabed = null;
        isGrabbing = false;
       
        items.Remove(item);

       
    }

    public void DropObject(GameObject grabbed)
    {
        grabbed.GetComponent<Rigidbody>().isKinematic = false;
        grabbed.GetComponent<Collider>().isTrigger = false;
        grabbed.transform.parent = null;
        inHand = null;
        _using = false;
    }
  
    public void UpdateIcons(int i)
    {
        if(items.Count>i)
        { 
            icons[i].image.sprite = items[i].icon;
        }
        else
        {
            icons[i].image.sprite = null;
        }
     
    }

    public void ActiveObject(int i)
    {
        if (items.Count > i&&!isGrabbing)
        {
            grabed = ShowObject(items[i].gameObject);
        }
        else if(items.Count-1<i)
        {
            HideObject(grabed);
        }
        else
        {
            Puzzled(i);
        }

       
       
    }

    public GameObject Use(GameObject grabbed)
    {
        if (_using&&!fromInvi)
        {
            DropObject(inHand);
        }
        if(isGrabbing)
        {
          DropItem(grabed.GetComponent<Item>());
            
         
        }
        grabbed.transform.SetParent(Camera.main.transform);
        grabbed.GetComponent<Rigidbody>().isKinematic = true;
        grabbed.GetComponent<Collider>().isTrigger = true;
        grabbed.transform.position = objPos.position;
        _using = true;
        fromInvi = false;
        return grabbed;
        
    }

    public void Puzzled(int i)
    {
        if (items.Count > i)
        {
            if (grabed.GetComponent<Item>().inPuzzle)
            {
                Item mbPiece = items[i];
               
                
                    if (mbPiece.gameObject.name == grabed.GetComponent<Item>().pusslePiece.gameObject.name|| mbPiece.gameObject.name == grabed.GetComponent<Item>().pusslePiece.gameObject.name+"(Clone)")
                    {

                        GameObject thePiece = Instantiate(grabed.GetComponent<Item>().instance, new Vector3(1000, 1000, 1000), Quaternion.identity);
                        
                        GameObject destroyed = grabed;
                        GameObject destroyed2 = items[i].gameObject;
                        DropItem(grabed.GetComponent<Item>());
                        DropItem(mbPiece);
                        Destroy(destroyed);
                        Destroy(destroyed2);
                        addItem(thePiece.GetComponent<Item>());
                        grabed = ShowObject(thePiece);
                        inHand = null;

                    }
                    else
                    {
                        grabed = ShowObject(items[i].gameObject);
                    }


              
            }
            else
            {
                grabed = ShowObject(items[i].gameObject);
            }
        }
           
    }
   
}
