using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] bool isSplashScreen = false;
    private void Awake()
    {
        // if more than 1 music player, then we want to destroy ourselves
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    void Start()
    {
        if (isSplashScreen)
        {
            Invoke("LoadFirstLevel", 3f);
        }

    }

    private void LoadFirstLevel()
    {


        SceneManager.LoadScene(1);


    }
}
