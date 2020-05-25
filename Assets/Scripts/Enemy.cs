using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX = null;
    // Start is called before the first frame update

    ScoreBoard scoreBoard;
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
        Destroy(gameObject);
        scoreBoard.AddPoints(500);
    }

    private void OnParticleCollision(GameObject other)
    {
        StartDeathSequence();
    }
}
