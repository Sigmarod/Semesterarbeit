using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherBehaviour : MonoBehaviour
{
    public GameObject opticalMissile;
    ObjectPooler objectPooler;
    public Animator rocketLauncherAnimations;
    public GameObject currentMissile;

    public Camera fpsCam;
    public GameObject revolver;
    private float animLength = 2.5f;
    private bool ready = true;
    private bool gameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameIsPaused){
            if (Input.GetMouseButton(0))
        {
            if (ready)
            {
                Shoot();
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (ready)
            {
                Debug.Log("switch");
                switchWeapon();
            }

        }
        }
        
    }

    void Shoot()
    {
        rocketLauncherAnimations.Play("rocketLauncherShoot", 0, 0.0f);
        currentMissile = objectPooler.SpawnFromPool("missile", opticalMissile.transform.position, Quaternion.identity);
        Quaternion launcher = this.transform.rotation;
        Vector3 direction = new Vector3(launcher.x, launcher.y, launcher.z);
        currentMissile.GetComponent<Missile>().shoot(direction, fpsCam);
        ready = false;
        StartCoroutine(reloading());
    }


    void switchWeapon()
    {

        revolver.SetActive(true);
        this.gameObject.SetActive(false);


    }

    IEnumerator reloading()
    {
        yield return new WaitForSeconds(animLength);
        // trigger the stop animation events here
        Debug.Log("rreload");
        ready = true;
    }

    public void pauseGame()
    {
        gameIsPaused = !gameIsPaused;
    }
}
