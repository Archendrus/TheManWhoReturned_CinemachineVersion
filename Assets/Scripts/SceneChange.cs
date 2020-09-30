using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string newLevel = "";
    [SerializeField] private Animator transition = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(LoadScene());
        }
    }

    public IEnumerator LoadScene()
    {
        Debug.Log("LoadScene");
        Debug.Log(newLevel);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(newLevel);
    }
}
