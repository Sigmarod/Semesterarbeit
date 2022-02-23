using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public float range = 100f;

    public Camera fpsCam;

    public Animator gunAnimations;

    public GameObject rocketLauncher;
    public GameController gameManager;
    private float animLength = 1.5f;

    private bool gameIsPaused = false;
    private bool ready = true;
    // Update is called once per frame
    void Update()
    {
        if (!gameIsPaused)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("key ist detected");
                if (ready)
                {
                    Shoot();
                    ready = false;
                }

            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (ready)
                {
                    switchWeapon();
                    ready = false;
                }
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
        
    }

    void switchWeapon()
    {
        rocketLauncher.SetActive(true);
        ready = true;
        this.gameObject.SetActive(false);
    }
    IEnumerator reloading()
    {
        yield return new WaitForSeconds(animLength);
        ready = true;
    }

    public void pauseGame(){
        gameIsPaused = !gameIsPaused;
        Debug.Log(gameIsPaused);
    }

    
}
