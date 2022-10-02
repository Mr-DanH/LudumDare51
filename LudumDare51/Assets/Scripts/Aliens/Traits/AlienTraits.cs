using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlienTraitData", menuName = "Alien Traits Data", order = 52)]
public class AlienTraits : ScriptableObject
{
    [SerializeField] List<TraitSetupData> _candleTraits;
    [SerializeField] List<TraitSetupData> _flowerTraits;
    [SerializeField] List<TraitSetupData> _flyTraits;
    [SerializeField] List<TraitSetupData> _strengthTraits;
    [SerializeField] List<TraitSetupData> _drinkTraits;
    [SerializeField] List<TraitSetupData> _dialogueTraits;

    public List<Trait> GenerateTraits(int num)
    {
        List<Trait> generatedTraits = new List<Trait>();

        generatedTraits.Add(GenerateTraitFromList(_candleTraits));
        generatedTraits.Add(GenerateTraitFromList(_flowerTraits));
        generatedTraits.Add(GenerateTraitFromList(_flyTraits));
        generatedTraits.Add(GenerateTraitFromList(_strengthTraits));
        generatedTraits.Add(GenerateTraitFromList(_drinkTraits));
        generatedTraits.Add(GenerateTraitFromList(_dialogueTraits));

        generatedTraits.Shuffle<Trait>();

        generatedTraits.RemoveRange(num-1, generatedTraits.Count-num);

        return generatedTraits;
    }

    private Trait GenerateTraitFromList(List<TraitSetupData> traits)
    {
        TraitSetupData setup = traits.RandomElement<TraitSetupData>();
        return new Trait(setup);
    }

    [System.Serializable]
    public class Trait
    {
        public string Description { get; private set; }
        public eTraitPolarity Polarity { get; private set;}
        public List<eMinigameEvent> OmittedEvents { get; private set; }
        public List<eMinigameEvent> IncludedEvents { get; private set; }
        public bool IsCompleted { get; private set; }

        public Trait(TraitSetupData data)
        {
            Description = data.Description;
            IncludedEvents = data.IncludeMinigameEvents;
            OmittedEvents = data.OmitMinigameEvents;
            Polarity = data.Polarity;
        }

        public void Complete()
        {
            IsCompleted = true;
        }

        public void SetPolarity(AlienTraits.eTraitPolarity polarity)
        {
            Polarity = polarity;
        }
    }

    [System.Serializable]
    public class TraitSetupData
    {
        [SerializeField] public string Description;
        [SerializeField] public eTraitPolarity Polarity; 
        [SerializeField] public List<eMinigameEvent> IncludeMinigameEvents;
        [SerializeField] public List<eMinigameEvent> OmitMinigameEvents;
    }

    public enum eTraitPolarity
    {
        LIKE = 1,
        DISLIKE = 2
    }
}
