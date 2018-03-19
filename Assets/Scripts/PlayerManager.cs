using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    PlayerData data;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
            data = new PlayerData(playerPosition);

            Debug.Log("PlayerData saved.");
            SaveLoadManager.SavePlayerPosition(data);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("PlayerData loaded.");
            Vector2 position = SaveLoadManager.LoadPlayerPosition();
            transform.position = position;
        }
    }
}
