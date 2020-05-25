using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    [Tooltip("In m/s")] [SerializeField] float xSpeed = 10f;
    [Tooltip("In m/s")] [SerializeField] float ySpeed = 7f;

    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 4.35f;


    [SerializeField] float pitchPosFactor = -5f;
    [SerializeField] float pitchThrowFactor = -25f;
    [SerializeField] float yawPosFactor = 4.6f;
    [SerializeField] float yawThrowFactor = 10f;
    [SerializeField] float rollThrowFactor = -31f;

    [SerializeField] ParticleSystem LeftBullets = null;
    [SerializeField] ParticleSystem RightBullets = null;

    [SerializeField] AudioClip BulletSound = null;
    AudioSource audioSource = null;




    bool isControlsEnable = true;
    float xThrow, xOffset, yThrow, yOffset, xNew, yNew;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlsEnable)
        {
            ProcessFiring();
            ProcessTranslation();
            ProcessRotation();
        }

    }

    public void OnPlayerDeath()
    {
        isControlsEnable = false;
    }


    private void ProcessFiring()
    {
        
        if (CrossPlatformInputManager.GetButtonDown("Fire"))
        {

            audioSource.PlayOneShot(BulletSound);
            LeftBullets.Play();
            RightBullets.Play();

        }

    }
    private void ProcessRotation()
    {
        float yPos = transform.localPosition.y;
        float pitchDueToPos = yPos * pitchPosFactor;
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


    }
}
