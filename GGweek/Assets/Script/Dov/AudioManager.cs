using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider volumeGeneral;
    public Slider volumeMusic;
    public Slider volumeSFX;
    public float timer = 180f;
    public Transform player;


    public void setVolumeGen()
    {
        AkSoundEngine.SetRTPCValue("RTPC_Main_Volume", volumeGeneral.value);
    }

    public void setVolumeMusic()
    {
        AkSoundEngine.SetRTPCValue("RTPC_Music_Volume", volumeMusic.value);
    }

    public void setVolumeSFX()
    {
        AkSoundEngine.SetRTPCValue("RTPC_SFX_Volume", volumeSFX.value);
    }

    public static void PlayAudio(Transform pos, string son)
    {
        AkSoundEngine.PostEvent(son, pos.gameObject);
    }

    public static void StopAudio(Transform pos, string son)
    {
        AkSoundEngine.PostEvent(son, pos.gameObject);
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            AudioManager.PlayAudio(player, "Play_Baleine");
            timer = 180f;
        }
    }

}
