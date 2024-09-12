using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource bg_adudio;
    [SerializeField] private AudioSource audioPlayer_wl;
    [SerializeField] private AudioSource audioPlayer_button;
    [SerializeField] private AudioSource audioPlayer_Spin;

    [Header("clips")]
    [SerializeField] private AudioClip SpinButtonClip;
    [SerializeField] private AudioClip SpinClip;
    [SerializeField] private AudioClip Button;
    [SerializeField] private AudioClip Win_Audio;
    [SerializeField] private AudioClip NormalBg_Audio;

    private void Awake()
    {
        playBgAudio();
        //if (bg_adudio) bg_adudio.Play();
        //audioPlayer_button.clip = clips[clips.Length - 1];
    }

    internal void PlayWLAudio(string type)
    {

        switch (type)
        {

            case "win":
                //index = UnityEngine.Random.Range(1, 2);
                audioPlayer_wl.clip = Win_Audio;
                break;

                //index = 3;

        }
        StopWLAaudio();
        //audioPlayer_wl.clip = clips[index];
        //audioPlayer_wl.loop = true;
        audioPlayer_wl.Play();

    }

    internal void PlaySpinAudio()
    {

        if (audioPlayer_Spin)
        {
            audioPlayer_Spin.clip = SpinClip;

            audioPlayer_Spin.Play();
        }

    }

    internal void StopSpinAudio()
    {

        if (audioPlayer_Spin) audioPlayer_Spin.Stop();

    }

    internal void CheckFocusFunction(bool focus, bool IsSpinning)
    {
        if (!focus)
        {
            bg_adudio.Pause();
            audioPlayer_wl.Pause();
            audioPlayer_button.Pause();
            audioPlayer_Spin.Pause();
        }
        else
        {
            if (!bg_adudio.mute) bg_adudio.UnPause();
            if (IsSpinning)
            {
                if (!audioPlayer_wl.mute) audioPlayer_wl.UnPause();
                audioPlayer_Spin.UnPause();
            }
            else
            {
                StopWLAaudio();
                audioPlayer_Spin.Stop();
            }
            if (!audioPlayer_button.mute) audioPlayer_button.UnPause();

        }
    }



    internal void playBgAudio()
    {


        //int randomIndex = UnityEngine.Random.Range(0, Bg_Audio.Length);
        if (bg_adudio)
        {
            bg_adudio.clip = NormalBg_Audio;


            bg_adudio.Play();
        }

    }

    internal void PlayButtonAudio(string type = "default")
    {

        if (type == "spin")
            audioPlayer_button.clip = SpinButtonClip;
        else
            audioPlayer_button.clip = Button;

        //StopButtonAudio();
        audioPlayer_button.Play();
        // Invoke("StopButtonAudio", audioPlayer_button.clip.length);

    }

    internal void StopWLAaudio()
    {
        audioPlayer_wl.Stop();
        audioPlayer_wl.loop = false;
    }

    internal void StopButtonAudio()
    {

        audioPlayer_button.Stop();

    }


    internal void StopBgAudio()
    {
        bg_adudio.Stop();

    }


    internal void ToggleMute(bool toggle, string type = "all")
    {

        switch (type)
        {
            case "bg":
                bg_adudio.mute = toggle;
                break;
            case "button":
                audioPlayer_button.mute = toggle;
                audioPlayer_Spin.mute = toggle;
                break;
            case "wl":
                audioPlayer_wl.mute = toggle;
                break;
            case "all":
                audioPlayer_wl.mute = toggle;
                bg_adudio.mute = toggle;
                audioPlayer_button.mute = toggle;
                break;
        }
    }

}
