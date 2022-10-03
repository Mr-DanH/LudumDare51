using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienTorso : AlienBodyPart
{
    [SerializeField] Transform _headTransform;
    [SerializeField] List<Image> _arms;

    public Transform HeadTransform { get { return _headTransform; } }

    public void Setup(AlienVisuals data) 
    {
        List<int> positionIndices = data.Arms.PositionIndex;
        Sprite arm = data.Arms.Element;
        SetPositionalImages(positionIndices, _arms, arm);
    }

    public Vector3 GetLeftMostHand()
    {
        Vector3 bestHand = Vector3.right * 1000;

        foreach(var arm in _arms)
        {
            if (!arm.gameObject.activeInHierarchy)
                continue;

            Vector3 hand = arm.transform.position + (arm.transform.up * 80);

            if(hand.x < bestHand.x)
                bestHand = hand;
        }

        return bestHand;
    }
}
