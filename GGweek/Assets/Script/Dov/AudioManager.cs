using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider volumeGeneral;
    public Slider volumeMusic;
    public Slider volumeSFX;


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
    public void Baleine(Transform pos)
    {
        AkSoundEngine.PostEvent("Play_Baleine", pos.gameObject);
    }
    public void BruitDePas(Transform pos)
    {
        AkSoundEngine.PostEvent("Play_Bruit_De_Pas", pos.gameObject);
    }
    public void CriMonstre(Transform pos)
    {
        AkSoundEngine.PostEvent("Play_Cri_Monstre", pos.gameObject);
    }
    public void Flippant(Transform pos)
    {
        AkSoundEngine.PostEvent("Play_flippant", pos.gameObject);
    }
    public void Tuyau(Transform pos)
    {
        AkSoundEngine.PostEvent("Play_Tuyau", pos.gameObject);
    }
    public void Valve(Transform pos)
    {
        AkSoundEngine.PostEvent("Play_Valve", pos.gameObject);
    }
    public void Vapeur(Transform pos)
    {
        AkSoundEngine.PostEvent("Play_Vapeur", pos.gameObject);
    }
    public void SonVideo1(Transform pos)
    {
        AkSoundEngine.PostEvent("Play_Son_Video_1", pos.gameObject);
    }
    public void SonVideo2(Transform pos)
    {
        AkSoundEngine.PostEvent("Play_Son_Video_2", pos.gameObject);
    }
    public void SonVideo3(Transform pos)
    {
        AkSoundEngine.PostEvent("Play_Son_Video_3", pos.gameObject);
    }
}
