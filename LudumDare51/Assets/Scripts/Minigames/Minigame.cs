using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    public enum eType
    {
        Unknown,

        Speech,
        TableProp,
        Drink,
        Handshake,
        Fly,
        Strength
    }

    public virtual eType Type { get; }


    public List<GameObject> m_objects;

    class ObjectData
    {
        public bool m_active;
        public Vector3 m_pos;
        public Vector3 m_scale;
        public Quaternion m_rot;
    }

    Dictionary<GameObject, ObjectData> m_objectData = new Dictionary<GameObject, ObjectData>();
    
    protected Game.Alien m_alien;

    public virtual void Awake()
    {
        foreach(var obj in m_objects)
        {
            ObjectData data = new ObjectData();

            data.m_pos = obj.transform.position;
            data.m_scale = obj.transform.localScale;
            data.m_rot = obj.transform.rotation;
            data.m_active = obj.activeSelf;

            m_objectData[obj] = data;
        }
    }

    public virtual void ResetMinigame()
    {
        foreach(var obj in m_objects)
        {
            obj.transform.position = m_objectData[obj].m_pos;
            obj.transform.localScale = m_objectData[obj].m_scale;
            obj.transform.rotation = m_objectData[obj].m_rot;
            obj.SetActive(m_objectData[obj].m_active);
            obj.SendMessage("ResetProp", SendMessageOptions.DontRequireReceiver);
        }
    }

    public virtual void AlienArrived(Game.Alien alien)
    {
        m_alien = alien;
    }

    public virtual void AlienLeave()
    {

    }
}
