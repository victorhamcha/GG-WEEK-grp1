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
    public GameObject blur;
    public Transform objPos;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    public List<Item> items = new List<Item>();
    private bool inventaireOn=false;
    public List<Button> icons = new List<Button>();
    public List<GameObject> button = new List<GameObject>();
    private Item prevItem;
    private Item currentItem;
    private int puzzling = 0;

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
      
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        HandleLookAtRay(ray);

        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, out RaycastHit hit, 3, grabable) && items.Count<7&&!isGrabbing&&!inventaireOn)
        {
            
            addItem(hit.transform.gameObject.GetComponent<Item>());
        }
     

        if (isGrabbing)
        {
           if (Input.GetKeyDown(KeyCode.K))
           {
                HideObject(grabed);
           }
           else if (Input.GetKeyDown(KeyCode.F))
           {
                DropItem(grabed.GetComponent<Item>());
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
    }


    private void HandleLookAtRay(Ray ray)
    {
      
       

        if (Physics.Raycast(ray, out RaycastHit hit, 3,grabable))
        {
            if (!isGrabbing && !inventaireOn)
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
        Camera.main.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
        inventaireOn = true;
    }
    public void CloseInv()
    {
        if(isGrabbing)
        {
            HideObject(grabed);
        }
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
        else
        {
            Puzzled(i);
        }

       
       
    }

    public void Puzzled(int i)
    {
        if (items.Count > i)
        {
            if (grabed.GetComponent<Item>().inPuzzle)
            {
                Item mbPiece = items[i];
                bool puzzled = false;
                for (int j = 0; j < grabed.GetComponent<Item>().pusslePiece.Count; j++)
                {
                    if (mbPiece == grabed.GetComponent<Item>().pusslePiece[j])
                    {

                        puzzled = true;
                        break;
                    }
                }
                if (puzzled)
                {
                    PuzzlePiece(mbPiece.gameObject);
                    puzzling += 1;
                }
                else
                {
                    puzzling = 0;
                    for (int j = 0; j < grabed.GetComponent<Item>().pusslePiece.Count; j++)
                    {
                        grabed.GetComponent<Item>().pusslePiece[j].gameObject.SetActive(false);
                    }
                    grabed = ShowObject(items[i].gameObject);
                }
                if (puzzling == grabed.GetComponent<Item>().pusslePiece.Count)
                {

                    GameObject thePiece = Instantiate(grabed.GetComponent<Item>().instance, new Vector3(1000, 1000, 1000), Quaternion.identity);
                    List<Item> destroyItem = new List<Item>();
                    for (int j = 0; j < grabed.GetComponent<Item>().pusslePiece.Count; j++)
                    {
                        destroyItem.Add(grabed.GetComponent<Item>().pusslePiece[j]);
                        DropItem(grabed.GetComponent<Item>().pusslePiece[j]);
                        Destroy(destroyItem[j].gameObject);
                    }
                    GameObject destroyed = grabed;
                    DropItem(grabed.GetComponent<Item>());
                    Destroy(destroyed);
                    addItem(thePiece.GetComponent<Item>());
                    grabed = ShowObject(thePiece);

                }
            }
            else
            {
                grabed = ShowObject(items[i].gameObject);
            }
        }
           
    }
   
}
