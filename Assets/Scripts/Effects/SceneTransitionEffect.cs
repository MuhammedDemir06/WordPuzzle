using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class SceneTransitionEffect : MonoBehaviour
{
    public static SceneTransitionEffect Instance;

    [SerializeField] private Animator anim;
    [SerializeField] private float nextSceneTime = 1f;
    public void Awake()
    {
        Instance = this;
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }
    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        anim.SetTrigger("End");

        yield return new WaitForSeconds(nextSceneTime);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }
    public void ExitGame()
    {
        anim.SetTrigger("End");
        StartCoroutine(ExitTime());
    }
    private IEnumerator ExitTime()
    {
        yield return new WaitForSeconds(nextSceneTime);
        Application.Quit();
    }
}
