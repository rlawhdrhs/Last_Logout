using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Puzzle4Move : MonoBehaviour
{
    public GameObject window;
    private void Start()
    {
        if(GameManager.instance != null)
        {
            if (GameManager.instance.PuzzleFail[1] == true)
            {
                StartCoroutine(Move());
                GameManager.instance.PuzzleFail[1] = false;
            }
        }
    }
    IEnumerator Move()
    {
        window.SetActive(true);
        yield return new WaitForSeconds(3f);
        window.SetActive(false);

        SceneManager.LoadScene("Puzzle4 Stage1");
    }
}
