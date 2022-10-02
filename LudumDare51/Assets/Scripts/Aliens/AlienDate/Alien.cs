using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] Material _alienMaterial;
    private AlienTorso _body;
    private AlienHead _head;

    public bool IsMoving { get; private set; }

    Vector3 m_startPos;

    public enum eMoveType
    {
        Bob,
        Wobble
    }
    eMoveType m_moveType;

    void Awake()
    {
        m_startPos = transform.position;
    }

    public void Setup(OngoingAlienData ongoingData)
    {
        AlienVisuals visualData = ongoingData.Data.Visuals;
        // set material and colour
        _alienMaterial.SetTexture("_PatternTex", visualData.Colouring.Pattern);
        _alienMaterial.SetColor("_ColorPat", visualData.Colouring.PatternColour);
        _alienMaterial.SetColor("_Color", visualData.Colouring.SkinColour);

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

        m_moveType = (eMoveType)Random.Range(0, System.Enum.GetValues(typeof(eMoveType)).Length);
        
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
    }

    public void Enter()
    {
        StartCoroutine(Move(m_startPos - (Vector3.right * Screen.width * 0.75f), m_startPos));
    }

    public void Exit()
    {
        StartCoroutine(Move(m_startPos, m_startPos + (Vector3.right * Screen.width * 0.75f)));
    }

    IEnumerator Move(Vector3 from, Vector3 to)
    {
        IsMoving = true;

        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime;
            Vector3 pos = Vector3.Lerp(from, to, t);
            Quaternion rot = transform.rotation;
        
            switch(m_moveType)
            {
                case eMoveType.Bob:
                    {
                        float curve = Mathf.Abs(Mathf.Sin(t * Mathf.PI * 4));
                        pos.y = pos.y + curve * 0.05f * (to.x - from.x);
                        transform.localScale = new Vector3(Mathf.Lerp(1.3f, 1f, curve), Mathf.Lerp(0.8f, 1.3f, curve), 1);
                    }
                    break;

                case eMoveType.Wobble:
                    rot = Quaternion.Euler(0, 0, Mathf.Sin(t * Mathf.PI * 4) * 20);
                    break;
            }

            transform.position = pos;
            transform.rotation = rot;
            yield return null;
        }

        transform.position = to;
        transform.rotation = Quaternion.identity;

        IsMoving = false;

        t = 0;

        switch(m_moveType)
        {
            case eMoveType.Bob:
                while(t < 1f)
                {
                    t += Time.deltaTime;
                    float curve = Mathf.Abs(Mathf.Sin(t * Mathf.PI * 2));
                    transform.localScale = Vector3.Lerp(new Vector3(Mathf.Lerp(1.3f, 1f, curve), Mathf.Lerp(0.8f, 1.2f, curve), 1), Vector3.one, t);
                    yield return null;
                }
                transform.localScale = Vector3.one;
                break;

            
            case eMoveType.Wobble:
                while(t < 1f)
                {
                    t += Time.deltaTime;
                    transform.rotation = Quaternion.Euler(0, 0, (1 - t) * Mathf.Sin(t * Mathf.PI * 2) * 20);
                    yield return null;
                }
                transform.rotation = Quaternion.identity;
                break;
        }
    }

    public void MinigameEvent(eMinigameEvent minigameEvent)
    {
        Debug.Log(minigameEvent);
    }
}
