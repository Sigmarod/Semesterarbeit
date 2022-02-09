using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public Animator death;
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            death.Play("targetDeath",0,0.0f);
        }
    }

     void Die(){
        Destroy(gameObject);
    }
}
