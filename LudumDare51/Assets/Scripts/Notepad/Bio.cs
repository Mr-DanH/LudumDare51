using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bio : MonoBehaviour
{
    [SerializeField] private Text _nameField;

    [Header("Visuals")]
    [SerializeField] private Text _bodyTypeField;
    [SerializeField] private Text _distinguishingFeaturesField;

    [Header("Traits")]
    [SerializeField] private Text _likesField;
    [SerializeField] private Text _dislikesField;

    public void Setup(BioData alienData)
    {
        _nameField.text = alienData.Name;

        _bodyTypeField.text = alienData.BodyType;
        _distinguishingFeaturesField.text = alienData.Features;

        _likesField.text = alienData.Likes;
        _dislikesField.text = alienData.Dislikes;
    }
}
