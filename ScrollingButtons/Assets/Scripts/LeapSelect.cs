using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapSelect : MonoBehaviour
{
    [SerializeField] Transform indexFinger;
    [SerializeField] float rayLength = 10f;
    [SerializeField] LayerMask layer;
    RaycastHit hit;

    [Header("Selector settings")]
    [SerializeField] LayerMask m_layerMask;
    [SerializeField] float m_maxHeightToPlay = 2f;
    [SerializeField] float m_rayCastLength;
    [SerializeField] float m_increaseRate = 200f;
    Vector3 btnPos;
    bool startTimer;
    public float time;

    [Header("Touch")]
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    public float x;

    [Header("Video Components")]
    [SerializeField] GameObject m_videoPlayer;
    [SerializeField] ButtonParent m_btnParent;
    [SerializeField] VideoManager videoManager;
    public bool videoIsPlaying;

    [Header("Buttons Available")]
    [SerializeField] GameObject[] btns;
    [SerializeField] GameObject leapBtnClose;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FingerRayCast();
        ResetButtonposition();
    }

    private void FingerRayCast()
    {
        Ray ray = new Ray(indexFinger.position, indexFinger.right);

        if (Physics.Raycast(ray, out hit, rayLength, layer))
        {
            if (!videoIsPlaying)
            {
                SelectAndDragUp();
            }
            PlayButtonVideo();
            m_btnParent.StopParentRotation(true);
        }
        else if (!Physics.Raycast(ray, out hit, rayLength, layer) && !videoIsPlaying)
        {
            m_btnParent.StopParentRotation(false);
        }
    }


    public void SelectAndDragUp()
    {
        btnPos = new Vector3(hit.transform.position.x, Mathf.Clamp(hit.point.y, 0f, 4f), hit.transform.position.z);
        m_btnParent.StopParentRotation(true);
        hit.transform.position = btnPos;
    }

    private void PlayButtonVideo()
    {
        //if button reached desired height to play the video
        if (hit.transform.position.y >= m_maxHeightToPlay)
        {

            //hit.transform.position = btnNormalPos;
            //PlayVideo
            videoIsPlaying = true;
            videoManager.mediaPlayer.OpenVideoFromFile(videoManager.mediaPlayer.m_VideoLocation, m_btnParent.m_videoPaths[int.Parse(hit.transform.name)], true);
            leapBtnClose.SetActive(true);
        }

    }
    private void ResetButtonposition()
    {
        //if (Input.GetKeyUp(KeyCode.Mouse0))
        //{
        //    if (btnSelected != null)
        //    {
        //        hit.transform.position = btnNormalPos;
        //    }
        //}
        if (hit.collider == null)
        {
            //btnNormalPos = new Vector3(hit.transform.position.x, 0f, hit.transform.position.z);
            //hit.transform.position = btnNormalPos;
            btns = GameObject.FindGameObjectsWithTag("Button");
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i].transform.position = new Vector3(btns[i].transform.position.x, 0f, btns[i].transform.position.z);
            }
            x = 0;
        }
    }

    public void Finger()
    {
        print("Extended");
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(indexFinger.position, indexFinger.right * rayLength);
    }
}
