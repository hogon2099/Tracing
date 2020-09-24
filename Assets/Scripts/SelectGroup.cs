using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectGroup : MonoBehaviour {

    public Color[] colorsCollection = { new Color(1, 0, 0) };
    [SerializeField] public Color ownColor;
    [SerializeField] public int counter;  // по счетчику выбираем цвет
    [SerializeField] public bool isSelected = false;
    public int maxGroups;
    GameObject[] groupButtons;
    Color orange = new Color(255, 128, 0);

    public void Switch()   // удаляем метки "выбрано" на всех остальных кнопках(находим по тегам), переключаем на нажатой
    {

        Image[] thisImgChildren = GetComponentsInChildren<Image>();

        if (!isSelected)
        {
            groupButtons = GameObject.FindGameObjectsWithTag("GroupButton");
            foreach (var button in groupButtons)
            {
                button.GetComponent<SelectGroup>().isSelected = false;
                Image[] otherImgChildren = button.GetComponentsInChildren<Image>();
                otherImgChildren[otherImgChildren.Length-1].color = Color.white;
            }
            isSelected = true;

            thisImgChildren[thisImgChildren.Length - 1].color = colorsCollection[counter];
        }
        else if (isSelected)
        {
            isSelected = false;
            thisImgChildren[thisImgChildren.Length - 1].color = Color.white;

        }
    }

    public void Start()
    {
   
        ownColor = colorsCollection[counter]; // получаем свой цвет по номеру
        Image[] children = gameObject.GetComponentsInChildren<Image>();
        children[1].color = ownColor;


    }
   
}
