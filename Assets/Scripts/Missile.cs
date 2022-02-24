using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 direction = new Vector3(0,0,20);

    public ParticleSystem blast;
    public ParticleSystem explosion;
    public GameObject model;
    public AudioSource rocketFire;
    public LayerMask targetMask;
    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().velocity = direction*40;
    }

    public void shoot(Vector3 givenDirection, Camera fpsCam){
        blast.Play();
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        direction = fpsCam.transform.forward;
        this.transform.rotation = Quaternion.LookRotation(direction,Vector3.up);
        model.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<CapsuleCollider>().enabled = true;
        rocketFire.enabled = true;

    }

    private void OnCollisionEnter(Collision other) {
        blast.Stop();
        model.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        rocketFire.enabled = false;
        explosion.Play();
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 6, targetMask);
        for(int i = 0; i < hitColliders.Length; i++){
            Debug.Log(hitColliders[i]);
            hitColliders[i].gameObject.GetComponentInParent<Target>().Die();
        }
    }
}
