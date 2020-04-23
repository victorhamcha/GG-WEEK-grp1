using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NewProjector : MonoBehaviour
{
    public Transform projection;
    public float distance;
    public Transform player;
    public Transform projector;
    public LayerMask projectable;
    private VideoPlayer screen;
    public VideoClip[] video = new VideoClip[3];
    private GameObject bobine;
    private GameObject bobineNear;
    private Grabbing grab;
    bool near=false;
    void Start()
    {
        grab = player.GetComponent<Grabbing>();
        screen = GetComponentInChildren<VideoPlayer>();

    }

    // Update is called once per frame

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.green);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, projectable);
        projection.position = new Vector3(hit.point.x+0.001f, hit.point.y + 0.001f, hit.point.z + 0.001f);
        
        if(near)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (bobine != null)
                {
                    grab.DropObject(grab.inHand);
                    GameObject nextInHand = bobine;
                    bobine = bobineNear;
                    grab.inHand = grab.Use(nextInHand);
                }
                else
                {
                    grab.DropObject(grab.inHand);                 
                    bobine = bobineNear;                  
                }
                Debug.Log("change");
                bobine.GetComponent<Rigidbody>().isKinematic = true;
                bobine.transform.position = projector.position;
                ChangeVideo(video[int.Parse(bobine.name)]);
                bobine.SetActive(false);
            }
        }
        
        if((projector.position-player.position).sqrMagnitude< distance* distance&&!grab.inventaireOn)
        {
            if(grab._using&& grab.inHand.tag == "bobine")
            {

                near = true;
                bobineNear= grab.inHand;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0)&&!grab._using)
            {
                if (!grab._using)
                {
                    grab.inHand = grab.Use(bobine);
                    bobine = null;
                    screen.clip = null;
                }
            }
           
            
        }
        else
        {
            near = false;
        }
    }

    public void ChangeVideo(VideoClip video)
    { 
        screen.clip = video;
    }
}
