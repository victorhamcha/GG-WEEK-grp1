using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NewProjector : MonoBehaviour
{
    public Transform projection;
    public LayerMask projectable;
    private VideoPlayer screen;
    public VideoClip[] video = new VideoClip[3];
    void Start()
    {
        screen = GetComponentInChildren<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.green);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, projectable);
        projection.position = new Vector3(hit.point.x+0.001f, hit.point.y + 0.001f, hit.point.z + 0.001f);
        if(Input.GetKeyDown(KeyCode.F))
        {
            int i = Random.Range(0, 3);
            ChangeVideo(video[i]);
        }
        
    }

    public void ChangeVideo(VideoClip video)
    {
        screen.clip = video;
    }
}
