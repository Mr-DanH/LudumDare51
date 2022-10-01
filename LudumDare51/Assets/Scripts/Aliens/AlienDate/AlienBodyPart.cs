using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AlienBodyPart : MonoBehaviour
{
    public abstract void Setup(AlienVisuals data);

    protected void SetPositionalImages(List<int> positionIndices, List<Image> images, Sprite image)
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].sprite = image;
            images[i].gameObject.SetActive(positionIndices.Contains(i));
        }
    }
}
