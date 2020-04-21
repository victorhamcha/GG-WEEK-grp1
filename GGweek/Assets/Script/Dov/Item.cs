using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite icon;
    public GameObject instance;
    private Grabbing inventaire;

    private void Start()
    {
        inventaire = FindObjectOfType<Grabbing>();
    }

}
