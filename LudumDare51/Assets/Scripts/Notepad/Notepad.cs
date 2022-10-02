using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notepad : MonoBehaviour
{
    [SerializeField] private Bio _bio;
    [SerializeField] private Toggle _tabTemplate;
    [SerializeField] private ToggleGroup _tabParent;
    [SerializeField] private Animator _openAnimator;


    private List<Toggle> _tabs = new List<Toggle>();

    private List<BioData> _bios = new List<BioData>();

    private bool _isOpen;
    
    private void Start()
    {
        AlienManager.Instance.onGeneratedAliens += ResetBios;
        _isOpen = false;
        _openAnimator.SetBool("IsOpen", false);
    }

    public void UiToggleOpen()
    {
        Debug.Log("Toggle Open!");
        _isOpen = !_isOpen;
        _openAnimator.SetBool("IsOpen", _isOpen);

    }

    public void UiSelectBio(int index, bool selected)
    {
        Debug.Log($"Selected a tab {index}, selected: {selected}");
        if (index < _bios.Count)
        {
            BioData bioData = _bios[index];
            _bio.Setup(bioData);
        }
    }

    private void ResetBios()
    {
        _bios.Clear();
        List<AlienData> aliens = AlienManager.Instance.GetAlienData();
        aliens.ForEach(GenerateBio);

        CleanupTabs();
        GenerateTabs(aliens.Count);

    }

    private void CleanupTabs()
    {
        _tabs.ForEach(x=> Destroy(x));
        _tabs.Clear();
    }

    private void GenerateBio(AlienData alien)
    {
        BioData bio = new BioData(alien);
        _bios.Add(bio);
    }

    private void GenerateTabs(int numAliens)
    {
        for (int i = 0; i < numAliens; i++)
        {
            Toggle tabClone = Instantiate(_tabTemplate, _tabParent.transform);
            tabClone.group = _tabParent;
            int index = i;
            tabClone.onValueChanged.AddListener( x=> UiSelectBio(index, x));
            _tabs.Add(tabClone);
        }
    }
}
