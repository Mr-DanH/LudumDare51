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
        Wobble,
        Drop,
        ShortArse
    }
    eMoveType m_moveType;
    static int g_lastMoveType;

    const float DISTANCE_OFFSCREEN = 900;
    const float HEIGHT_OFFSCREEN = 800;

    void Awake()
    {
        m_startPos = transform.localPosition;
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
        _head.Setup(ongoingData.Data.Name, visualData);

        int moveTypeIndex = Random.Range(0, System.Enum.GetValues(typeof(eMoveType)).Length - 1);
        if(moveTypeIndex >= g_lastMoveType)
            ++moveTypeIndex;
        g_lastMoveType = moveTypeIndex;

        m_moveType = (eMoveType)moveTypeIndex;
        
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
    }

    public void TriggerEmotionalResponse(eEventEmotion emotion)
    {
        if (emotion != eEventEmotion.Neutral)
        {
            StartCoroutine(_head.ShowEmotion(emotion));
        }
    }

    public void Enter()
    {
        //m_moveType = eMoveType.ShortArse;

        if(m_moveType == eMoveType.Drop)
            StartCoroutine(Move(m_startPos + (Vector3.up * HEIGHT_OFFSCREEN), m_startPos));
        else
            StartCoroutine(Move(m_startPos - (Vector3.right * DISTANCE_OFFSCREEN), m_startPos));
    }

    public void Exit()
    {
        if(m_moveType == eMoveType.Drop)
            StartCoroutine(Move(m_startPos, m_startPos + (Vector3.up * HEIGHT_OFFSCREEN)));
        else
            StartCoroutine(Move(m_startPos, m_startPos + (Vector3.right * DISTANCE_OFFSCREEN)));
    }

    IEnumerator Move(Vector3 from, Vector3 to)
    {
        IsMoving = true;

        float t = 0;
        while(t < 1)
        {
            if(m_moveType == eMoveType.ShortArse)
                t += Time.deltaTime * 0.5f;
            else
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

                case eMoveType.Drop:
                    {
                        float cappedT = Mathf.InverseLerp(0.3f, 0.7f, t);
                        pos = Vector3.Lerp(from, to, cappedT);
                        float curve = (Mathf.Sin(t * Mathf.PI * 3f) + 1) * 0.5f;
                        transform.localScale = new Vector3(Mathf.Lerp(0.7f, 1.3f, curve), Mathf.Lerp(1.2f, 0.8f, curve), 1);
                    }
                    break;
                    
                case eMoveType.ShortArse:
                    {
                        float invT = t;
                        Vector3 invFrom = from;
                        Vector3 invTo = to;
                        if(from == m_startPos)
                        {
                            invT = 1 - t;
                            invFrom = to;
                            invTo = from;
                        }

                        float cappedT = Mathf.InverseLerp(0.0f, 0.8f, invT);
                        pos = Vector3.Lerp(invFrom, invTo, cappedT);
                        
                        float curve = Mathf.Abs(Mathf.Sin(cappedT * Mathf.PI * 6));
                        pos.y = pos.y + curve * 0.03f * (to.x - from.x);

                        float heightT = Mathf.InverseLerp(0.8f, 1, invT);
                        pos += Vector3.down * 200 * Mathf.Cos(heightT * 1.5f * Mathf.PI) * (1 - heightT);
                    }
                    break;
            }

            transform.localPosition = pos;
            transform.rotation = rot;
            yield return null;
        }

        transform.localPosition = to;
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
                    Vector3 scale = new Vector3(Mathf.Lerp(1.3f, 1f, curve), Mathf.Lerp(0.8f, 1.2f, curve), 1);
                    transform.localScale = Vector3.Lerp(scale, Vector3.one, t);
                    yield return null;
                }
                transform.localScale = Vector3.one;
                break;

            case eMoveType.Drop:
                while(t < 1f)
                {
                    t += Time.deltaTime;                    
                    float curve = (Mathf.Sin((t * Mathf.PI * 3f) + Mathf.PI) + 1) * 0.5f;
                    Vector3 scale = new Vector3(Mathf.Lerp(0.7f, 1.3f, curve), Mathf.Lerp(1.2f, 0.8f, curve), 1);
                    transform.localScale = Vector3.Lerp(scale, Vector3.one, t);
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
}
