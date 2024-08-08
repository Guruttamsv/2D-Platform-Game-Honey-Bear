using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public GameObject door;
    public GameObject[] checkEnemy;
    // Start is called before the first frame update
    void Start()
    { 
    
    }

    // Update is called once per frame
    void Update()
    {
        if(AreAllEnemiesDestroyed()){
            door.gameObject.SetActive(false);
        }
    }
        bool AreAllEnemiesDestroyed(){
        if(checkEnemy.Length == 0) return true;
        for (int i = 0; i < 3; i++)
        {
        // Check if the value in enemyToCheck at index i is null
            if (checkEnemy[i] == null)
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
