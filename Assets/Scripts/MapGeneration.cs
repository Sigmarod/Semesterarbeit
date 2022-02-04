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
    List<Vector4> rooms = new List<Vector4>();
    List<string> divisors = new List<string>();
    //Objectpooler
    ObjectPooler objectPooler;


    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        generateMap();
    }

    void generateMap()
    {
        areaList.Add(new Vector4(0, 0, baseAreaWidth, baseAreaLength));

        for (int i = 0; i < areaList.Count; i++)
        {
            
            Vector4 area = areaList[i];
            /* WORDAROUND SOLUTION SHOULD PROBABLY BE CHANGED IN THE FUTURE */
            if (isDivisible(area, i) && divisors.Count < maxRooms-1)
            {
                Debug.Log(roomCount);
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
                    divisors.Add("v" + i + " " + divisor);
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
                /*  Debug.Log("Area " + i + " isn't divisible."); */
                if (!(roomCount > maxRooms))
                {
                    finalAreas.Add(area);
                    roomCount += 1;
                }

            }

        }
        Debug.Log("Divisors: " + divisors.Count);
        Debug.Log("FinalAreas: " + finalAreas.Count);
        Debug.Log("AreaList: " + areaList.Count);

        for (int i = 0; i < finalAreas.Count; i++)
        {

            Vector4 area = finalAreas[i];
            /*             Debug.Log("AREA "+i+": "+area);
             */
            rooms.Add(new Vector4(area.x + roomAreaOffset, area.y + roomAreaOffset, area.z - roomAreaOffset, area.w - roomAreaOffset));

            Vector4 room = rooms[i];
            GameObject currentRoom = objectPooler.SpawnFromPool("room", new Vector3(room.x, 0, room.y), Quaternion.identity);
            placeRoom(new Vector3(room.z - room.x, roomHeight, room.w - room.y), currentRoom, i);

            Debug.Log(i + " " + area + " " + room);

        }

    }
    bool isDivisible(Vector4 area, int i)
    {
        float currentAreaWidth = area.z - area.x;
        float currentAreaLength = area.w - area.y;
        float minAreaLength = minRoomLength + roomAreaOffset;
        /*         Debug.Log("Size of room " + i + ": " + currentAreaLength + " " + currentAreaWidth);
         */

        if (divideVert){
            if(currentAreaLength > currentAreaWidth*3){
                divideVert = false;
            }
        }else{
            if(currentAreaWidth > currentAreaLength*3){
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
        Debug.Log("Scale of " + room + ": " + room.transform.localScale);
    }
}
