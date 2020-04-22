using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Camera cam;
    public float dist;
    public GameObject obj;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    private Light myLight;
    private Light myLight2;
    private Light myLight3;
    private Light myLight4;


    private void Start()
    {
        myLight = obj.GetComponent<Light>();
        myLight2 = obj2.GetComponent<Light>();
        myLight3 = obj3.GetComponent<Light>();
        myLight4 = obj4.GetComponent<Light>();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, dist))
        {
            if (hit.collider.CompareTag("Interact1"))
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    myLight.enabled = !myLight.enabled;
                    myLight3.enabled = !myLight3.enabled;
                }
            }
            if (hit.collider.CompareTag("Interact2"))
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    myLight2.enabled = !myLight2.enabled;
                    myLight3.enabled = !myLight3.enabled;
                }
            }
            if (hit.collider.CompareTag("Interact3"))
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    myLight2.enabled = !myLight2.enabled;
                    myLight4.enabled = !myLight4.enabled;
                }
            }

            if (myLight.enabled && myLight2.enabled && myLight3.enabled && myLight4.enabled)
            {
                Finish();
            }
            
        }

        void Finish()
        {
            Debug.Log("Enigme 3 reussit !!");
        }
        
    }
}
