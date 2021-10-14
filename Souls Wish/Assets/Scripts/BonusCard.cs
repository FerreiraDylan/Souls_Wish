using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCard : MonoBehaviour
{
    public bool heal;
    public bool speed;
    public bool damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddBonus()
    {
        if (heal && GameManager.instance.selected_bonus > 0)
        {
            GameManager.instance.selected_bonus -= 1;
            GameManager.instance.B_Heal = 45f;
            gameObject.SetActive(false);
        }
        if (speed && GameManager.instance.selected_bonus > 0)
        {
            GameManager.instance.selected_bonus -= 1;
            GameManager.instance.B_Speed = 9f;
            gameObject.SetActive(false);
        }
        if (damage && GameManager.instance.selected_bonus > 0)
        {
            GameManager.instance.selected_bonus -= 1;
            GameManager.instance.B_Damage = 50f;
            gameObject.SetActive(false);
        }
    }
}
