using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int numberOfGemsToGet;
    public AudioClip victorySound;
    public AudioClip alertSound;

    public Text gemsLeftDisplay;

    private bool victoryStarted = false;
    private bool playerKilled = false;

	// Use this for initialization
	void Start ()
    {
        ChangeGemCounter(); // Initialize the gems counter.
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (numberOfGemsToGet == 0 && !victoryStarted)
            StartCoroutine("VictoryRoutine");
	}

    public void GemCollected()
    {
        numberOfGemsToGet--; // Remove a gem
        ChangeGemCounter(); // Update counter
    }

    public void ChangeGemCounter()
    {
        if (numberOfGemsToGet >= 0) // Avoiding negative values:
            gemsLeftDisplay.text = "Gems: " + numberOfGemsToGet;
    }

    // Function to run when the required number of gems has been collected.
    IEnumerator VictoryRoutine()
    {
        victoryStarted = true;
        // Play sound;
        AudioSource.PlayClipAtPoint(victorySound, transform.position);
        yield return new WaitForSeconds(2);
        // Restart level or go to next one
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return null;
    }

    public void KillPlayer()
    {
        StartCoroutine("PlayerDeath");
    }

    IEnumerator PlayerDeath()
    {
        if (!playerKilled && !victoryStarted)
        {
            playerKilled = true;
            AudioSource.PlayClipAtPoint(alertSound, transform.position);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        yield return null;
    }
}
