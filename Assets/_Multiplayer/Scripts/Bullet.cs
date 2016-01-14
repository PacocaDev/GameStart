using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    //Inspector Variables

    [SerializeField]
    private float speed = 70.0f;

    [SerializeField]
    private float maxAliveTime = 3.0f;

    private Rigidbody rigidBody;

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        rigidBody.velocity = transform.forward * speed;
        Destroy(gameObject, maxAliveTime);
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }

}
