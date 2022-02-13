using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool telOff = false;
    public GameObject partner;
    public GameObject player;
    public GameObject room;

    public int roomCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            Vector3 destination = partner.transform.position;
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = destination;
            player.GetComponent<CharacterController>().enabled = true;
            partner.GetComponent<Teleporter>().telOff = true;
            partner.GetComponent<Teleporter>().room.GetComponent<room>().playerEnter(roomCount);        }
    }
}


