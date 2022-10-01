using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{   
    [SerializeField] private AlienManager _alienManager;
    [SerializeField] private Alien _alienObject;
    private OngoingAlienData _currentAlien;

    public List<Minigame> m_minigames = new List<Minigame>();

    IEnumerator Start()
    {
        _alienObject.gameObject.SetActive(false);

        for (int i = 0; i < _alienManager.NumAliens; ++i)
        {
            //Reset table
            foreach(var minigame in m_minigames)
                minigame.ResetMinigame();
                
            yield return new WaitForSeconds(2);

            Debug.Log($"Alien {i} arrive");
            _currentAlien = _alienManager.GetNextAlien(round:1);
            _alienObject.Setup(_currentAlien);
            _alienObject.gameObject.SetActive(true);
            _alienObject.Enter();
            
            while(_alienObject.IsMoving)
                yield return null;
            
            foreach(var minigame in m_minigames)
                minigame.AlienArrived(_alienObject);

            yield return new WaitForSeconds(10);

            Debug.Log($"Alien {i} leave");
            
            _alienObject.Exit();

            while(_alienObject.IsMoving)
                yield return null;

            //Alien leaves (fail if still active)
            _alienObject.gameObject.SetActive(false);
            
            foreach(var minigame in m_minigames)
                minigame.AlienLeave();
            
            yield return new WaitForSeconds(2);
        }

        //Game end
    }

    void SuccessDate()
    {

    }

    void FailDate()
    {

    }
}
