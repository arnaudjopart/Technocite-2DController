
using UnityEngine;

namespace com.ajc.turnbase.scriptableObject
{
    [CreateAssetMenu(menuName ="Turnbase/Stats")]
    public class CharacterStatScriptableObject : ScriptableObject
    {
        public enum AttackType
        {
            BASIC,
            MAGIC,
            HEAL
        }
        public AttackType m_type;
        public int m_startHealthPoints;
        public int m_maxHealthPoints;
        public int m_damagePoints;

        public int m_magicPoints;

        public GameObject m_attackParticleSystem;
        
    }
}

