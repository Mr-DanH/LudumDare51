using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlienVisualData", menuName = "Alien Visual Data", order = 51)]
public class AlienVisualsData : ScriptableObject
{
    [SerializeField] private List<HeadData> Heads;
    [SerializeField] private List<TextureSetupData> HeadAccessories;
    [SerializeField] private List<PositionsSetupData> HeadAccessoryPositions;
    [SerializeField] private List<BodyData> Bodies;
    [SerializeField] private List<TextureSetupData> Arms;
    [SerializeField] private List<PositionsSetupData> ArmPositions;
    [SerializeField] private List<TextureSetupData> Eyes;
    [SerializeField] private List<PositionsSetupData> EyePositions;
    [SerializeField] private List<PatternSetupData> Patterns;
    [SerializeField] private List<ColourPaletteSetupData> Colourings;
    [SerializeField] private List<MouthData> Mouths;

    List<ColourPaletteSetupData> _generatedColourings = new List<ColourPaletteSetupData>();

    public List<AlienVisuals> GenerateAlienVisuals(int num)
    {
        GenerateAllColouringData();
        List<AlienVisuals> generatedAliens = new List<AlienVisuals>();

        for (int i = 0; i < num; i++)
        {
            AlienVisuals newData = GenerateAlienVisual(i);
            generatedAliens.Add(newData);
        }

        return generatedAliens;
    }

    private AlienVisuals GenerateAlienVisual(int index)
    {
        AlienVisuals visuals = new AlienVisuals();

        visuals.Head = Heads.RandomElement<HeadData>();
        visuals.HeadAccessory = GeneratePositionalVisualElement(HeadAccessories, HeadAccessoryPositions);
        visuals.Body = Bodies.RandomElement<BodyData>();
        visuals.Eyes = GeneratePositionalVisualElement(Eyes, EyePositions);
        visuals.Arms = GeneratePositionalVisualElement(Arms, ArmPositions);
        visuals.Colouring = GenerateFashionData(index);
        visuals.Mouths = Mouths.RandomElement<MouthData>();

        return visuals;
    }

    private PositionalVisualElement GeneratePositionalVisualElement(List<TextureSetupData> textureSetupData, List<PositionsSetupData> positionSetupData)
    {
        TextureSetupData setupData = textureSetupData.RandomElement<TextureSetupData>();
        PositionalVisualElement newElement = new PositionalVisualElement()
        {
            Element = setupData.Visual,
            Description = setupData.Description,
            PositionIndex = positionSetupData.RandomElement<PositionsSetupData>().PositionIndex
        };
        return newElement;
    }

    private void GenerateAllColouringData()
    {
        _generatedColourings = new List<ColourPaletteSetupData>(Colourings);

        List<ColourPaletteSetupData> colourings = new List<ColourPaletteSetupData>(Colourings);
        for (int i = _generatedColourings.Count; i < AlienManager.Instance.NumAliens; i++)
        {
            ColourPaletteSetupData colour = colourings.RandomElement<ColourPaletteSetupData>();
            _generatedColourings.Add(colour);

            colourings.Remove(colour);

            if (colourings.Count == 0)
            {
                colourings = new List<ColourPaletteSetupData>(Colourings);
            }
        }
        
        _generatedColourings.Shuffle();

        if (_generatedColourings.Count > AlienManager.Instance.NumAliens)
        {
            int diff = _generatedColourings.Count - AlienManager.Instance.NumAliens;
            _generatedColourings.RemoveRange(0, diff-1);
        }
    }

    private ColouringData GenerateFashionData(int index)
    {
        PatternSetupData patternData = Patterns.RandomElement<PatternSetupData>();
        ColourPaletteSetupData colourPaletteData = _generatedColourings[index];
        ColouringData newData = new ColouringData()
        {
            Pattern = patternData.Pattern,
            Description = patternData.Description,
            PatternColour = colourPaletteData.Pattern,
            SkinColour = colourPaletteData.Skin,
            PaletteDescription = colourPaletteData.Description
        };
        return newData;
    }
}

    [System.Serializable]
    public class TextureSetupData : VisualElement
    {
        public Sprite Visual;
    }

    [System.Serializable]
    public class PositionsSetupData
    {
        public List<int> PositionIndex;
    }

    [System.Serializable]
    public class ColourPaletteSetupData : VisualElement
    {
        public Color Skin;
        public Color Pattern;
    }

    [System.Serializable]
    public class PatternSetupData : VisualElement
    {
        public Texture Pattern;
    }