using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_CircularMenu : MonoBehaviour
{
    public GameObject Menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Tab))
        {
            Menu.SetActive(true);
        }
        else
        {
            Menu.SetActive(false);
        }
    }
}
