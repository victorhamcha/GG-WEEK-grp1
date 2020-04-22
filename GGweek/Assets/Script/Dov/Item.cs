using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite icon;
    public bool inPuzzle=false;
    public float maxOutlineWidth;
    public GameObject instance;
    [Header("HightLight")]
    public Color OutlineColor;
    private MeshRenderer rendererr;
    public Item pusslePiece;

    

    private void Start()
    {
        rendererr = GetComponent<MeshRenderer>();
    }

    public void ShouOutline()
    {
        rendererr.material.SetFloat("_Outline", maxOutlineWidth);
        rendererr.material.SetColor("_OutlineColor", OutlineColor);
    }

    public void HideOutline()
    {
        rendererr.material.SetFloat("_Outline", 0f);
    }
}
