using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Camera cam;
    public float dist;
    public Animator animShake;
    public GameObject obj;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    private Light myLight;
    private Light myLight2;
    private Light myLight3;
    private Light myLight4;
    public bool finish;
    public LayerMask lighter;

    private void Start()
    {
        myLight = obj.GetComponent<Light>();
        myLight2 = obj2.GetComponent<Light>();
        myLight3 = obj3.GetComponent<Light>();
        myLight4 = obj4.GetComponent<Light>();
    }

    private void Update()
    {
        if (animShake.GetBool("canShake") == true)
        {
            StartCoroutine(Shaking());
        }
        if (animShake.GetBool("canShake2") == true)
        {
            StartCoroutine(Shaking2());
        }

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if  (Physics.Raycast(ray, out RaycastHit hit, dist, lighter))
        {
            if (hit.collider.gameObject.name == "ValveLampe1")
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("1");
                    myLight.enabled = !myLight.enabled;
                    myLight3.enabled = !myLight3.enabled;
                    
                }
            }
            if (hit.collider.gameObject.name == "ValveLampe2")
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("2");
                    myLight2.enabled = !myLight2.enabled;
                    myLight3.enabled = !myLight3.enabled;
                 
                }

                if (animShake.GetBool("canShake2") == true)
                {
                    StartCoroutine(Shaking2());
                }
            }
            if (hit.collider.gameObject.name == "ValveLampe3")
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("1");
                    myLight2.enabled = !myLight2.enabled;
                    myLight4.enabled = !myLight4.enabled;
                    animShake.SetBool("canShake", true);
                }
            }

         


            if (myLight.enabled && myLight2.enabled && myLight3.enabled && myLight4.enabled)
            {
                if(!finish)
                Finish();
            }
            
        }

        IEnumerator Shaking()
        {
            for (int i = 0; i < 10; i++)
            {
                animShake.Play("ShakeFreeFall");
                yield return new WaitForSeconds(0.04f);
            }
            animShake.SetBool("canShake", false);
        }

        IEnumerator Shaking2()
        {
            for (int i = 0; i < 10; i++)
            {
                animShake.Play("LittleShake");
                yield return new WaitForSeconds(0.04f);
            }
            animShake.SetBool("canShake2", false);
        }

        void Finish()
        {
            Debug.Log("Enigme 3 reussit !!");
            finish = true;
        }
        
    }
}
