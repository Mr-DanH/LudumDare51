using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Game : SingletonMonoBehaviour<Game>
{   
    [SerializeField] private AlienManager _alienManager;
    [SerializeField] private Alien _alienObject;
    private OngoingAlienData _currentAlien;

    public List<Minigame> m_minigames = new List<Minigame>();

    public Transform m_timerSegmentParent;

    public Image m_drinkImage;

    const float ALIEN_ARRIVE_DELAY = 1;
    const int ALIEN_TIME = 10;
    const float ALIEN_LEAVE_DELAY = 1;

    public override void Awake()
    {
        base.Awake();
        SceneManager.LoadScene("Screens", LoadSceneMode.Additive);
    }

    void Start()
    {
        foreach(Transform child in m_timerSegmentParent)
            child.gameObject.SetActive(false);

        foreach(var minigame in m_minigames)
        {
            minigame.ResetMinigame();
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCo());
    }

    IEnumerator StartGameCo()
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

            foreach(Transform child in m_timerSegmentParent)
                child.gameObject.SetActive(false);
                
            yield return new WaitForSeconds(ALIEN_ARRIVE_DELAY);

            Debug.Log($"Alien {i} arrive");
            _currentAlien = _alienManager.GetNextAlien(round:1);
            _alienObject.Setup(_currentAlien);
            _alienObject.gameObject.SetActive(true);
            _alienObject.Enter();
            
            while(_alienObject.IsMoving)
                yield return null;
            
            foreach(var minigame in m_minigames)
                minigame.AlienArrived(_alienObject, _currentAlien);

            for (int j = 0; j < ALIEN_TIME; ++j)
            {
                yield return new WaitForSeconds(1);
                if (j < m_timerSegmentParent.childCount)
                    m_timerSegmentParent.GetChild(j).gameObject.SetActive(true);
            }

            Debug.Log($"Alien {i} leave");

            _alienObject.Exit(m_drinkImage.gameObject.activeInHierarchy ? m_drinkImage.sprite : null);

            foreach(var minigame in m_minigames)
            {
                minigame.AlienLeave();
                minigame.onMinigameComplete -= MinigameComplete;
            }            

            while(_alienObject.IsMoving)
                yield return null;

            //Alien leaves (fail if still active)
            _alienObject.gameObject.SetActive(false);
            

            
            yield return new WaitForSeconds(ALIEN_LEAVE_DELAY);
        }

        MatchResultsScreen.Instance.Show();
    }

    private void MinigameComplete(eMinigameEvent minigameEvent)
    {
        _alienManager.OnMinigameComplete(minigameEvent);
        eEventEmotion emotion = _currentAlien.GetEmotionAboutEvent(minigameEvent);
        _alienObject.TriggerEmotionalResponse(emotion);
    }
}
