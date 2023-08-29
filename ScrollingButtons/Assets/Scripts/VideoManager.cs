using RenderHeads.Media.AVProVideo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoManager : MonoBehaviour
{
    [SerializeField] string screenSaverVideoName;
    [SerializeField] GameObject closeBtn;
    [SerializeField] ButtonParent m_btnParent;
    [SerializeField] ButtonSelector m_btnSelector;
    //[SerializeField] LeapSelect leapSelect;
    //[SerializeField] GameObject leapBtn;
    public MediaPlayer mediaPlayer;
    public bool playScreenSaver;
    
    void Start()
    {
        PlayScreenSaverVideo();
        closeBtn.SetActive(false);
    }

    private void Update()
    {
        StopParentRotation();
    }

    private void StopParentRotation()
    {
        if (closeBtn.activeSelf)
        {
            m_btnParent.StopParentRotation(true);
        }
    }

    public void PlayScreenSaver(bool p_value)
    {
        if (p_value)
        {
            PlayScreenSaverVideo();
        }
    }

    public void PlayScreenSaverVideo()
    {
        mediaPlayer.OpenVideoFromFile(mediaPlayer.m_VideoLocation, Application.streamingAssetsPath + "/ScreenSaver/" + screenSaverVideoName + ".mp4", true);
    }

    public void ShowCloseButton(bool p_value)
    {
        closeBtn.SetActive(p_value);
    }

    public void StopVideo()
    {
        m_btnSelector.videoIsPlaying = false;
        //leapSelect.videoIsPlaying = false;
        m_btnParent.StopParentRotation(false);
        mediaPlayer.Stop();
        PlayScreenSaver(true);
    }
}
