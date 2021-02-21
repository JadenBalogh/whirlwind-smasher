using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private float startDelay = 1f;

    public void Play()
    {
        MainMenu.Instance.Hide();
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(startDelay);
        SceneManager.LoadScene("Main");
    }
}
