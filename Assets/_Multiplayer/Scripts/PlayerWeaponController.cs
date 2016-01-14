using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkIdentity))]
public class PlayerWeaponController : NetworkBehaviour {

    //Inspector Variables
    [SerializeField]
    private float weaponCooldown = 0.2f;

    [SerializeField]
    private Bullet bulletPrefab;

    //Private Variables
    private float lastShootTime = float.MaxValue;
    private NetworkIdentity networkIdentity;

    protected virtual void Awake()
    {
        
        networkIdentity = GetComponent<NetworkIdentity>();
        
    }

    protected virtual void Update()
    {
        if (!networkIdentity.isLocalPlayer)
            return;

        if (Input.GetMouseButton(0) && lastShootTime > weaponCooldown)
        {
            lastShootTime = 0;
            CmdShoot();
        }
        lastShootTime += Time.deltaTime;
    }

    [Command]
    private void CmdShoot()
    {
        Bullet bulletClone = Instantiate(bulletPrefab);

        bulletClone.transform.position = transform.position+transform.forward*1.0f;
        bulletClone.transform.rotation = transform.rotation;

        NetworkServer.Spawn(bulletClone.gameObject);
    }
}
