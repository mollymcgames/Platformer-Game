using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{

    [SerializeField] private AudioSource finishSound;

    private bool levelComplete = false;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelComplete)
        {
            finishSound.Play();
            levelComplete = true;
            Invoke("CompleteLevel", 1.5f); //wait before loading next scene
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("You finished the level!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
