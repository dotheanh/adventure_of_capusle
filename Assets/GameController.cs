using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public KeyItem keyItem1, keyItem2, keyItem3;
    private List<KeyItem> keyItems;
    public Text TextGameInfo;

    private int keyItemTotal;
    
    void Start()
    {
        Screen.autorotateToLandscapeLeft = true;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        StartCoroutine(SpawnKeyItems());

    }

    IEnumerator SpawnKeyItems()
    {
        yield return new WaitForSeconds(1);

        // spawn key items to the forest
        keyItems = new List<KeyItem> { keyItem1, keyItem2, keyItem3 };
        keyItemTotal = keyItems.Count;
        TextGameInfo.text = "Find all " + keyItemTotal + " key-items to complete this level.";
        foreach(KeyItem keyItem in keyItems)
        {
            float min = 230f;
            float max = 250f;
            int randomPositiveX = (Random.Range(0f, 1f) > 0.5f) ? 1 : -1;
            int randomPositiveZ = (Random.Range(0f, 1f) > 0.5f) ? 1 : -1;
            Vector3 spawnPosition = new Vector3(randomPositiveX*Random.Range(min, max), 
                                                max + 50f, 
                                                randomPositiveZ*Random.Range(min, max));
            Debug.Log("spawnPosition " + spawnPosition.x + ", " + spawnPosition.y + ", " + spawnPosition.z);
            keyItem.SetSpawnPosition(spawnPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (keyItems != null && keyItems.Count > 0) {
            CheckFoundItem();
        }
    }

    void CheckFoundItem()
    {
        if (player.transform.position.y > 20) return;   // did not leave the stage yet
        for (int i = 0; i < keyItems.Count; i++)
        {
            KeyItem keyItem = keyItems[i];
            float dist = Vector3.Distance(keyItem.transform.position, player.transform.position);
            if (dist < 3) 
            {
                Destroy(keyItem.gameObject, 1);
                keyItems.RemoveAt(i);
                i--;
                Debug.Log("Congrat, you found it");
                TextGameInfo.text = "Found " + (keyItemTotal - keyItems.Count) + "/" + keyItemTotal;
                if (keyItems.Count == 0) {
                    StartCoroutine(OnLevelCompleted());
                }
            }
        }
    }

    IEnumerator OnLevelCompleted()
    {
        yield return new WaitForSeconds(1);
        TextGameInfo.text = "Congratulations! You have completed the level.";
    }
}
