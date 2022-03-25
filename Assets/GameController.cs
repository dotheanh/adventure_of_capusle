using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private const bool IS_TEST_MODE = false;
    public GameObject player;
    public KeyItem keyItem1, keyItem2, keyItem3;
    private List<KeyItem> keyItems;
    public Text TextGameInfo;
    public GameObject portalDoor;

    private int keyItemTotal;
    public GameObject spotlightPrefab;
    
    void Start()
    {
        Screen.autorotateToLandscapeLeft = true;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        StartCoroutine(SpawnKeyItems());
        portalDoor.SetActive(false);

    }

    IEnumerator SpawnKeyItems()
    {
        yield return new WaitForSeconds(1);

        // spawn key items to the forest
        if (IS_TEST_MODE) {
            keyItems = new List<KeyItem> { keyItem1 };
        }
        else {
            keyItems = new List<KeyItem> { keyItem1, keyItem2, keyItem3 };
        }
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
           
           AddSpotlightToObjects(keyItem.gameObject);
        }
    }

    void AddSpotlightToObjects(GameObject gameObject)
    {
        // add spotlight to object, easier to see in minimap
        GameObject child = GameObject.Instantiate(spotlightPrefab);
        child.transform.SetParent(gameObject.transform, false); // set worldPositionStays to false, then it use local position
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
                Destroy(keyItem.gameObject);
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
        portalDoor.SetActive(true);
        yield return new WaitForSeconds(1);
        TextGameInfo.text = "Congratulations! You have completed the level.";
        player.transform.position = portalDoor.transform.position + new Vector3(-3, 0, 3); // move player to front of the door
        player.transform.LookAt(portalDoor.transform.position);
    }
}
