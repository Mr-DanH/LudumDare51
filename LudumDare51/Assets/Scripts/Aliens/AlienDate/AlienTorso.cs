using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienTorso : AlienBodyPart
{
    [SerializeField] Transform _headTransform;
    [SerializeField] List<Image> _arms;

    public Transform HeadTransform { get { return _headTransform; } }

    public override void Setup(AlienVisuals data) 
    {
        List<int> positionIndices = data.Arms.PositionIndex;
        Sprite arm = data.Arms.Element;
        SetPositionalImages(positionIndices, _arms, arm);
    }
}
