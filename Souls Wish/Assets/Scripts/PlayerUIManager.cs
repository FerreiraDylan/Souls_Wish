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

    public Slider UICustom_Health;
    public Slider UICustom_Shield;

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
}
