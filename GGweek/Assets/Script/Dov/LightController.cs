using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject obj;
    public GameObject obj2;
    private Light myLight;
    private Light myLight2;

    private void Start()
    {
        myLight = obj.GetComponent<Light>();
        myLight2 = obj2.GetComponent<Light>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myLight.enabled = !myLight.enabled;
            myLight2.enabled = !myLight2.enabled;
        }
    }
}
