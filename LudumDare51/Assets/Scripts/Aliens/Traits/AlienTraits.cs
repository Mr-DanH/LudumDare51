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
        return new Trait(setup.Description, setup.MinigameEvents, setup.IsPositive);
    }

    [System.Serializable]
    public class Trait
    {
        private string _description;
        private List<eMinigameEvent> _events;
        private bool _isPositive;

        public string Description { get { return _description; } }
        public List<eMinigameEvent> Events { get { return _events; } }
        public bool IsPositive { get { return _isPositive; } }
        public bool IsCompleted { get; private set; }

        public Trait(string description, List<eMinigameEvent> events, bool isPositive)
        {
            _description = description;
            _events = events;
            _isPositive = isPositive;
        }

        public void Complete()
        {
            IsCompleted = true;
        }
    }

    [System.Serializable]
    public class TraitSetupData
    {
        [SerializeField] public string Description;
        [SerializeField] public bool IsPositive;
        [SerializeField] public List<eMinigameEvent> MinigameEvents;
    }
}
