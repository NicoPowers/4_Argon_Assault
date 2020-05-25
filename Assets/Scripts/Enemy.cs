using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX = null;
    // Start is called before the first frame update

    ScoreBoard scoreBoard;
    int timesHit = 0;
    void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void StartDeathSequence()
    {
        Instantiate(deathFX, transform.position, Quaternion.identity);
        if (timesHit == 1)
        {
            scoreBoard.AddPoints(500);
        }
        
        Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        StartDeathSequence();
        timesHit++;
    }
}
