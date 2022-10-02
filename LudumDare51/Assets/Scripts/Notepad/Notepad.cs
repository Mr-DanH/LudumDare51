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
    [SerializeField] private GameObject _notepadBlocker;


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
        _isOpen = !_isOpen;
        _openAnimator.SetBool("IsOpen", _isOpen);
        _notepadBlocker.SetActive(_isOpen);
    }

    public void UiSelectBio(int index, bool selected)
    {
        if (index < _bios.Count)
        {
            BioData bioData = _bios[index];
            _bio.Setup(bioData);
        }
    }

    private void ResetBios()
    {
        CleanupTabs();
        _bios.Clear();
        List<AlienData> aliens = AlienManager.Instance.GetAlienData();
        aliens.ForEach(GenerateBio);
    }

    private void CleanupTabs()
    {
        _tabs.ForEach(x=> Destroy(x.gameObject));
        _tabs.Clear();
    }

    private void GenerateBio(AlienData alien)
    {
        BioData bio = new BioData(alien);
        _bios.Add(bio);
        GenerateTab(_bios.Count-1, alien.Visuals.Colouring.SkinColour);
    }

    private void GenerateTab(int index, Color mainColour)
    {
        Toggle tabClone = Instantiate(_tabTemplate, _tabParent.transform);
        tabClone.group = _tabParent;
        tabClone.onValueChanged.AddListener( x=> UiSelectBio(index, x));
        ColorBlock colours = tabClone.colors;
        colours.normalColor = mainColour;
        tabClone.colors = colours;
        _tabs.Add(tabClone);
    }
}
