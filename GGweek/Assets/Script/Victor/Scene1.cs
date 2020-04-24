using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene1 : MonoBehaviour
{
    public Transform vane;
    public Transform guidon;
    public Transform tubeHaut;
    public Transform tubeBas;
    public Transform tubeMilieu;
    public Transform barometre;
    public Transform player;
    public Transform projecteur;
    public Grabbing grab;
    public GameObject smoke;
    public float distance;
    public bool scene1=false;
    bool valveOn = false;
    int tubeOn=0;
    public Text helpTxt;
    public Text actonTxt;
    public bool scene2 = false;
    public bool scene3 = false;
    bool guidonOn = false;
    public GameObject bobine2;
    public Transform meuble;
    public Transform light;
    public Transform porte1;
    public Material materialOn;
    public GameObject boutDeClé;
    public GameObject clé1;
    bool doorOpen = false;
    public GameObject bobine3;
    public GameObject engrenage3;
    bool enigme = false;
    public bool construct = false;
    bool lastThing = false;
    public GameObject barometrepart;
    private LightController lightAndAnimation;
   
    void Start()
    {
        helpTxt.text = "Qu'est-ce qu'il se passe ou suis-je?";
        lightAndAnimation = FindObjectOfType<LightController>();
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if(!scene1&&!scene2&&!scene3)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                helpTxt.text = "Qu'est-ce que je fais ici il faut que je me rappelle!";
            }
        }
        if(scene1)
        {
            if ((vane.position - player.position).sqrMagnitude < distance * distance && !grab.inventaireOn)
            {



                if (Input.GetKeyDown(KeyCode.F))
                {


                    if (grab._using)
                    {
                        if (grab.inHand.name == "valveItem" && tubeOn >= 2)
                        {
                            GameObject destroyed = grab.inHand;
                            grab.DropObject(grab.inHand);
                            Destroy(destroyed);
                            vane.gameObject.SetActive(true);
                            valveOn = true;
                        }
                        else if (grab.inHand.name == "tubeHautItem")
                        {
                            GameObject destroyed = grab.inHand;
                            grab.DropObject(grab.inHand);
                            Destroy(destroyed);
                            tubeHaut.gameObject.SetActive(true);
                            tubeOn++;
                        }
                        else if (grab.inHand.name == "tubeBasItem")
                        {
                            GameObject destroyed = grab.inHand;
                            grab.DropObject(grab.inHand);
                            Destroy(destroyed);
                            tubeBas.gameObject.SetActive(true);
                            tubeOn++;
                        }
                        else if (grab.inHand.name == "tubeMilieuItem")
                        {
                            GameObject destroyed = grab.inHand;
                            grab.DropObject(grab.inHand);
                            Destroy(destroyed);
                            tubeMilieu.gameObject.SetActive(true);
                            tubeOn++;
                        }
                         if (tubeOn < 2)
                        {
                            helpTxt.text = "Il faut réparer les tuyaux";
                        }
                        else if (!valveOn)
                        {
                            helpTxt.text = "Il faut que je trouve la valve";
                        }
                        else if (valveOn && tubeOn <= 2)
                        {
                            helpTxt.text = "Il ne me manque plus qu'un tuyaux";
                        }
                        else if (tubeOn > 2)
                        {
                            helpTxt.text = "J'ai l'impression que le gouvernail c'est allumé";
                        }


                    }
                    else
                    {
                        if (tubeOn < 2)
                        {
                            helpTxt.text = "Il faut réparer les tuyaux";
                        }
                        else if (!valveOn)
                        {
                            helpTxt.text = "Il faut que je trouve la valve";
                        }
                        else if(valveOn&&tubeOn<=2)
                        {
                            helpTxt.text = "Il ne me manque plus qu'un tuyaux";
                        }
                        else if (tubeOn>2)
                        {
                            helpTxt.text = "J'ai l'impression que le gouvernail c'est allumé";
                        }
                        if (valveOn)
                        {
                            AudioManager.PlayAudio(vane, "Play_Valve");
                            smoke.SetActive(false);

                        }
                    }
                    if (tubeOn >= 3 && valveOn)
                    {
                        //paneau brille
                        guidon.GetComponent<MeshRenderer>().material = materialOn;
                        guidonOn = true;
                        valveOn = false;
                    }

                    


                }
               

            }
            if (guidonOn)
            {

                if ((guidon.position - player.position).sqrMagnitude < distance * distance*6 && !grab.inventaireOn)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        guidonOn = false;
                        Drop(bobine2, boutDeClé, meuble);
                        lightAndAnimation.animShake.SetBool("canShake2", true);
                        helpTxt.text = "Quelque chose est tombée du meuble";
                        Debug.Log("yeapa");

                      
                    }
                    
                }
            }

            
        }
        if (scene2)
        {
            if (scene1)
            {
                scene1 = false;
            }
            if ((porte1.position - player.position).sqrMagnitude < distance * distance && !grab.inventaireOn)
            {
                if (Input.GetKeyDown(KeyCode.F) && grab.inHand.name == "clé1(Clone)" && !doorOpen)
                {
                    //anim Porte
                    GameObject destroyed = grab.inHand;
                    grab.DropObject(grab.inHand);
                    Destroy(destroyed);
                    porte1.gameObject.SetActive(false);
                    doorOpen = true;
                }


            }
            else if ((guidon.position - player.position).sqrMagnitude < distance * distance * 4 && !grab.inventaireOn)
            {
                if (Input.GetKeyDown(KeyCode.F) && enigme)
                {

                    Drop2(bobine3, engrenage3, meuble);
                    helpTxt.text = "une nouvelle bobine est tombée du meuble";
                    Debug.Log("yeapa2");

                   lightAndAnimation.animShake.SetBool("canShake", true);
                }

            }

            if (doorOpen && (light.position - player.position).sqrMagnitude < distance * distance*4 && !grab.inventaireOn)
            {
                if (lightAndAnimation.finish)
                {
                    helpTxt.text = "Essayons de partir maintenant!";
                    enigme = true;
                }
            }


        }

        if (scene3)
        {
            if (scene2)
            {
                lightAndAnimation.animShake.SetBool("canShake", true);
                scene2 = false;
            }
            if ((projecteur.position - player.position).sqrMagnitude < distance * distance * 2 && !grab.inventaireOn && !construct)
            {

                helpTxt.text = "Il ne me reste plus qu'à assembler les pièce du baromêtre";


            }
            if (construct && (barometre.position - player.position).sqrMagnitude < distance * distance * 3 && !grab.inventaireOn)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    lastThing = true;
                    GameObject destroyed = grab.inHand;
                    grab.DropObject(grab.inHand);
                    Destroy(destroyed);
                    barometrepart.SetActive(true);
                    helpTxt.text = "Je peux enfin partir !";
                }
            }
            if(grab.grabed!=null)
            {
                if (grab.grabed.name == "barometrefinie(Clone)" && !construct)
                {
                    construct = true;
                }
            }
           
            if (lastThing && (guidon.position - player.position).sqrMagnitude < distance * distance * 6 && !grab.inventaireOn)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    
                    SceneManager.LoadScene("Fin");

                }
            }
        }
        if (((((vane.position - player.position).sqrMagnitude < distance * distance)||((projecteur.position - player.position).sqrMagnitude < distance * distance*2) || ((barometre.position - player.position).sqrMagnitude < distance * distance*3) || ((guidon.position - player.position).sqrMagnitude < distance * distance )|| (porte1.position - player.position).sqrMagnitude < distance * distance&&!doorOpen) || ((light.position - player.position).sqrMagnitude < distance * distance)||((projecteur.position - player.position).sqrMagnitude < distance * distance) || ((barometre.position - player.position).sqrMagnitude < distance * distance)) && !grab.inventaireOn)
        {
            actonTxt.text = "Utiliser F";
        }
        else
        {
            helpTxt.text = "";
            actonTxt.text = "";
        }
    }


    public void Drop(GameObject instance, GameObject instance2,Transform position)
    {
       GameObject bobine= Instantiate(instance, position.position,Quaternion.identity);

        bobine.name = "1";
        Instantiate(instance2, position.position,Quaternion.identity);
    }
    public void Drop2(GameObject instance, GameObject instance2, Transform position)
    {
        GameObject bobine = Instantiate(instance, position.position, Quaternion.identity);

        bobine.name = "2";
        Instantiate(instance2, position.position, Quaternion.identity);
    }
}
