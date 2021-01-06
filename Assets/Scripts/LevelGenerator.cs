using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform startPosition;
    public GameObject[] tilePresets;    // 0 = LeftRight Opening, 1 = LeftRightDown Opening, 2 = LeftRightTop Opening, 3 = LeftRightTopDown Opening.
    private GameObject prevTilePlaced;

    private int leftVal;
    private int rightVal;
    private int downVal;
    private int directionInt;
    private int curTile;
    public float moveDistance;
    private float timer;
    public float timerReset;
    private bool stopGeneration = false;


    //  Values that dictate where it can 
    [Header("Direction Boundaries")]
    public int leftMax;
    public int rightMax;
    public int downMax;

    [Header("Direction Chance")]

    [Tooltip("Chance To Move Left \n(NOTE: ALL VALUES WILL BE IN PROPORTION TO EACHOTHER \nEX: 1:1:1 == 3:3:3)")]
    public int leftChance;
    [Tooltip("Chance To Move Right \n(NOTE: ALL VALUES WILL BE IN PROPORTION TO EACHOTHER \nEX: 1:1:1 == 3:3:3)")]
    public int rightChance;
    [Tooltip("Chance To Move Down \n(NOTE: ALL VALUES WILL BE IN PROPORTION TO EACHOTHER \nEX: 1:1:1 == 3:3:3)")]
    public int downChance;



    void Start()
    {
        int randInt = Random.Range(0, 4);
        transform.position = startPosition.position;
        prevTilePlaced = Instantiate(tilePresets[randInt], transform.position, Quaternion.identity);

        directionInt = Random.Range(1, 1 + leftChance + rightChance + downChance);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && !stopGeneration)
        {
            timer = timerReset;
            Place();
        }
    }

    void Place()
    {
        if (directionInt <= leftChance)
        {
            if(leftVal != leftMax)
            {
                Vector3 newPosition = new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z);
                transform.position = newPosition;

                curTile = Random.Range(0, tilePresets.Length);
                prevTilePlaced = Instantiate(tilePresets[curTile], transform.position, Quaternion.identity);

                Debug.Log("Left: " + directionInt);
                directionInt = Random.Range(1, 1 + leftChance + rightChance + downChance);
                while(directionInt <= leftChance + rightChance && directionInt > leftChance)
                {
                    directionInt = Random.Range(1, 1 + leftChance + rightChance + downChance);
                }
                Debug.Log("Direction: " + directionInt);
                leftVal++;
                rightVal--;
            }
            else
            {
                directionInt = leftChance + rightChance + downChance;
            }
        }
        else if(directionInt <= leftChance + rightChance && directionInt > leftChance)
        {
            if(rightVal != rightMax)
            {
                Vector3 newPosition = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);
                transform.position = newPosition;

                curTile = Random.Range(0, tilePresets.Length);
                prevTilePlaced = Instantiate(tilePresets[curTile], transform.position, Quaternion.identity);

                Debug.Log("Right: " + directionInt);

                directionInt = Random.Range(1 + leftChance, 1 + leftChance + rightChance + downChance);
                Debug.Log("Direction: " + directionInt);

                leftVal--;
                rightVal++;
            }
            else
            {
                directionInt = leftChance + rightChance + downChance;
            }
        }
        else if (directionInt <= leftChance + rightChance + downChance && directionInt > leftChance + rightChance)
        {
            if(downVal !=  downMax)
            {
                if(curTile != 1 && curTile != 3)
                {
                    curTile = tilePresets.Length - 1;
                    Destroy(prevTilePlaced);
                    prevTilePlaced = Instantiate(tilePresets[curTile], transform.position, Quaternion.identity);
                }

                Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance);
                transform.position = newPosition;

                curTile = Random.Range(2, tilePresets.Length);
                prevTilePlaced = Instantiate(tilePresets[curTile], transform.position, Quaternion.identity);

                Debug.Log("Down: " + directionInt);
                downVal++;
                directionInt = Random.Range(1, 1 + leftChance + rightChance + downChance);
                Debug.Log("Direction: " + directionInt);
            }
            else
            {
                stopGeneration = true;
                Debug.Log("HitBottom");
            }
        }
        //Instantiate(tilePresets[0], transform.position, Quaternion.identity);
    }
}
