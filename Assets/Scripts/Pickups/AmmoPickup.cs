using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(other.GetComponent<WeaponZoom>()) //to differenciate between my player and the EasyFPS player
            {
                FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
                Destroy(gameObject);
            }
            else
            {
                FindObjectOfType<GunScript>().IncreaseBulletsAmount(ammoAmount);
                Destroy(gameObject);
            }
        }
    }
}
