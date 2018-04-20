using UnityEngine;
using System.Collections;

/// <summary>
/// Basically debug stuff right now, but might store whether the player is a ghost or not and health later.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    EnemyManager[] enemies;

    // Use this for initialization
    void Start()
    {
        // get all of the enemies in the level
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Ghost");
        enemies = new EnemyManager[enemyObjects.Length];
        for(int index = 0; index < enemyObjects.Length; index++)
            enemies[index] = enemyObjects[index].GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        changeEnemyState();

        //saveLoadInput();
    }

    /// <summary>
    /// Change the enemies from calm to angry or the other way around with the press of a button!
    /// Wow!
    /// </summary>
    private void changeEnemyState()
    {
        // flop all of the states of the enemies
        if(Input.GetKeyDown(KeyCode.R))
            foreach(EnemyManager enemy in enemies)
                if(enemy != null)
                    enemy.Angery = !enemy.Angery;
    }

    /// <summary>
    /// Input for saving and loading the player's position as a binary file, mostly for testing
    /// </summary>
    private void saveLoadInput()
    {
        // save the player's position
        if(Input.GetKeyDown(KeyCode.E))
        {
            Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
            PlayerData data = new PlayerData(playerPosition);

            Debug.Log("PlayerData saved.");
            SaveLoadManager.SavePlayerPosition(data);
        }

        // load the player's position
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("PlayerData loaded.");
            Vector2 position = SaveLoadManager.LoadPlayerPosition();
            transform.position = position;
        }
    }
}
