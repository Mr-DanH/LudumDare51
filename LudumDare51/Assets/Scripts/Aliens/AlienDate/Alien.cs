using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] Material _alienMaterial;
    private AlienTorso _body;
    private AlienHead _head;

    public void Setup(OngoingAlienData ongoingData)
    {
        AlienVisuals visualData = ongoingData.Data.Visuals;
        // set material and colour
        _alienMaterial.mainTexture = visualData.Colouring.Pattern;
        _alienMaterial.color = visualData.Colouring.Colours.RandomElement<Color>();

        if (_body != null)
        {
            Destroy(_body.gameObject);
        }
        _body = Instantiate<AlienTorso>(visualData.Body.Body, transform);
        _body.Setup(visualData);

        if (_head != null)
        {
            Destroy(_head.gameObject);
        }
        _head = Instantiate<AlienHead>(visualData.Head.Head, _body.HeadTransform);
        _head.Setup(visualData);
    }

    public void MinigameEvent(eMinigameEvent minigameEvent)
    {
        Debug.Log(minigameEvent);
    }
}
