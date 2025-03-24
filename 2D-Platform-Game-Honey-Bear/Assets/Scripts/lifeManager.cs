using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class lifeManager : MonoBehaviour
{
    public string playerTag = "Player";
    public GameObject cageOpen;
    public GameObject cageClose;
    public GameObject goal;
    public Image Queen;
    public TextMeshProUGUI cross;
    public List<Image> enemyImages = new List<Image>();

    private int i = 0;

    public int playerLife = 1; // Player's life
    public int enemyLife = 1;  // Enemy's life
    public int queenLife = 10; // Queen's life

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI TextObject;
    public TextMeshProUGUI gameOverText;

    void Start()
    {

        UpdateLifeText();
    }

    void Update()
    {
        if (i > 5)
        {
            Queen.gameObject.SetActive(true);
        }
        UpdateLifeText();
        // Check if the player has reached zero life
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (playerLife < 0 || player.transform.position.y < -2.194645)
        {
            player.transform.GetComponent<playerController>().SetControllable(false);
            // Activate the TextMeshProUGUI

            gameOverText.gameObject.SetActive(true);
            cross.gameObject.SetActive(true);
            player.gameObject.SetActive(false);

        }
    }

    // Called when the player kicks the enemy
    public void PlayerKicksEnemy(GameObject enemy)
    {
        if (enemyLife > 0)
        {
            enemyLife--;
        }
        else
        {
            Destroy(enemy);
            enemyImages[i].gameObject.SetActive(false);
            i++;
        }
    }

    // Called when the enemy touches the player
    public void EnemyTouchesPlayer()
    {
        if (playerLife >=0 )
        {
            playerLife--;
        }
    }

    // Called when the player kicks the queen
    public void PlayerKicksQueen(GameObject enemy)
    {
        if (queenLife > 0)
        {
            queenLife--;
        }
        else
        {
            Destroy(enemy);
            cageClose.gameObject.SetActive(true);
            cageOpen.gameObject.SetActive(false);

            // Start the coroutine to wait for the player and activate the text
            StartCoroutine(WaitForPlayerAndActivateText());
        }
    }

    IEnumerator WaitForPlayerAndActivateText()
    {
        // Wait for the player to reach the target object (replace "YourTargetObjectName" with the actual object name or tag)
        yield return new WaitUntil(() => PlayerReachedTarget(goal));

        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        // Activate your text object (replace "YourTextObjectName" with the actual text object name)
        TextObject.gameObject.SetActive(true);
        player.transform.GetComponent<playerController>().SetControllable(false);
    }

    bool PlayerReachedTarget(GameObject targetObjectName)
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        // Check if the player is near the target object (replace "YourTargetObjectName" with the actual object name or tag)
        float distance = Vector3.Distance(player.transform.position, targetObjectName.transform.position);
        return distance < 0.5f;
    }

    void UpdateLifeText()
    {
        coinText.text = playerLife.ToString();
    }
}
