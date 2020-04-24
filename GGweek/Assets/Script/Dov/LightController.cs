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
    private float timer1 = 4.0f;
    private float timer2 = 3.0f;
    private bool firstStep = false;
    private bool unstep = false;

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
            if(!finish)
            {
                if (hit.collider.gameObject.name == "ValveLampe1")
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        Debug.Log("1");
                        myLight.enabled = !myLight.enabled;
                        myLight3.enabled = !myLight3.enabled;
                        AudioManager.PlayAudio(cam.transform, "Play_Ampoule");
                    }
                }
                if (hit.collider.gameObject.name == "ValveLampe2")
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        Debug.Log("2");
                        myLight2.enabled = !myLight2.enabled;
                        myLight3.enabled = !myLight3.enabled;
                        AudioManager.PlayAudio(cam.transform, "Play_Ampoule");
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
                        AudioManager.PlayAudio(cam.transform, "Play_Ampoule");
                    }
                }
            }
            

         


            if (myLight.enabled && myLight2.enabled && myLight3.enabled && myLight4.enabled)
            {
                if(!finish)
                Finish();
            }

            if (finish)
            {
                if (timer1 > 0)
                {
                    timer1 -= Time.deltaTime;
                }
                else if (!firstStep)
                {
                    AudioManager.PlayAudio(cam.transform, "Play_Cri_Monstre");
                    firstStep = true;
                    animShake.SetBool("canShake", true);
                }

                if (timer2 > 0 && firstStep)
                {
                    timer2 -= Time.deltaTime;
                }
                else if (!unstep && firstStep)
                {
                    AudioManager.PlayAudio(cam.transform, "Play_flippant");
                    unstep = true;
                }
                
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
