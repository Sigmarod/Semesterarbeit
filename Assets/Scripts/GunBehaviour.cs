using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;


    public Camera fpsCam;

    public Animator gunAnimations;

    public GameObject rocketLauncher;
    public GameController gameManager;
    private float animLength = 0.59f;

    bool ready = true;
    // Update is called once per frame
    void Update()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            if (ready)
            {
                Shoot();
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (ready)
            {
                switchWeapon();
            }
        }
    }

    void Shoot()
    {

        gunAnimations.Play("revolverShoot", 0, 0.0f);
        StartCoroutine(reloading());
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            GameObject hitObject = hit.transform.parent.gameObject;
            if (hitObject.layer == 7 && hitObject.tag != "armor")
            {
                if (hitObject.GetComponent<Target>() != null)
                {
                    hitObject.GetComponent<Target>().Die(); ;

                }
            }


        }
        ready = false;
    }

    void switchWeapon()
    {
        rocketLauncher.SetActive(true);
        this.gameObject.SetActive(false);
    }

    IEnumerator reloading()
    {
        yield return new WaitForSeconds(animLength);
        // trigger the stop animation events here
        Debug.Log("rreload");
        ready = true;
    }
}
