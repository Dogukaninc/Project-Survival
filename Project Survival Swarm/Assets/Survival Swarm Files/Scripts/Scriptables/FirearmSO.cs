using UnityEngine;

namespace Survival_Swarm_Files.Scripts.Scriptables
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Firearm",fileName = "Firearm_Object")]
    public class FirearmSO : ScriptableObject
    {
        public int damagePower;
        public float fireRate;
        public float bulletSpeed;

    }
}