using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StrengthItem : MinigameProp, IPointerDownHandler
{
    public StrengthMinigame m_minigame;

    public List<MinigameProp> m_chunks;
    public List<MinigameProp> m_smallChunks;
    public float m_chunkSpeed = 100;

    public Image m_damageImage;
    public List<Sprite> m_damageSprites = new List<Sprite>();

    public float m_damageShakeOffset = 10;
    public float m_damageShakeTime = 0.25f;

    const int HEALTH = 10;
    int m_damage;

    Vector3 m_startPos;
    float m_offsetTime;

    void Awake()
    {
        m_startPos = transform.localPosition;
    }

    public override void ResetState()
    {
        m_damage = 0;
        m_offsetTime = 0;
        m_damageImage.enabled = false;

        if(gameObject.activeSelf)
            return;

        base.ResetState();

        DropOnscreen();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ++m_damage;
        m_offsetTime = 1;

        if(m_damage >= HEALTH)
        {
            gameObject.SetActive(false);
            m_minigame.PropEvent(eMinigameEvent.StrengthTested);

            float angleOffset = Random.Range(0, Mathf.PI * 2);
            float anglePerChunk = Mathf.PI * 2 / m_chunks.Count;

            foreach(var chunk in m_chunks)
            {
                Vector3 velocity = new Vector3(Mathf.Cos(angleOffset), Mathf.Sin(angleOffset), 0) * m_chunkSpeed;
                velocity.x *= 0.5f;

                angleOffset += anglePerChunk;

                chunk.gameObject.SetActive(true);
                chunk.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                chunk.Throw(velocity);
            }
        }
        else
        {
            int damageSpriteIndex = 1 + (m_damage * (m_damageSprites.Count - 1) / HEALTH);
            m_damageImage.enabled = true;
            m_damageImage.sprite = m_damageSprites[damageSpriteIndex];

            foreach(var chunk in m_smallChunks)
            {
                if(chunk.gameObject.activeSelf)
                    continue;
                    
                float angleOffset = Random.Range(0, Mathf.PI * 2);

                Vector3 velocity = new Vector3(Mathf.Cos(angleOffset), Mathf.Sin(angleOffset), 0) * m_chunkSpeed;
                velocity.x *= 0.5f;

                chunk.gameObject.SetActive(true);
                chunk.transform.position = eventData.position;
                chunk.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                chunk.Throw(velocity);
                break;
            }
        }
    }


    public override void Update()
    {
        base.Update();

        if(m_falling)
            return;

        m_offsetTime = Mathf.MoveTowards(m_offsetTime, 0, Time.deltaTime / m_damageShakeTime);
        float angle = 2 * Mathf.PI * Random.value;
        transform.localPosition = m_startPos + (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * m_offsetTime * m_damageShakeOffset);
    }
}
