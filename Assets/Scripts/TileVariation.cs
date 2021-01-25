using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVariation : MonoBehaviour
{
    public GameObject[] tilePresets;    // 0 = LeftRight Opening, 1 = LeftRightDown Opening, 2 = LeftRightTop Opening, 3 = LeftRightTopDown Opening.
    public GameObject[] doorBlockers;
    public LevelGenerator levelGenerator;
    public float timerStart;
    public LayerMask room;
    private float timerRemaining;
    private bool checkedRoom;
    //public Collider[] check;

    private void Start()
    {
        levelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();
        timerRemaining = timerStart;
        checkedRoom = false; 
    }

    private void Update()
    {
        if(timerStart < 0 && !checkedRoom)
        {
            checkedRoom = true;
            MakeRoom(levelGenerator.moveDistance, false);
            MakeRoom(-levelGenerator.moveDistance, true);
        }
        else
        {
            timerStart -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(new Vector3(transform.position.x + levelGenerator.moveDistance, transform.position.y, transform.position.z), 3);
        Gizmos.DrawSphere(new Vector3(transform.position.x - levelGenerator.moveDistance, transform.position.y, transform.position.z), 3);
    }

    private void MakeRoom(float distance, bool isLeft)
    {
        Vector3 newTilePosition = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        Collider[] check = Physics.OverlapSphere(newTilePosition, 3, room);
        //Debug.DrawLine(transform.position, new Vector3(transform.position.x + distance, transform.position.y, transform.position.z), Color.red, 5, false);
        
        if (check.Length == 0 && levelGenerator.randomRoomCount > 0)
        {
            int randomNum = Random.Range(0, 2);
            Debug.Log("Bongo " + randomNum);
            if (randomNum != 0)
            {
                Debug.Log("Boingo");
                if (isLeft)
                {
                    Debug.Log("Boink");
                    Instantiate(tilePresets[0], newTilePosition, Quaternion.identity);
                }
                else
                {
                    Debug.Log("Bonk");
                    Instantiate(tilePresets[1], newTilePosition, Quaternion.identity);
                }
                levelGenerator.randomRoomCount--;
            }
            else
            {
                if (isLeft)
                {
                    Debug.Log("Boink");
                    Instantiate(doorBlockers[0], new Vector3(transform.position.x + distance / 2, transform.position.y, transform.position.z), Quaternion.identity);
                }
                else
                {
                    Debug.Log("Bonk");
                    Instantiate(doorBlockers[1], new Vector3(transform.position.x + distance / 2, transform.position.y, transform.position.z), Quaternion.identity);
                }
            }
        }
    }

    
}
