using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NetworkIdentity))]
public class PlayerMovementController : MonoBehaviour {

    //Inspector Variables
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float speed;

    //Private Variables
    private float verticalInput;
    private float horizontalInput;
    private Vector3 lookingPosition;
    private NavMeshAgent navMeshAgent;
    private NetworkIdentity networkIdentity;
    
    protected virtual void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        navMeshAgent = GetComponent<NavMeshAgent>();
        networkIdentity = GetComponent<NetworkIdentity>();

        speed = 8.0f;
    }
    
    // Use this for initialization
	protected virtual void Start () {
	
	}
	
	// Update is called once per frame
	 protected virtual void Update () {

        if (!networkIdentity.isLocalPlayer)
            return;

        DetectInput();
        MovePlayer();
        LookToPointer();
	}

    private void LookToPointer()
    {
        Quaternion lookRotation = Quaternion.LookRotation(lookingPosition - transform.position, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed * Time.deltaTime);
    }

    private void MovePlayer()
    {
        Vector3 targetPosition = new Vector3(horizontalInput*speed,0,verticalInput*speed);
        navMeshAgent.Move(targetPosition*Time.deltaTime);
    }

    private void DetectInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        Ray cameraToGround = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(cameraToGround, out hit, 50.0f, groundLayer))
            lookingPosition = hit.point;
        
    }
}
