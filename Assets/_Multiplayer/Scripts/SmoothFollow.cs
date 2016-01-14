using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

    //Inspector Variables
    [SerializeField] private Transform target;
    [SerializeField] private float speed;


    //Private Variables
    private Vector3 offset;

    protected virtual void Awake()
    {
        offset = transform.position;
    }

    protected virtual void Update()
    {
        if (target == null)
            return;

        Vector3 finalPosition = target.position + offset;

        transform.position = Vector3.Slerp(transform.position, finalPosition, speed * Time.deltaTime);
    }

}
