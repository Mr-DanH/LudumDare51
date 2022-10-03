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
    [SerializeField] CanvasGroup _happy;
    [SerializeField] CanvasGroup _sad;

    [SerializeField] AnimationCurve _emotionCurve;
    [SerializeField] float _emotionDuration;

    private float _transparent = 0;
    private float _opaque = 1;

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
        
        _happy.alpha = _transparent;
        _sad.alpha = _transparent;
    }

    public IEnumerator ShowEmotion(eEventEmotion emotion)
    {
        CanvasGroup emotionImage = emotion == eEventEmotion.Happy ? _happy : _sad;

        float t = 0;
        while(t < _emotionDuration)
        {
            t += Time.deltaTime;
            float fade = Mathf.Lerp(_transparent, _opaque, _emotionCurve.Evaluate(t/_emotionDuration));
            emotionImage.alpha = fade;
            yield return null;
        }

        yield return null;
    }
}
