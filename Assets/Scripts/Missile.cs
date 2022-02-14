using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 direction = new Vector3(0,0,20);
    public Camera fpsCam;
    public ParticleSystem blast;
    public ParticleSystem explosion;
    private void Awake() {
        fpsCam = FindObjectOfType<Camera>();
        Debug.Log(fpsCam);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().AddForce(direction*50,ForceMode.Force);
    }

    public void shoot(Vector3 givenDirection){
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        direction = fpsCam.transform.forward;
        this.transform.rotation = Quaternion.LookRotation(direction,Vector3.up);
    }

    private void OnTriggerEnter(Collider other) {
        blast.Stop();
        explosion.Play();
    }
}
