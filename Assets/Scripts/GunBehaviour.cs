
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;


    public Camera fpsCam;

    public Animator gunAnimations;

    public GameObject rocketLauncher;

    bool ready = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(ready){
                Shoot();
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            if(ready){
                switchWeapon();
            }
        }
    }

    void Shoot()
    {
        
        gunAnimations.Play("revolverShoot", 0, 0.0f);
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            
            GameObject hitObject = hit.transform.parent.gameObject;
            
            if (hitObject.GetComponent<Target>() != null)
            {   
                hitObject.GetComponent<Target>().Die();;
                
            }
        }
        ready = false;
    }

    void reload(){
        Debug.Log("reload");
        ready = true;
    }

    void switchWeapon(){
        rocketLauncher.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
