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
    public AudioClip[] clip = new AudioClip[3];
    public AudioSource audio;
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
      
      
            if(screen.clip==video[0]&&!scene.scene1)
            {
                scene.scene1 = true;
            }
            else if (screen.clip == video[1] && !scene.scene2)
            {
                scene.scene2 = true;
            }
            else if (screen.clip == video[2] && !scene.scene3)
            {
                scene.scene3 = true;
            }
        
        
        if(near)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {            
                bobine = bobineNear;                  
                Debug.Log("change");
                ChangeVideo(video[int.Parse(bobine.name)]);
                //audio.clip = clip[int.Parse(bobine.name)];
                if(bobine.name=="0")
                {
                    //premier sond
                    AudioManager.PlayAudio(projector, "Play_Son_Video_1");
                }
                else if (bobine.name == "1")
                {
                    //deuxième sond
                    AudioManager.PlayAudio(projector, "Play_Son_Video_2");
                }
                else if (bobine.name == "2")
                {
                    //troisième sond
                    AudioManager.PlayAudio(projector, "Play_Son_Video_1");
                }
                GameObject destroyed = grab.inHand;
                grab.DropObject(grab.inHand);
                Destroy(destroyed);
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
