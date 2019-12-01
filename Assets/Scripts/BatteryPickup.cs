﻿using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 90f;
    [SerializeField] float addIntensity = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
            weapon.GetComponentInChildren<FlashlightSystem>().RestoreLightAngle(restoreAngle);
            weapon.GetComponentInChildren<FlashlightSystem>().AddLightIntensity(addIntensity);
            Destroy(gameObject);
        }
    }

}
