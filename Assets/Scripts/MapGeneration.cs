using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGeneration : MonoBehaviour
{

    //Variables
    public float roomHeight = 7f;
    public float baseAreaWidth = 100f;
    public float baseAreaLength = 200f;
    public float minRoomLength = 13f;
    public float roomAreaOffset = 1f;
    public int maxRooms = 10;
    int roomCount = 1;
    bool divideVert = true;

    //Lists
    List<Vector4> areaList = new List<Vector4>();
    List<Vector4> finalAreas = new List<Vector4>();
    List<int> areaNumber = new List<int>();
    List<GameObject> roomGB = new List<GameObject>();
    List<string> divisors = new List<string>();
    //Objectpooler
    ObjectPooler objectPooler;


    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        generateMap();
        generateTeleporter();
    }

    void generateMap()
    {
        areaList.Add(new Vector4(0, 0, baseAreaWidth, baseAreaLength));

        for (int i = 0; i < areaList.Count; i++)
        {

            Vector4 area = areaList[i];
            /* WORDAROUND SOLUTION SHOULD PROBABLY BE CHANGED IN THE FUTURE */
            if (isDivisible(area, i) && divisors.Count < maxRooms - 1)
            {
                if (divideVert)
                {
                    float divisor = Random.Range(area.x + minRoomLength, area.z - minRoomLength);
                    divisors.Add("v" + i + " " + divisor);
                    /* Debug.Log("Divisor: " + divisor); */
                    //left area
                    areaList.Add(new Vector4(area.x, area.y, divisor, area.w));
                    //right area
                    areaList.Add(new Vector4(divisor, area.y, area.z, area.w));
                    //next room is divided horizontally
                    divideVert = false;
                }
                else
                {
                    float divisor = Random.Range(area.y + minRoomLength, area.w - minRoomLength);
                    divisors.Add("h" + i + " " + divisor);
                    //upper area
                    areaList.Add(new Vector4(area.x, divisor, area.z, area.w));
                    //lower area
                    areaList.Add(new Vector4(area.x, area.y, area.z, divisor));
                    //next room is divided vertically
                    divideVert = true;
                }


            }
            else
            {
                if (!(roomCount > maxRooms))
                {
                    finalAreas.Add(area);
                    areaNumber.Add(i);
                    roomCount += 1;
                }

            }

        }
        Debug.Log("Divisors: " + divisors.Count);
        Debug.Log("FinalAreas: " + finalAreas.Count);
        Debug.Log("AreaList: " + areaList.Count);

        for (int i = 0; i < finalAreas.Count; i++)
        {
            //number of room/area in tree
            int roomNumber = areaNumber[i];
            //simplify area 
            Vector4 area = finalAreas[i];
            Vector4 roomVec4 = new Vector4(area.x + roomAreaOffset, area.y + roomAreaOffset, area.z - roomAreaOffset, area.w - roomAreaOffset);
            GameObject currentRoom = objectPooler.SpawnFromPool("room", new Vector3(roomVec4.x, 0, roomVec4.y), Quaternion.identity);
            roomGB.Add(currentRoom);
            currentRoom.GetComponent<room>().roomNumber = roomNumber;
            currentRoom.GetComponent<room>().roomVec4 = roomVec4;
            placeRoom(new Vector3(roomVec4.z - roomVec4.x, roomHeight, roomVec4.w - roomVec4.y), currentRoom, i);

        }

    }

    void generateTeleporter()
    {
        Debug.Log(roomGB.Count);
        for (int i = roomGB.Count - 1; i > 0; i--)
        {
            //room one
            Vector4 roomOneVec4 = roomGB[i].GetComponent<room>().roomVec4;
            int roomOneNumber = roomGB[i].GetComponent<room>().roomNumber;
            Vector3 telOnePosition = new Vector3(roomOneVec4.x + 3, 0, roomOneVec4.y + ((roomOneVec4.w - roomOneVec4.y) / 2));
            GameObject telOne = objectPooler.SpawnFromPool("teleporter", telOnePosition, Quaternion.Euler(-90, 0, 0));


            //room two
            Vector4 roomTwoVec4 = roomGB[i - 1].GetComponent<room>().roomVec4;
            int roomTwoNumber = roomGB[i - 1].GetComponent<room>().roomNumber;
            Vector3 telTwoPosition = new Vector3(roomTwoVec4.z - 3, 0, roomTwoVec4.y + ((roomTwoVec4.w - roomTwoVec4.y) / 2));
            GameObject telTwo = objectPooler.SpawnFromPool("teleporter", telTwoPosition, Quaternion.Euler(-90, 0, 0));

        }
    }
    bool isDivisible(Vector4 area, int i)
    {
        float currentAreaWidth = area.z - area.x;
        float currentAreaLength = area.w - area.y;
        float minAreaLength = minRoomLength + roomAreaOffset;
        /*         Debug.Log("Size of room " + i + ": " + currentAreaLength + " " + currentAreaWidth);
         */

        if (divideVert)
        {
            if (currentAreaLength > currentAreaWidth * 3)
            {
                divideVert = false;
            }
        }
        else
        {
            if (currentAreaWidth > currentAreaLength * 3)
            {
                divideVert = true;
            }
        }
        if (divideVert)
        {
            if (currentAreaWidth > minAreaLength * 2 && currentAreaLength > minAreaLength && roomCount < maxRooms - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (currentAreaWidth > minAreaLength && currentAreaLength > minAreaLength * 2 && roomCount < maxRooms - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    void placeRoom(Vector3 size, GameObject room, int i)
    {
        string istring = i.ToString();
        room.name = istring;
        room.transform.localScale = size;
        /*  Debug.Log("Scale of " + room + ": " + room.transform.localScale); */
    }

    void placeTeleporter(Vector3 position, GameObject teleporter)
    {

    }
}
