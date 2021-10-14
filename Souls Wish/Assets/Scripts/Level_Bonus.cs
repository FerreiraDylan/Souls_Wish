using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Bonus : MonoBehaviour
{
    public GameObject BCard_Heal;
    public GameObject BCard_Speed;
    public GameObject BCard_Damage;

    public Text bonuses_Allowed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bonuses_Allowed.text = "Bonuses allowed: " + GameManager.instance.selected_bonus + "/" + GameManager.instance.bonus_allowed;
    }

    public void ApplyBonuses()
    {
        var tmp = GameManager.instance.Player.GetComponent<PlayerManager>();
        tmp.Heal = (GameManager.instance.B_Heal == 0 ? GameManager.instance.Heal : GameManager.instance.B_Heal);
        tmp.CurrentSpeed = (GameManager.instance.B_Speed == 0 ? GameManager.instance.Speed : GameManager.instance.B_Speed);
        tmp.Speed = (GameManager.instance.B_Speed == 0 ? GameManager.instance.Speed : GameManager.instance.B_Speed);
        tmp.Damage = (GameManager.instance.B_Damage == 0 ? GameManager.instance.Damage : GameManager.instance.B_Damage);

        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ResetBonuses()
    {
        GameManager.instance.selected_bonus = GameManager.instance.bonus_allowed;
        GameManager.instance.B_Heal = 0f;
        GameManager.instance.B_Speed = 0f;
        GameManager.instance.B_Damage = 0f;
        BCard_Heal.SetActive(true);
        BCard_Speed.SetActive(true);
        BCard_Damage.SetActive(true);
    }
}
