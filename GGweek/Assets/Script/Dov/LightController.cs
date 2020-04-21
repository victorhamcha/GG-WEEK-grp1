using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            myLight.enabled = !myLight.enabled;
            myLight3.enabled = !myLight3.enabled;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            myLight2.enabled = !myLight2.enabled;
            myLight3.enabled = !myLight3.enabled;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            myLight2.enabled = !myLight2.enabled;
            myLight4.enabled = !myLight4.enabled;
        }

    }
}

// de base 2 lampes allumés 2 et 3, valve du milieu agi sur les 2 lampes du milieu(donc 2 et 3), la valve a gauche agi sur la 1 et 3, la valve a droite agi sur la 2 et 4.