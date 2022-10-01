using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablePropMinigame : Minigame
{
    public override eType Type { get { return eType.TableProp; } }

    public List<GameObject> m_objects;

    class ObjectData
    {
        public bool m_active;
        public Vector3 m_pos;
    }

    Dictionary<GameObject, ObjectData> m_objectData = new Dictionary<GameObject, ObjectData>();

    void Awake()
    {
        foreach(var obj in m_objects)
        {
            ObjectData data = new ObjectData();

            data.m_pos = obj.transform.position;
            data.m_active = obj.activeSelf;

            m_objectData[obj] = data;
        }
    }


    public override void ResetMinigame()
    {
        foreach(var obj in m_objects)
        {
            obj.transform.position = m_objectData[obj].m_pos;
            obj.SetActive(m_objectData[obj].m_active);
            obj.SendMessage("ResetProp");
        }
    }
    
    public override void AlienArrived(Game.Alien alien)
    {
        base.AlienArrived(alien);

    }

    void Update()
    {

    }
}