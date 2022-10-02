using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Game : MonoBehaviour
{   
    [SerializeField] private AlienManager _alienManager;
    [SerializeField] private Alien _alienObject;
    private OngoingAlienData _currentAlien;

    public List<Minigame> m_minigames = new List<Minigame>();

    const float ALIEN_ARRIVE_DELAY = 1;
    const float ALIEN_TIME = 10;
    const float ALIEN_LEAVE_DELAY = 1;

    void Awake()
    {
        SceneManager.LoadScene("Screens", LoadSceneMode.Additive);
    }

    IEnumerator Start()
    {
        _alienObject.gameObject.SetActive(false);

        for (int i = 0; i < _alienManager.NumAliens; ++i)
        {
            //Reset table
            foreach(var minigame in m_minigames)
            {
                minigame.ResetMinigame();
                minigame.onMinigameComplete += MinigameComplete;
            }
                
            yield return new WaitForSeconds(ALIEN_ARRIVE_DELAY);

            Debug.Log($"Alien {i} arrive");
            _currentAlien = _alienManager.GetNextAlien(round:1);
            _alienObject.Setup(_currentAlien);
            _alienObject.gameObject.SetActive(true);
            _alienObject.Enter();
            
            while(_alienObject.IsMoving)
                yield return null;
            
            foreach(var minigame in m_minigames)
                minigame.AlienArrived(_alienObject);

            yield return new WaitForSeconds(ALIEN_TIME);

            Debug.Log($"Alien {i} leave");
            
            _alienObject.Exit();

            while(_alienObject.IsMoving)
                yield return null;

            //Alien leaves (fail if still active)
            _alienObject.gameObject.SetActive(false);
            
            foreach(var minigame in m_minigames)
            {
                minigame.AlienLeave();
                minigame.onMinigameComplete -= MinigameComplete;
            }
            
            yield return new WaitForSeconds(ALIEN_LEAVE_DELAY);
        }

        ChooseMatchesScreen.Instance.Show();
    }

    private void MinigameComplete(eMinigameEvent minigameEvent)
    {
        _alienManager.OnMinigameComplete(minigameEvent);
    }
}
