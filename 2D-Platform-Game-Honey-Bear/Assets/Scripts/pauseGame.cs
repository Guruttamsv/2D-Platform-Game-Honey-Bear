using UnityEngine;
using UnityEngine.UI;

public class pauseGame : MonoBehaviour
{
    public GameObject[] targetObjects;  // Use an array to hold multiple target objects

    private bool isPaused = false;
   

    private void Start()
    {
        Button pauseButton = GetComponent<Button>();
        // Add a listener to the button's click event
        pauseButton.onClick.AddListener(TogglePause);
    }

    private void TogglePause()
    {
        // Toggle the pause state
        isPaused = !isPaused;

        // Pause or resume the game based on the state
        if (isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f; // Set time scale to zero to pause the game
        Debug.Log("Game Paused");

        // Activate the target objects and deactivate the rest
        SetObjectsActivation(false);
    }

    private void Resume()
    {
        Time.timeScale = 1f; // Set time scale to one to resume the game
        Debug.Log("Game Resumed");

        // Deactivate all target objects
        SetObjectsActivation(true);
    }

    private void SetObjectsActivation(bool activate)
    {
        // Activate or deactivate all target objects based on the state
        foreach (GameObject obj in targetObjects)
        {
            obj.SetActive(activate);
        }
    }
}
