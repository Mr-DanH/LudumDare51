using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienHead : AlienBodyPart
{
    [SerializeField] List<Image> _eyes;
    [SerializeField] List<Image> _accessories;
    [SerializeField] Image _mouth;
    [SerializeField] Text _nameTag;
    [SerializeField] Image _happy;
    [SerializeField] Image _sad;

    public void Setup(string name, AlienVisuals data)
    {
        List<int> eyePositionIndices = data.Eyes.PositionIndex;
        Sprite eye = data.Eyes.Element;
        SetPositionalImages(eyePositionIndices, _eyes, eye);

        List<int> accessoryPositionIndices = data.HeadAccessory.PositionIndex;
        Sprite accessory = data.HeadAccessory.Element;
        SetPositionalImages(accessoryPositionIndices, _accessories, accessory);

        _mouth.sprite = data.Mouths.Mouth;

        _nameTag.text = name;
    }

    public IEnumerator ShowEmotion(eEventEmotion emotion)
    {
        Image emotionImage = emotion == eEventEmotion.Happy ? _happy : _sad;
        Color startColour = emotionImage.color;
        startColour.a = 0;
        Color endColour = emotionImage.color;
        endColour.a = 1;

        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime;
            Color fade = Color.Lerp(startColour, endColour, t);
            emotionImage.color = fade;
            yield return null;
        }

        t = 0;
        while(t < 1)
        {
            t += Time.deltaTime;
            Color fade = Color.Lerp(endColour, startColour, t);
            emotionImage.color = fade;
            yield return null;
        }
        yield return null;
    }
}
