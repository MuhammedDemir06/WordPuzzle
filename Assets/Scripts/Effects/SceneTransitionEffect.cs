using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionEffect : MonoBehaviour
{
    public static SceneTransitionEffect Instance;

    [SerializeField] private Animator anim;
    [SerializeField] private float nextSceneTime = 1f;
    public void Awake()
    {
        Instance = this;
    }
    public void LoadScene(string nextScene)
    {
        anim.SetTrigger("End");
        StartCoroutine(NextSceneTime(nextScene));
    }
    private IEnumerator NextSceneTime(string next)
    {
        yield return new WaitForSeconds(nextSceneTime);
        SceneManager.LoadScene(next);
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
