using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public Animator death;
    public GameObject room;
    public GameObject model;

    public void Die()
    {
        room.GetComponent<room>().targetDies(this.gameObject);
        Destroy(gameObject);
    }
}
