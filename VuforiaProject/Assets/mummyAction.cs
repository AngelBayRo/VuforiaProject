using UnityEngine;
using System.Collections;

public class mummyAction : MonoBehaviour {

    public GameObject mummy;
    public float force;
    Animator anim;
    Rigidbody body;
    int deathHash = Animator.StringToHash("Death");
    int attackHash = Animator.StringToHash("Attack");

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Mock")
        {
            if (obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack")
                && name == obj.gameObject.name)
                anim.SetTrigger(deathHash);
            else
                obj.GetComponent<mockAction>().hitted();
            if (name == "Up")
                body.AddForce(0.0f, 0.0f, -20f * force);
            else if (name == "Left")
                body.AddForce(20f * force, 0.0f, 0.0f);
            else if (name == "Down")
                body.AddForce(0.0f, 0.0f, 20f * force);
            else
                body.AddForce(-20f * force, 0.0f, 0.0f);
            Destroy(mummy, 2f);
        }
    }
}
