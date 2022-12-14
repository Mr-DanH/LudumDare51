using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkMinigame : Minigame
{
    public override eType Type { get { return eType.Drink; } }

    public Drink m_actualDrink;

    public override void ResetMinigame()
    {
        base.ResetMinigame();
    }

    public override void AlienLeave()
    {
        base.AlienLeave();
        m_actualDrink.gameObject.SetActive(false);
    }


    void Update()
    {

    }

    public void OrderDrink(Drink drink)
    {
        m_actualDrink.gameObject.SetActive(true);
        
        m_actualDrink.transform.GetChild(0).GetComponent<Image>().sprite = drink.m_sprite;

        m_actualDrink.GetComponent<MinigameProp>().DropOnscreen();
    }
}