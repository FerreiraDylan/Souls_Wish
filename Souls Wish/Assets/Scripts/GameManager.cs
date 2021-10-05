using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 SpawnPosition;
    public GameObject Player;
    public GameObject Player_Prefab;
    public Transform Cam;

    public GameObject Pause_UI;

    public bool InMenu;

    private bool IsPaused = false;

    private GameObject ActualCampFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!InMenu)
        {
            if (PlayerManager.instance.CurrentHealth <= 0)
            {
                PlayerSpawn();
            }
        }

        if (Input.GetButtonDown("Pause") || Input.GetKey(KeyCode.P))
        {
            PauseGame();
        }
    }
    public void PlayerSpawn()
    {
        ActualCampFire = PlayerManager.instance.ActualCampFire;
        SpawnPosition = ActualCampFire.transform.Find("Interact").GetComponent<CampFire_spawn>().Spawn_Point.position;
        Destroy(Player);
        Player = Instantiate(Player_Prefab);
        Player.name = "Player";
        PlayerManager.instance.cam = Cam;
        PlayerManager.instance.ActualCampFire = ActualCampFire;
        Player.transform.position = SpawnPosition;
    }

    public void PauseGame()
    {
        if (!InMenu) {
            if (IsPaused)
            {
                Time.timeScale = 1;
                Pause_UI.SetActive(false);
                IsPaused = false;
            } else
            {
                Time.timeScale = 0;
                Pause_UI.SetActive(true);
                IsPaused = true;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
