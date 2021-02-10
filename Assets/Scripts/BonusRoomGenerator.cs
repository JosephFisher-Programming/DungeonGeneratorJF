using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BonusRoomGenerator : MonoBehaviour
{
    public bool isRandom;
    public bool isLeft;
    public int maxRandomDistance;
    public int forward, down, up;
    public string[] directions;
    public float distance;
    public GameObject bonusRoomTile;
    public GameObject finalTile;
    public LayerMask tile;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(bonusRoomTile, transform.position, Quaternion.identity);
        if (!isRandom)
        {
            PremadeDungeon();
        }
        else
        {
            RandomDungeon();
        }
    }

    public void PremadeDungeon()
    {
        int stringCount = 0;
        foreach (string s in directions)
        {
            Debug.Log("Running");
            stringCount++;

            switch (s)
            {
                case "left":
                    if(stringCount == directions.Length)
                    {
                        transform.position = new Vector3(transform.position.x - distance * 2f, transform.position.y, transform.position.z);
                        bonusRoomTile = finalTile;
                        break;
                    }
                    transform.position = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
                    break;

                case "right":
                    if (stringCount == directions.Length)
                    {
                        transform.position = new Vector3(transform.position.x + distance * 2f, transform.position.y, transform.position.z);
                        bonusRoomTile = finalTile;
                        break;
                    }
                    transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
                    break;

                case "up":
                    if (stringCount == directions.Length)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance * 2f);
                        bonusRoomTile = finalTile;
                        break;
                    }
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
                    break;

                case "down":
                    if (stringCount == directions.Length)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance * 2f);
                        bonusRoomTile = finalTile;
                        break;
                    }
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance);
                    break;
            }
            Instantiate(bonusRoomTile, transform.position, Quaternion.identity);
        }
    }

    public void RandomDungeon()
    {
        int direction = Random.Range(1, 4);
        float forwardMovement = distance;

        if (isLeft)
            forwardMovement = -forwardMovement;

        for(int i = 0; i < maxRandomDistance; i++)
        {
            switch (direction)
            {
                case 1:
                    Collider[] check = Physics.OverlapSphere(new Vector3(transform.position.x + forwardMovement, transform.position.y, transform.position.z), 3, tile);
                    if(maxRandomDistance - i == 1)
                    {
                        check = Physics.OverlapSphere(new Vector3(transform.position.x + forwardMovement * 2f, transform.position.y, transform.position.z), 9, tile);
                        if (check.Length == 0)
                        {
                            transform.position = new Vector3(transform.position.x + forwardMovement * 2f, transform.position.y, transform.position.z);
                            bonusRoomTile = finalTile;
                            break;
                        }
                        direction = 1;
                        i -= 2;
                        continue;
                    }
                    transform.position = new Vector3(transform.position.x + forwardMovement, transform.position.y, transform.position.z);
                    break;

                case 2:
                    check = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + distance), 3, tile);
                    if (maxRandomDistance - i == 1)
                    {
                        check = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + distance * 2f), 9, tile);
                        if (check.Length == 0)
                        {   
                            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance * 2f);
                            bonusRoomTile = finalTile;
                            break;
                        }
                        direction = 1;
                        i -= 2;
                        continue;
                    }
                    if (check.Length != 0)
                    {
                        direction = 1;
                        i -= 2;
                        continue;
                    }
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
                    break;

                case 3:
                    check = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z - distance), 3, tile);
                    if (maxRandomDistance - i == 1)
                    {
                        check = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z - distance * 2f), 9, tile);
                        if(check.Length == 0)
                        {
                            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance * 2f);
                            bonusRoomTile = finalTile;
                            break;
                        }
                        direction = 1;
                        i -= 2;
                        continue;
                    }
                    if (check.Length != 0)
                    {
                        direction = 1;
                        i -= 2;
                        continue;
                    }
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance);
                    break;
            }

            Instantiate(bonusRoomTile, transform.position, Quaternion.identity);
            direction = Random.Range(1, 4);
        }
    }
}

/*[CustomEditor(typeof(BonusRoomGenerator))]
public class BonusRoomGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myScript = target as BonusRoomGenerator;

        myScript.isRandom = GUILayout.Toggle(myScript.isRandom, "IsRandom");

        if(myScript.isRandom)
        {
            myScript.isLeft = GUILayout.Toggle(myScript.isLeft, "IsLeft");
            myScript.forward = EditorGUILayout.IntSlider("Forward", myScript.forward, 1, 100);
            myScript.up = EditorGUILayout.IntSlider("Up", myScript.up, 1, 100);
            myScript.down = EditorGUILayout.IntSlider("Down", myScript.down, 1, 100);
        }
        else
        {
            myScript.directions = EditorGUI.TextArea("Directions", myScript.directions);
        }
    }
}*/
