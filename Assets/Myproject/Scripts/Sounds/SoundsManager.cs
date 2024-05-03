using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSZombie.Generic;

public class SoundsManager : GenericSingleTon<SoundsManager>
{
   
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private float Volume;
    [SerializeField] private float effactVolume;
    [SerializeField] private bool IsMute = false;
    public SoundType[] allSounds;

    private void Start()
    {
        SetVolume(Volume, effactVolume);
        PlayMusic(Sounds.Music);
        
    }
    public void SetVolume(float mVolume, float effectVol)
    {
        music.volume = mVolume;
        sfx.volume = effectVol;
    }
    public void Mute(bool Status)
    {
        IsMute = Status;
       
    }
    public void PlayMusic(Sounds sounds)
    {
        if(IsMute)
        {
            return;
        }
        AudioClip clip = GetSoundClip(sounds);
        if(clip != null)
        {
            
            music.clip = clip;
            music.Play();
        }
        else
        {
            Debug.LogError("Clip not found for sound type: " + sounds);
        }
    }
    public void Play(Sounds sounds)
    {
        if(IsMute)
        {
            return;
        }
        AudioClip clip = GetSoundClip(sounds);
        if(clip != null)
        {
            sfx.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Clip not found for sound type: " + sounds);
        }
    }
    public void StopSound()
    {
        if(IsMute)
        {
            return;
        }
    }
    private AudioClip GetSoundClip(Sounds sounds)
    {
        Debug.Log("Clip Is Found");
        SoundType item = Array.Find(allSounds, i => i.soundType == sounds);
        if(item != null)
        {
            return item.soundClip;
        }
        return null;
    }
}

[Serializable]
public class SoundType 
{
    public Sounds soundType;
    public AudioClip soundClip;
}
public enum Sounds
{
    Music,
    GunSfx,
    FootStep,
    ZombieAttack,
    Zombie,
    PickUps

}
