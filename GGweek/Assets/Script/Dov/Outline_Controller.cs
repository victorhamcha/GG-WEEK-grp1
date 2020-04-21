using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline_Controller : MonoBehaviour
{
    private MeshRenderer rendererr;

    public float maxOutlineWidth;

    public Color OutlineColor;

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
