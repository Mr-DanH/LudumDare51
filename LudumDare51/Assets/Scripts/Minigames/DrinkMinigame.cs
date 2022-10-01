using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMinigame : Minigame
{
    public override eType Type { get { return eType.Drink; } }

    public Drink m_actualDrink;

    // void Awake()
    // {
    // }


    public override void ResetMinigame()
    {
        base.ResetMinigame();
    }
    
    public override void AlienArrived(Alien alien)
    {
        base.AlienArrived(alien);

    }

    void Update()
    {

    }

    public void OrderDrink(Drink drink)
    {
        m_actualDrink.gameObject.SetActive(true);

        m_actualDrink.GetComponent<MinigameProp>().DropOnscreen();
    }
}