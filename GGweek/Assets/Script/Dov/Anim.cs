using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public Animator animShake;


    void Update()
    {
        if (animShake.GetBool("canShake") == true)
        {
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        for (int i = 0; i < 10; i++)
        {
            animShake.Play("ShakeFreeFall");
            yield return new WaitForSeconds(0.16f);
        }
        animShake.SetBool("canShake", false);
    }
}
