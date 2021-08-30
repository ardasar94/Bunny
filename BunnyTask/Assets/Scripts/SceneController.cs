using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject WinScreen;
    [SerializeField] GameObject LoseScreen;

    int currentSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex); // it should be "currentSceneIndex + 1" when there is more than one scene
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ShowWinScreen()
    {
        Invoke("ActivateWinScreen", 1);
    }

    public void ShowLoseScreen()
    {
        Invoke("ActivateLoseScreen", 1);
    }

    void ActivateWinScreen()
    {
        WinScreen.SetActive(true);
    }

    void ActivateLoseScreen()
    {
        LoseScreen.SetActive(true);
    }
}
