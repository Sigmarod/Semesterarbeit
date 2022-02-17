using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGeneration : MonoBehaviour
{

    //Variables
    public float roomHeight = 10f;
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
    //Player
    GameObject player;
    //UI
    public GameObject UI;


    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        generateMap();
        
    }
    public void generateMap(){
        UI.GetComponent<UIController>().timerFunction();
        generateRooms();
        generateTeleporter();
        generateTargetCountsList();
        generatePlayer();
        roomGB[0].GetComponent<room>().playerEnter(roomGB.Count);
    }
    void generateRooms()
    {
        areaList.Add(new Vector4(0, 0, baseAreaWidth, baseAreaLength));

        for (int i = 0; i < areaList.Count; i++)
        {

            Vector4 area = areaList[i];
            if (isDivisible(area, i) && divisors.Count < maxRooms - 1)
            {
                if (divideVert)
                {
                    float divisor = Random.Range(area.x + minRoomLength, area.z - minRoomLength);
                    divisors.Add("v" + i + " " + divisor);
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
            currentRoom.layer = 6;

        }

    }

    void generateTeleporter()
    {
        for (int i = roomGB.Count - 1; i > 0; i--)
        {
            room roomScript = roomGB[i].GetComponent<room>();
            room roomScript2 = roomGB[i - 1].GetComponent<room>();
            //room one
            Vector4 roomOneVec4 = roomScript.roomVec4;
            int roomOneNumber = roomScript.roomNumber;
            Vector3 telOnePosition = new Vector3(roomOneVec4.x + 3, 0, roomOneVec4.y + 3);
            GameObject telOne = objectPooler.SpawnFromPool("teleporter", telOnePosition, Quaternion.Euler(-90, 0, 0));
            roomScript.telIn = telOne;
            Teleporter telOneScript = telOne.GetComponent<Teleporter>();
            telOneScript.room = roomGB[i];
            telOneScript.roomCount = roomGB.Count;

            //room two
            Vector4 roomTwoVec4 = roomScript2.roomVec4;
            int roomTwoNumber = roomScript2.roomNumber;
            Vector3 telTwoPosition = new Vector3(roomTwoVec4.z - 3, 0, roomTwoVec4.w - 3);
            GameObject telTwo = objectPooler.SpawnFromPool("teleporter", telTwoPosition, Quaternion.Euler(-90, 0, 0));
            roomScript2.telOut = telTwo;
            Teleporter telTwoScript = telTwo.GetComponent<Teleporter>();
            telTwoScript.room = roomGB[i - 1];
            telTwoScript.roomCount = roomGB.Count;

            //connect the teleporters
            telOneScript.partner = telTwo;
            telTwoScript.partner = telOne;
            if (i == roomGB.Count - 1)
            {
                roomGB[i].tag = "lastRoom";
            }
            else
            {
                if (i == 1)
                {
                    roomGB[i - 1].tag = "firstRoom";
                }
            }

        }
    }

    void generateTargets(List<int> targetCountsList, List<int> armTargetCountsList)
    {

        for (int i = 0; i < roomGB.Count; i++)
        {
            Vector4 rV4 = roomGB[i].GetComponent<room>().roomVec4;

            for (int a = 0; a < targetCountsList[i]; a++)
            {
                GameObject currentTarget = objectPooler.SpawnFromPool("target", new Vector3(Random.Range(rV4.x + 3, rV4.z - 3), 2, Random.Range(rV4.y + 3, rV4.w - 3)), Quaternion.identity);
                roomGB[i].GetComponent<room>().targets.Add(currentTarget);
                currentTarget.GetComponent<Target>().room = roomGB[i];
                currentTarget.GetComponent<Target>().UI = UI;
            }

            for (int a = 0; a < armTargetCountsList[i]; a++)
            {
                GameObject currentTarget = objectPooler.SpawnFromPool("armorTarget", new Vector3(Random.Range(rV4.x + 3, rV4.z - 3), 2, Random.Range(rV4.y + 3, rV4.w - 3)), Quaternion.identity);
                roomGB[i].GetComponent<room>().targets.Add(currentTarget);
                currentTarget.GetComponent<Target>().room = roomGB[i];
                currentTarget.GetComponent<Target>().UI = UI;
            }

        }
    }

    void generateTargetCountsList()
    {

        List<int> targetCountsList = new List<int>();
        List<int> armTargetCountsList = new List<int>();
        int listSum = 0;
        int armListSum = 0;
        int maxTargets = objectPooler.GetComponent<ObjectPooler>().pools[3].size;
        int maxArmTargets = objectPooler.GetComponent<ObjectPooler>().pools[5].size;
        for (int i = 0; i < roomGB.Count; i++)
        {
            float roomWith = roomGB[i].transform.localScale.x;
            float roomLength = roomGB[i].transform.localScale.z;
            float roomSize = roomWith * roomLength;
            int targetCount = (int)(roomSize / 350);
            targetCountsList.Add(targetCount);
            listSum = listSum + targetCount;
        }
        if (listSum > maxTargets)
        {
            List<int> copiedTargetCountsList = targetCountsList;
            targetCountsList.Sort();
            int difference = listSum - maxTargets;
            int highest = targetCountsList[0];
            for (int a = 0; a < copiedTargetCountsList.Count; a++)
            {
                if (copiedTargetCountsList[a] == highest)
                {
                    copiedTargetCountsList[a] = copiedTargetCountsList[a] - difference;
                }
            }
            targetCountsList = copiedTargetCountsList;
        }


        for (int i = 0; i < roomGB.Count; i++)
        {
            float roomWith = roomGB[i].transform.localScale.x;
            float roomLength = roomGB[i].transform.localScale.z;
            float roomSize = roomWith * roomLength;
            int targetCount = (int)(roomSize / 650);
            armTargetCountsList.Add(targetCount);
            armListSum = armListSum + targetCount;
        }
        if (armListSum > maxArmTargets)
        {
            List<int> copiedArmTargetCountsList = armTargetCountsList;
            armTargetCountsList.Sort();
            int difference = listSum - maxTargets;
            int highest = armTargetCountsList[0];
            for (int a = 0; a < copiedArmTargetCountsList.Count; a++)
            {
                if (copiedArmTargetCountsList[a] == highest)
                {
                    copiedArmTargetCountsList[a] = copiedArmTargetCountsList[a] - difference;
                }
            }
            generateTargets(targetCountsList, copiedArmTargetCountsList);

        }
        else
        {
            generateTargets(targetCountsList, armTargetCountsList);

        }
    }
    void generatePlayer()
    {
        float x = roomGB[0].GetComponent<room>().roomVec4.x + 3;
        float z = roomGB[0].GetComponent<room>().roomVec4.y + 3;
        player = objectPooler.SpawnFromPool("player", new Vector3(x, 1, z), Quaternion.identity);
        player.tag = "Player";
    }
    bool isDivisible(Vector4 area, int i)
    {
        float currentAreaWidth = area.z - area.x;
        float currentAreaLength = area.w - area.y;
        float minAreaLength = minRoomLength + roomAreaOffset;

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
    }

}
