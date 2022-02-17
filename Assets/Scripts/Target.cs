using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public Animator death;
    public GameObject room;
    public GameObject model;
    public GameObject UI;
    float maxHeight =6f;
    float minHeight = 1f;
    bool hoverUp = true;
    Transform targetTransform;
    private void Start() {
        targetTransform = this.gameObject.transform;
    }
    private void Update() {
        if(this.gameObject.tag != "armor"){
            if(hoverUp){
                targetTransform.position += new Vector3(0,0.005f,0);
                if(targetTransform.position.y >= maxHeight){
                    hoverUp = false;
                }
            }else{
                targetTransform.position += new Vector3(0,-0.005f,0);
                if(targetTransform.position.y <= minHeight){
                    hoverUp = true;
                }
            }
        }
    }
    public void Die()
    {
        room.GetComponent<room>().targetDies(this.gameObject);
        UI.GetComponent<UIController>().addScore();
        Destroy(gameObject);
    }
}
