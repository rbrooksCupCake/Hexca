using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour
{
    public bool isMuted;

    public Image SoundIcon;
    public Sprite MutedAudioSprite;
    public Sprite UnMutedAudioSprite;

    public AudioSource stageSFX;
    public AudioSource pieceSFX;
    public AudioSource bgMusic;
    public AudioSource dropSFX;
    public AudioSource loadSFX;

    public AudioClip loadInEffect;
    public AudioClip loadOutEffect;
    public AudioClip popEffect;
    public AudioClip explosionEffect;
    public AudioClip swapInEffect;
    public AudioClip noSwapEffect;
    public AudioClip storeEffect;
    public AudioClip greatExplosionEffect;

    public void playSwapInEffect()
    {
        pieceSFX.clip = swapInEffect;
        pieceSFX.Play();
    }

    public void playStoreEffect()
    {
        pieceSFX.clip = storeEffect;
        pieceSFX.Play();
    }

    public void playNoSwapEffect()
    {
        pieceSFX.clip = noSwapEffect;
        pieceSFX.Play();
    }

    public void playPopEffect()
    {
        pieceSFX.clip = popEffect;
        pieceSFX.Play();
    }

    public void playExplosionEffect()
    {
        pieceSFX.clip = explosionEffect;
        pieceSFX.Play();
    }

    public void playLoadInEffect()
    {
        stageSFX.clip = loadInEffect;
        stageSFX.Play();
    }

    public void playLoadOutEffect()
    {
        stageSFX.clip = loadOutEffect;
        stageSFX.Play();
    }

    public void playExplosion_AlternativeEffect()
    {
        pieceSFX.clip = greatExplosionEffect;
        pieceSFX.Play();
    }

    private void MuteAudio()
    {
        SoundIcon.sprite = MutedAudioSprite;
        stageSFX.mute=true;
        pieceSFX.mute=true;
        bgMusic.mute=true;
        dropSFX.mute = true;
        loadSFX.mute = true;
    }
    private void UnMuteAudio()
    {
        SoundIcon.sprite = UnMutedAudioSprite;
        stageSFX.mute = false;
        pieceSFX.mute = false;
        bgMusic.mute = false;
    }

    public void ToggleAudio()
    {
        isMuted = !isMuted;

        if(!isMuted)
        {
            UnMuteAudio();
        }
        else
        {
            MuteAudio();
        }
    }

}
