using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public GameObject Player_UIDefault;
    public GameObject Player_UICustom;

    public Slider UIDefault_Health;
    public Slider UIDefault_Shield;
    public Text UIDefault_Potions;
    public Text UIDefault_Coins;

    public Slider UICustom_Health;
    public Slider UICustom_Shield;
    public Text UICustom_Potions;
    public Text UICustom_Coins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player_UI();
    }
    public void Player_UI()
    {
        Player_UICustom.SetActive(!InputManager.instance.PlayerUI);
        Player_UIDefault.SetActive(InputManager.instance.PlayerUI);
    }

    // HEALTH //
    public void SetPlayerUI_Health(float MaxHealth, float CurrentHealth)
    {
        UIDefault_Health.maxValue = MaxHealth;
        UIDefault_Health.value = CurrentHealth;
        UICustom_Health.maxValue = MaxHealth;
        UICustom_Health.value = CurrentHealth;
    }
    public void UpdatePlayerUI_Health(float health)
    {
        UIDefault_Health.value = health;
        UICustom_Health.value = health;
    }

    // SHIELD //
    public void SetPlayerUI_Shield(float MaxShield, float CurrentShield)
    {
        UIDefault_Shield.maxValue = MaxShield;
        UIDefault_Shield.value = CurrentShield;
        UICustom_Shield.maxValue = MaxShield;
        UICustom_Shield.value = CurrentShield;
    }
    public void UpdatePlayerUI_Shield(float shield)
    {
        UIDefault_Shield.value = shield;
        UICustom_Shield.value = shield;
    }

    // POTIONS //
    public void UpdatePlayerUI_HealPotions(int potions)
    {
        UIDefault_Potions.text = potions.ToString();
        UICustom_Potions.text = potions.ToString();
    }

    // Coins //
    public void UpdatePlayerUI_Coins(int coins)
    {
        UIDefault_Coins.text = coins.ToString();
        UICustom_Coins.text = coins.ToString();
    }
}
