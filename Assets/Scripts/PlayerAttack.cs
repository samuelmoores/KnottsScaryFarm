using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform Hand;
    GameObject Weapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AquireWeapon()
    {
        Rigidbody rb = Weapon.GetComponent<Rigidbody>();

        if (rb.linearVelocity == Vector3.zero)
        {
            Weapon.GetComponent<MeshCollider>().enabled = false;
            rb.useGravity = false;
            Weapon.transform.position = Vector3.zero;
            Weapon.transform.SetParent(Hand, false);
        }
    }

}
