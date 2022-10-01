using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienVisuals
{
    public HeadData Head;
    public PositionalVisualElement HeadAccessory;
    public BodyData Body;
    public PositionalVisualElement Eyes;
    public PositionalVisualElement Arms;
    public ColouringData Colouring;
}

    #region Data Classes
    public class VisualElement
    {
        public string Description;
    }
    [System.Serializable]
    public class PositionalVisualElement : VisualElement
    {
        public Sprite Element;
        public List<int> PositionIndex;
    }

    [System.Serializable]
    public class HeadData : VisualElement
    {
        public AlienHead Head;       
    }

    [System.Serializable]
    public class BodyData : VisualElement
    {
        public AlienTorso Body;
    }

    [System.Serializable]
    public class ColouringData : VisualElement
    {
        public Texture Pattern;
        public List<Color> Colours;
    }

    #endregion
