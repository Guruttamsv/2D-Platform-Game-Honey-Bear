using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagScript : MonoBehaviour
{
    public Transform player;
    public GameObject[] objectsToActivate;
    public GameObject[] enemyToCheck;
    public Vector3 targetPosition = new Vector3(0.0f,0.0f,0.0f);
    public float teleportDuration = 2f;
    public float interactionDistance = 1.0f; // Adjust this distance as needed

    private bool isPlayerNearFlag = false;

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < interactionDistance)
        {
            isPlayerNearFlag = true;
            Debug.Log("Player is near the flag!");

            if (isPlayerNearFlag && Input.GetKeyDown(KeyCode.Return))
                {
                if (AreAllEnemiesDestroyed())
                {
                    StartCoroutine(TeleportPlayer());
                }
                else
                {
                    Debug.Log("Cannot teleport. Enemies in the layer are still active.");
                }
                }
        }else
        {
            isPlayerNearFlag = false;
        }
    }

    private IEnumerator TeleportPlayer()
    {
        // Disable player control during teleportation
        player.GetComponent<playerController>().SetControllable(false);

        // Activate objects during transition
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }

        float elapsedTime = 0f;
        Vector3 startingPosition = player.position;

        while (elapsedTime < teleportDuration)
        {
            player.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / teleportDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the player is exactly at the target position
        player.position = targetPosition;

        // Enable player control after teleportation
        player.GetComponent<playerController>().SetControllable(true);
    }
    bool AreAllEnemiesDestroyed(){
        if(enemyToCheck.Length == 0) return true;
        for (int i = 0; i < 3; i++)
        {
        // Check if the value in enemyToCheck at index i is null
            if (enemyToCheck[i] == null)
                {
                continue;
                }
            else
                {
                return false;
            }
            
        }return true;
        
    }
}