using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room : MonoBehaviour
{
    public int roomNumber;
    public Vector4 roomVec4 = new Vector4(0, 0, 0, 0);

    public GameObject telIn;
    public GameObject telOut;

    private int deadTargets = 0;
    public bool roomClear = false;
    public List<GameObject> targets = new List<GameObject>();

    public void playerEnter(int roomCount)
    {

        if (!roomClear)
        {
            if (this.gameObject.tag.Equals("firstRoom"))
            {
                telOut.GetComponent<ParticleSystem>().Stop();
            }
            else
            {
                if (this.gameObject.tag.Equals("lastRoom"))
                {
                    telIn.GetComponent<ParticleSystem>().Stop();
                }
                else
                {
                    telIn.GetComponent<ParticleSystem>().Stop();
                    telOut.GetComponent<ParticleSystem>().Stop();
                }
            }


        }
    }
    public void targetDies(GameObject target)
    {
        deadTargets++;
        if (deadTargets == targets.Count)
        {
            Debug.Log("All Targets in Room " + roomNumber + " are dead.");
            roomClear = true;
            if (this.gameObject.tag != "lastRoom")
            {
                telOut.GetComponent<CapsuleCollider>().enabled = true;
                telOut.GetComponent<ParticleSystem>().Play();
            }


        }
    }
}



