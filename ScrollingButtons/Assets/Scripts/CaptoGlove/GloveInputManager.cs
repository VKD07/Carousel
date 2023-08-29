using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveInputManager : MonoBehaviour
{
    [SerializeField] ButtonParent btnParent;
    [SerializeField] ButtonRayDetector btnRayDetector;
    [SerializeField] ButtonSelector btnSelector;
    [SerializeField] VideoManager videoManager;
    [SerializeField] GameObject closeButton;
    [SerializeField] float additionalSpeed = 5f;
    [Header("Input Buttons")]
    [SerializeField] KeyCode leftAndrightAccel = KeyCode.L;
    [SerializeField] KeyCode indexFingBend = KeyCode.T;
    [SerializeField] KeyCode upAnddownAccel = KeyCode.U;
    [SerializeField] KeyCode midFingBend = KeyCode.M;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ButtonSwipe();
        StopButtonRotation();
        PlayVideo();
        // StopVideo();
    }

    private void ButtonSwipe()
    {
        if (Input.GetKeyDown(leftAndrightAccel))
        {
            print("Swiped");
            btnParent.rotationSpeed += additionalSpeed;
            //btnParent.rotationSpeed = -btnParent.rotationSpeed;
            StopVideo();
        }
    }

    private void StopButtonRotation()
    {
            if (Input.GetKeyDown(indexFingBend))
            {
                btnParent.stopRotation = true;
            btnParent.rotationSpeed = 60f;

            }
            else if (Input.GetKeyUp(indexFingBend))
            {
                btnParent.stopRotation = false;
            }
    }
    private void PlayVideo()
    {
        if (btnRayDetector.ButtonDetected)
        {
            StartCoroutine(PlayVideoWithDelay());
        }
    }

    IEnumerator PlayVideoWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        if (Input.GetKeyDown(upAnddownAccel))
        {
            btnRayDetector.Button.transform.position = new Vector3(btnRayDetector.Button.transform.position.x, 2, btnRayDetector.Button.transform.position.z);
            btnSelector.PlayButtonVideo(btnRayDetector.Button);
        }
    }

    private void StopVideo()
    {
        videoManager.StopVideo();
        closeButton.SetActive(false);
    }
}
