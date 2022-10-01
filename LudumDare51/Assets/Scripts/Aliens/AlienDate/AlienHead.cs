using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienHead : AlienBodyPart
{
    [SerializeField] List<Image> _eyes;
    [SerializeField] List<Image> _accessories;

    public override void Setup(AlienVisuals data)
    {
        List<int> eyePositionIndices = data.Eyes.PositionIndex;
        Sprite eye = data.Eyes.Element;
        SetPositionalImages(eyePositionIndices, _eyes, eye);

        List<int> accessoryPositionIndices = data.HeadAccessory.PositionIndex;
        Sprite accessory = data.HeadAccessory.Element;
        SetPositionalImages(accessoryPositionIndices, _accessories, accessory);
    }
}
