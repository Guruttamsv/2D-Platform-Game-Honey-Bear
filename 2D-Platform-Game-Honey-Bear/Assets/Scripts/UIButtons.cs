using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    public GameObject targetObject;
    private Button button;
    private bool canClick = true; // Flag to control button click

    private void Start()
    {
        // Get the Button component attached to this GameObject
        button = GetComponent<Button>();

        // Add a listener to the button's click event
        button.onClick.AddListener(OnClick);
    }

    private void Update()
    {
    }

    private void OnClick()
    {
        // Toggle the activation state
        bool isActivated = !targetObject.activeSelf;

        // Activate or deactivate the target object based on the state
        if (targetObject != null)
        {
            targetObject.SetActive(isActivated);
        }

        // Disable further button clicks for a short duration (adjust as needed)
        StartCoroutine(DisableButtonClick());
    }

    private void OnEnterOrSpaceKeyPress()
    {
        // Custom logic for Enter or Space key press
        Debug.Log("Enter or Space key pressed!");
    }

    private IEnumerator DisableButtonClick()
    {
        // Disable further button clicks for a short duration
        canClick = false;
        yield return new WaitForSeconds(0.5f); // Adjust the duration as needed
        canClick = true;
    }
}
