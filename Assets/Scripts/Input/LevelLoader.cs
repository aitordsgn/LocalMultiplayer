using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator Transitions;

    public float transitionTime = 1f;

    [SerializeField] private GameObject Container;


    public void LoadNextLevel(int BuildIndex)
    {
        Container.SetActive(true);
        StartCoroutine(LoadLevel(BuildIndex));
    }

    IEnumerator LoadLevel(int LevelIndex)
    {
        //Play Animation
        Transitions.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(LevelIndex);
    }
}
