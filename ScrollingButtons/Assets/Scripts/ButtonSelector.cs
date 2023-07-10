using RenderHeads.Media.AVProVideo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelector : MonoBehaviour
{
    [Header("Selector settings")]
    [SerializeField] LayerMask m_layerMask;
    [SerializeField] float m_maxHeightToPlay = 2f;
    [SerializeField] float m_rayCastLength;
    [SerializeField] float m_increaseRate = 200f;
    RaycastHit hit;
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
    VideoManager videoManager;
    public bool videoIsPlaying;

    [Header("Buttons Available")]
    [SerializeField] GameObject[] btns;
    void Start()
    {
        videoManager = GetComponent<VideoManager>();
    }
    void Update()
    {
        SelectButton();
        Swipe();
        IncreaseRotationSpeed();
    }

    private void SelectButton()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, m_rayCastLength, m_layerMask))
        {
            //drag and drop
            SelectAndDragUp();
            PlayButtonVideo();
        }
        ResetButtonposition();
    }


    private void SelectAndDragUp()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            btnPos = new Vector3(hit.transform.position.x, Mathf.Clamp(hit.point.y, 0f, 4f), hit.transform.position.z);
            m_btnParent.StopParentRotation(true);
            hit.transform.position = btnPos;

            //reset 
            ResetRotationSpeed();
        }
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
            videoManager.ShowCloseButton(true);
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
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //btnNormalPos = new Vector3(hit.transform.position.x, 0f, hit.transform.position.z);
            //hit.transform.position = btnNormalPos;
            btns = GameObject.FindGameObjectsWithTag("Button");
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i].transform.position = new Vector3(btns[i].transform.position.x, 0f, btns[i].transform.position.z);
            }
            x = 0;
            m_btnParent.StopParentRotation(false);
        }
    }
    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTimer = true;
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            startTimer = false;

            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            currentSwipe.Normalize();

            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("up swipe");
                //m_btnParent.StopParentRotation(true);
            }
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("down swipe");
                m_btnParent.StopParentRotation(false);
            }
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("left swipe");
                m_btnParent.rotationSpeed = Mathf.Abs(m_btnParent.rotationSpeed);

                if (time <= 0.2 && time > 0.1 && !videoIsPlaying) //If the user let it go immediately the rotation speed will increase
                {
                    m_btnParent.rotationSpeed += m_increaseRate;
                }
            }
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                m_btnParent.rotationSpeed = -Mathf.Abs(m_btnParent.rotationSpeed);
                Debug.Log("right swipe");

                if (time <= 0.2 && time > 0.1 && !videoIsPlaying) //If the user let it go immediately the rotation speed will increase
                {
                    m_btnParent.rotationSpeed -= m_increaseRate;
                }
            }
        }
    }

    public void RotateLeft()
    {
        m_btnParent.rotationSpeed = Mathf.Abs(m_btnParent.rotationSpeed);
    }

    public void RotateRight()
    {
        m_btnParent.rotationSpeed = -Mathf.Abs(m_btnParent.rotationSpeed);
    }

    private void IncreaseRotationSpeed()
    {
        if (startTimer) //left mouse button is pressed it will start a timer
        {
            time += Time.deltaTime;
        }
        else // if left mouse button is let go it will stop the timer
        {
            time = 0;
        }      
    }

    private void ResetRotationSpeed()
    {
        if (m_btnParent.rotationSpeed < m_btnParent.initialRotation)
        {
            m_btnParent.rotationSpeed = -m_btnParent.initialRotation;
        }
        else
        {
            m_btnParent.rotationSpeed = m_btnParent.initialRotation;
        }
    }
}
