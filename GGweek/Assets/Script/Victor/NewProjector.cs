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
    public AudioSource[] audio = new AudioSource[3];
    private GameObject bobine;
    private GameObject bobineNear;
    private Grabbing grab;
    private Scene1 scene;
    bool near=false;
    void Start()
    {
        scene = FindObjectOfType<Scene1>();
        grab = player.GetComponent<Grabbing>();
        screen = GetComponentInChildren<VideoPlayer>();

    }

    // Update is called once per frame

    void Update()
    {
      
        if(bobine!=null)
        {
            if(bobine.name=="0"&&!scene.scene1)
            {
                scene.scene1 = true;
            }
            else if (bobine.name == "1" && !scene.scene2)
            {
                scene.scene2 = true;
            }
            else if (bobine.name == "2" && !scene.scene3)
            {
                scene.scene3 = true;
            }
        }
        
        if(near)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (bobine != null)
                {
                    bobine.SetActive(true);
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
