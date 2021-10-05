using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularMenuSettings : MonoBehaviour
{
    //public GameObject Prefab;
    public List<GameObject> CircularTabs = new List<GameObject>();
    public Text Description;

    private Vector2 normalisedMousePosition;
    private float currentAngle;
    private int selection;
    private int previousSelection;

    private MenuItemScript menuItemSc;
    private MenuItemScript previousMenuItemSc;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        normalisedMousePosition = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        currentAngle = Mathf.Atan2(normalisedMousePosition.y, normalisedMousePosition.x) * Mathf.Rad2Deg;
        currentAngle = (currentAngle + 360) % 360;

        selection = (int) currentAngle / (360 / CircularTabs.Count);

        if (selection != previousSelection) {
            previousMenuItemSc = CircularTabs[previousSelection].GetComponent<MenuItemScript>();
            previousMenuItemSc.Deselect();
            previousSelection = selection;

            menuItemSc = CircularTabs[selection].GetComponent<MenuItemScript>();
            Description.text = menuItemSc.transform.Find("Information").Find("Text").GetComponent<Text>().text;
            menuItemSc.Select();
        }
        if (Input.GetMouseButtonDown(0))
        {
            menuItemSc = CircularTabs[selection].GetComponent<MenuItemScript>();
            menuItemSc.OpenWindow();

        }
    }
}
