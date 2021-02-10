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
    public bool dontWantRandomRooms;
    private bool checkedRoom;
    public float distance;

    private void Start()
    {
        levelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();
        timerRemaining = timerStart;
        checkedRoom = false;
    }

    private void Update()
    {
        if (timerStart < 0 && !checkedRoom)
        {
            checkedRoom = true;
            if(!dontWantRandomRooms)
            {
                MakeRoom(distance, false);
                MakeRoom(-distance, true);
            }
            blockOpenings();
        }
        else
        {
            timerStart -= Time.deltaTime;
        }
    }

    private void MakeRoom(float distance, bool isLeft)
    {
        Vector3 newTilePosition = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        Collider[] check = Physics.OverlapSphere(newTilePosition, 3, room);

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
        }
    }

    private void blockOpenings()
    {
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    Collider[] check = Physics.OverlapSphere(new Vector3(transform.position.x - distance, transform.position.y, transform.position.z), 3, room);
                    if(check.Length == 0)
                    {
                        Instantiate(doorBlockers[i], new Vector3(transform.position.x - distance / 2, transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    break;

                case 1:
                    check = Physics.OverlapSphere(new Vector3(transform.position.x + distance, transform.position.y, transform.position.z), 3, room);
                    if (check.Length == 0)
                    {
                        Instantiate(doorBlockers[i], new Vector3(transform.position.x + distance / 2, transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    break;

                case 2:
                    check = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + distance), 3, room);
                    if (check.Length == 0)
                    {
                        Instantiate(doorBlockers[i], new Vector3(transform.position.x, transform.position.y, transform.position.z + distance / 2), Quaternion.identity);
                    }
                    break;

                case 3:
                    check = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z - distance), 3, room);
                    if (check.Length == 0)
                    {
                        Instantiate(doorBlockers[i], new Vector3(transform.position.x, transform.position.y, transform.position.z - distance / 2), Quaternion.identity);
                    }
                    break;
            }
        }
    }
}
