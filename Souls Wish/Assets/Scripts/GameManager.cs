using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player Spawn")]
    public Vector3 SpawnPosition;
    public GameObject Player;
    public GameObject Player_Prefab;
    public Transform Cam;

    [Header("UI")]
    public GameObject Pause_UI;
    public GameObject Defeate_UI;
    public GameObject Victory_UI;

    [Header("Player Stats")]
    public float Heal = 30f;
    public float Speed = 6f;
    public float Damage = 35f;

    [Header("Bonuses")]
    public float B_Heal = 0f;
    public float B_Speed = 0f;
    public float B_Damage = 0f;

    public int bonus_allowed;
    public int selected_bonus;


    public bool InMenu;
    public string scene;

    private bool SpawnActive = false;
    private bool nextlvlActive = false;
    private bool IsPaused = false;

    private GameObject ActualCampFire;

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        selected_bonus = bonus_allowed;
        if (!InMenu)
        {
            Time.timeScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!InMenu)
        {
            if (PlayerManager.instance.CurrentHealth <= 0)
            {
                StartCoroutine(DeathCDDuration(5f));
                if (SpawnActive) {
                    PlayerSpawn();
                    SpawnActive = false;
                }
            }
        }

        if (Input.GetButtonDown("Pause") || Input.GetKey(KeyCode.P))
        {
            PauseGame();
        }
    }

    public IEnumerator DeathCDDuration(float time)
    {
        Defeate_UI.SetActive(true);
        PlayerManager.instance.Player_Death();
        yield return new WaitForSeconds(time);
        SpawnActive = true;
    }
    public void PlayerVictory()
    {
        StartCoroutine(VictoryCDDuration(5f));
        if (nextlvlActive)
        {
            Nextlevel();
            nextlvlActive = false;
        }
    }
    public IEnumerator VictoryCDDuration(float time)
    {
        Victory_UI.SetActive(true);
        nextlvlActive = true;
        yield return new WaitForSeconds(time);
    }

    public void Nextlevel()
    {
        SceneManager.LoadScene(scene);
    }



    public void PlayerSpawn()
    {
        Defeate_UI.SetActive(false);
        ActualCampFire = PlayerManager.instance.ActualCampFire;
        SpawnPosition = ActualCampFire.transform.Find("Interact").GetComponent<CampFire_spawn>().Spawn_Point.position;
        Destroy(Player);
        Player = Instantiate(Player_Prefab);
        var tmp = Player.GetComponent<PlayerManager>();
        tmp.Heal = (GameManager.instance.B_Heal == 0 ? GameManager.instance.Heal : GameManager.instance.B_Heal);
        tmp.CurrentSpeed = (GameManager.instance.B_Speed == 0 ? GameManager.instance.Speed : GameManager.instance.B_Speed);
        tmp.Speed = (GameManager.instance.B_Speed == 0 ? GameManager.instance.Speed : GameManager.instance.B_Speed);
        tmp.Damage = (GameManager.instance.B_Damage == 0 ? GameManager.instance.Damage : GameManager.instance.B_Damage);
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
