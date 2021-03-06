﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cheese : MonoBehaviour, IPointerClickHandler
{
    private ObjectSelection select;

    public bool hasCheese;
    public bool cheeseUsed;

    private void Awake()
    {
        hasCheese = true;
        select = GameObject.FindGameObjectWithTag("ObjectSelection").GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (cheeseUsed == true)
        {
            select.DeselectAll();
            Destroy(gameObject);
        }

        if (select.usingCheese == false)
            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.usingCheese = true;
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        DeselectRest();
    }

    private void DeselectRest()
    {
        select.usingPlate = false;
        select.usingPlateW1Ing = false;
        select.usingPlateWBread = false;
        select.usingPlateWFrozenBread = false;

        select.usingKnife = false;
        select.usingBread = false;
        select.usingGlass = false;
        select.usingFrozenBread = false;

        select.usingHam = false;
        select.usingMayo = false;
        select.usingLettuce = false;
        select.usingTomato = false;
        select.usingBottle = false;
        select.usingMilk = false;
        select.usingJuice = false;
        select.usingPickle = false;

        select.usingStoveCloth = false;
        select.usingGlove = false;
    }
}
