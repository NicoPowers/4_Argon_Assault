using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadLevelDelay = 1f;
    [SerializeField] GameObject deathFX = null;
    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {

    }

    private void Respawn()
    {
        SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Triggered");
        StartDeathSequence();

    }
    private void OnCollisionEnter(Collision collision)
    {
        print("Collided");
        StartDeathSequence();
    }


    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        deathFX.SetActive(true);
        Invoke("Respawn", loadLevelDelay);


    }
}
