using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Invoke("LoadFirstLevel", 3f);
    }

    private void LoadFirstLevel ()
    {
        
        
        SceneManager.LoadScene(1);
        
        
    }
}
