using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioData
{
    private static string COMMA = ", ";

    public string Name { get; private set; }
    public string BodyType { get; private set; }
    public string Features { get; private set; }
    public string Likes { get; private set; }
    public string Dislikes { get; private set; }

    public BioData(AlienData alienData)
    {
        Name = alienData.Name;
        SetVisualData(alienData.Visuals);
        SetTraitData(alienData.Traits);
    }

    private void SetVisualData(AlienVisuals visualData)
    {
        BodyType = visualData.Body.Description + COMMA;
        BodyType += visualData.Head.Description + COMMA;
        BodyType += visualData.Mouths.Description + COMMA;
        BodyType += visualData.Eyes.Description + COMMA;
        BodyType += visualData.Arms.Description;

        Features = visualData.HeadAccessory.Description + COMMA;
        Features += visualData.Colouring.Description + COMMA;
        Features += visualData.Colouring.PaletteDescription;
    }

    private void SetTraitData(List<AlienTraits.Trait> traitData)
    {
        Likes = "";
        Dislikes = "";
        foreach (var trait in traitData)
        {
            if (trait.IsPositive)
            {
                Likes += $"\n {trait.Description}";
            }
            else
            {
                Dislikes += $"\n {trait.Description}";
            }
        }
    }
}
