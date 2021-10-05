using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemScript : MonoBehaviour
{
    [Header("Outer Ring")]
    public Color outer_baseColor;
    public Color outer_hoverColor;
    public Image OuterBase;

    [Header("Inner Ring")]
    public Color inner_baseColor;
    public Color inner_hoverColor;
    public Image InnerBase;

    [Header("Inner Ring")]
    public GameObject Information;
    public bool DisplayIcon;
    public bool DisplayText;

    [Header("Display Window")]
    public GameObject Prefab;

    // Start is called before the first frame update
    void Start()
    {
        OuterBase.color = outer_baseColor;
        InnerBase.color = inner_baseColor;
        Information.transform.Find("Image").gameObject.SetActive(DisplayIcon);
        Information.transform.Find("Text").gameObject.SetActive(DisplayText);

        var value = OuterBase.GetComponent<Image>().fillAmount;
        Information.transform.Rotate(0, 0, ((360 * value) / 2) * -1);
        
    }

    public void Select()
    {
        OuterBase.color = outer_hoverColor;
        InnerBase.color = inner_hoverColor;
    }
    public void Deselect()
    {
        OuterBase.color = outer_baseColor;
        InnerBase.color = inner_baseColor;
    }

    public void OpenWindow()
    {
        var clone = Instantiate(Prefab);
        // a supprimer
        clone.SetActive(true);
        // 
        clone.transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
