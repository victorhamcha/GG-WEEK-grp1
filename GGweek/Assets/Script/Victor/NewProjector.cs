using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewProjector : MonoBehaviour
{
    public Transform projection;
    public LayerMask projectable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.green);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, projectable);
        projection.position = new Vector3(hit.point.x+0.001f, hit.point.y + 0.001f, hit.point.z + 0.001f);
    }
}
