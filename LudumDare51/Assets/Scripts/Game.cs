using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{   
    public enum eProgress
    {
        Available,
        Active,
        Success,
        Fail
    }

    public class Alien
    {
        public eProgress m_progress;

    }    

    public GameObject m_alien;

    public List<Minigame> m_minigames = new List<Minigame>();

    const int NUM_ALIENS = 10;

    List<Alien> m_aliens = new List<Alien>();

    IEnumerator Start()
    {
        //Generate aliens
        for (int i = 0; i < NUM_ALIENS; ++i)
        {
            Alien alien = new Alien();
            m_aliens.Add(alien);
        }
        
        m_alien.gameObject.SetActive(false);

        for (int i = 0; i < NUM_ALIENS; ++i)
        {
            //Reset table
            foreach(var minigame in m_minigames)
                minigame.ResetMinigame();
                
            yield return new WaitForSeconds(2);

            Debug.Log($"Alien {i} arrive");
            m_alien.gameObject.SetActive(true);
            
            foreach(var minigame in m_minigames)
                minigame.AlienArrived(m_aliens[i]);

            yield return new WaitForSeconds(10);

            Debug.Log($"Alien {i} leave");
            
            //Alien leaves (fail if still active)
            m_alien.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(2);
        }

        //Game end
    }

    public void NextAlien()
    {

    }

    void SuccessDate()
    {

    }

    void FailDate()
    {

    }
}
