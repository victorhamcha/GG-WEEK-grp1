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
            if (hit.collider.gameObject.name == "Sphere1")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    myLight.enabled = !myLight.enabled;
                    myLight3.enabled = !myLight3.enabled;
                }
            }
            if (hit.collider.gameObject.name == "Sphere2")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    myLight2.enabled = !myLight2.enabled;
                    myLight3.enabled = !myLight3.enabled;
                    animShake.SetBool("canShake2", true);
                }

                if (animShake.GetBool("canShake2") == true)
                {
                    StartCoroutine(Shaking2());
                }
            }
            if (hit.collider.gameObject.name == "Sphere3")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    myLight2.enabled = !myLight2.enabled;
                    myLight4.enabled = !myLight4.enabled;
                    animShake.SetBool("canShake", true);
                }
            }

            if (animShake.GetBool("canShake") == true)
            {
                StartCoroutine(Shaking());
            }


            if (myLight.enabled && myLight2.enabled && myLight3.enabled && myLight4.enabled)
            {
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
        }
        
    }
}
