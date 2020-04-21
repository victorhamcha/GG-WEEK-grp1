using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        Image displayImage = transform.Find("Image").GetComponent<Image>();

        if (item)
        {
            displayImage.sprite = item.icon;
        }
        else
        {
            displayImage.sprite = null;
        }
    }
}
