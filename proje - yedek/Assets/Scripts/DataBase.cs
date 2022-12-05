using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "DataBase", menuName = "StarterKit/Data Base")]

public class DataBase : ScriptableObject
{
    public bool isFirstTime = true;
    public int Score;

    [Serializable]
    public class PlayerData
    {
        [Header("Ability")]
        public bool isDoubleJumpActive;
        public bool isDashActive; 
        public List<int> AbilityCosts = new List<int>();
        public int currentAbilityIndex;

        [Header("Health")]
        public List<int> HealthCount = new List<int>();
        public List<int> HealthCosts = new List<int>();
        public int currentHealthIndex;

        [Header("Damage")]
        public List<int> DamageList = new List<int>();
        public List<int> DamageCosts = new List<int>();
        public int currentDamageIndex;
    }
    public PlayerData playerData;

}
