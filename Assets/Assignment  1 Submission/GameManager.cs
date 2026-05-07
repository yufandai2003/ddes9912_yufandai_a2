using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    public Text Tiop;
    public GameObject panel;
    public Text tishitext;
    public string readingContent;
    public bool startBool;

    private void Awake()
    {
        ins = this;
        if (panel != null)
            panel.SetActive(false);
    }

    void Update()
    {
        if (panel != null && panel.activeSelf)
        {
            if (Input.anyKeyDown)
                panel.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}