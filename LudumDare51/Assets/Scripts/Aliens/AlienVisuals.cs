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
        public Texture Element;
        public List<int> PositionIndex;
    }

    [System.Serializable]
    public class HeadData : VisualElement
    {
        public GameObject Head;       
    }

    [System.Serializable]
    public class BodyData : VisualElement
    {
        public GameObject Body;
    }

    [System.Serializable]
    public class ColouringData : VisualElement
    {
        public Texture Pattern;
        public List<Color> Colours;
    }

    #endregion
