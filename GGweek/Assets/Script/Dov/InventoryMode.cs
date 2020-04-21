using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMode : MonoBehaviour
{
    public static bool inventoryMode = false;

    public GameObject inventoryUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryMode)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        inventoryUI.SetActive(false);
        Time.timeScale = 1f;
        inventoryMode = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        inventoryUI.SetActive(true);
        Time.timeScale = 0f;
        inventoryMode = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
