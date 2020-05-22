using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [Tooltip("In m/s")][SerializeField] float xSpeed = 10f;
    [Tooltip("In m/s")] [SerializeField] float ySpeed = 7f;

    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;


    [SerializeField] float pitchPosFactor = -5f;
    [SerializeField] float pitchThrowFactor = -25f;
    [SerializeField] float yawPosFactor = 4.6f;
    [SerializeField] float yawThrowFactor = 10f;
    [SerializeField] float rollThrowFactor = -31f;


    [SerializeField] ParticleSystem LeftBullets;
    [SerializeField] ParticleSystem RightBullets;

    bool fired = false;

    float xThrow, xOffset, yThrow, yOffset, xNew, yNew;
    Vector3 currentPos, finalPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessFiring();
        ProcessTranslation();
        ProcessRotation();
        

    }

    private void ProcessFiring()
    {
        float fire = CrossPlatformInputManager.GetAxisRaw("Jump");
        if (fire > 0f)
        {
            fired = false;
            finalPos = transform.position;
            var LeftBulletsMain = LeftBullets.main;
            var RightBulletsMain = RightBullets.main;
            float initialSpeed = Mathf.Abs((finalPos.sqrMagnitude - currentPos.sqrMagnitude) / Time.deltaTime);
            print("speed" + initialSpeed);
            LeftBulletsMain.startSpeed = initialSpeed + 150f;
            RightBulletsMain.startSpeed = initialSpeed + 150f;
            LeftBullets.Play();
            RightBullets.Play();

        }
    }

    private void ProcessRotation()
    {
        float yPos = transform.localPosition.y;
        float pitchDueToPos =  yPos * pitchPosFactor;
        float pitchDueToThrow = yThrow * pitchThrowFactor;

        float xPos = transform.localPosition.x;
        float yawDueToPos = xPos * yawPosFactor;
        float yawDueToThrow = xThrow * yawThrowFactor;

        float rollDueToThrow = xThrow * rollThrowFactor;
        transform.localRotation = Quaternion.Euler(pitchDueToPos + pitchDueToThrow, yawDueToPos + yawDueToThrow, rollDueToThrow);
    }

    private void ProcessTranslation()
    {
        

        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        xOffset = xThrow * xSpeed * Time.deltaTime;
        yOffset = yThrow * ySpeed * Time.deltaTime;

        xNew = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);
        yNew = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange);

        transform.localPosition = new Vector3(xNew, yNew, transform.localPosition.z);
        if (!fired)
        {
            currentPos = transform.position;
        }
        
    }
}
