using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelector : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject videoPlayer;
    VideoManager videoManager;

    void Start()
    {
        videoManager = GetComponent<VideoManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 1000f, layerMask))
        {
            videoManager.mediaPlayer.m_VideoPath = Application.streamingAssetsPath + "/" + "Video1.mp4";
            videoPlayer.SetActive(true);
        }

        Debug.DrawLine(ray.origin, ray.origin * 1000f, Color.red);
    }
}
