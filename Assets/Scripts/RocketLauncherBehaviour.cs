using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherBehaviour : MonoBehaviour
{
    public GameObject opticalMissile;
    ObjectPooler objectPooler;
    public Animator rocketLauncherAnimations;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    
    void Shoot(){
        rocketLauncherAnimations.Play("rocketLauncherShoot", 0, 0.0f);
        GameObject currentMissile = objectPooler.SpawnFromPool("missile",opticalMissile.transform.position, Quaternion.identity);
        Quaternion launcher = this.transform.rotation;
        Vector3 direction = new Vector3(launcher.x,launcher.y,launcher.z);
        currentMissile.GetComponent<Missile>().shoot(direction);
    }
}
