using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire_spawn : MonoBehaviour
{
    public GameObject Entity;
    public Transform Spawn_Point;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (PlayerManager.instance.ActualCampFire == null)
                PlayerManager.instance.ActualCampFire = Entity;
            else if (Entity != PlayerManager.instance.ActualCampFire)
                PlayerManager.instance.UI_Interaction.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (PlayerManager.instance.ActualCampFire == null)
                PlayerManager.instance.ActualCampFire = Entity;
            if (PlayerManager.instance.UI_Interaction.activeSelf == true)
                if (Input.GetButtonDown("Interaction"))
                {
                    PlayerManager.instance.ActualCampFire = Entity;
                    PlayerManager.instance.UI_Interaction.SetActive(false);
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (PlayerManager.instance.UI_Interaction.activeSelf == true)
            {
                PlayerManager.instance.UI_Interaction.SetActive(false);
            }
        }
    }
}
