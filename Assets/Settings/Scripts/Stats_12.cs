using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class Stats
    {
        [SerializeField] private int baseValue;
        //public int BaseValue => baseValue;

        //private int modifier = 0;

        public int GetValue()
        {
            return baseValue ;
        }

        public void Buff(int amount)
        {
            baseValue += amount;
        }

        /*public void Debuff(int amount)
        {
            baseValue -= amount;
            if (baseValue <= 0) 
            {
                baseValue=0;
            }
        }*/
        
        
        /*public float BaseValue;
        private List<StatModifier> modifiers = new List<StatModifier>();

        public float CurrentValue
        {
            get
            {
                float finalValue = BaseValue;
                foreach (StatModifier modifier in modifiers)
                {
                    finalValue += modifier.Value; // Simple additive example
                }
                return finalValue;
            }
        }

        public void AddModifier(StatModifier modifier)
        {
            modifiers.Add(modifier);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            modifiers.Remove(modifier);
        }*/
    }

   /* [System.Serializable]
    public class StatModifier
    {
        public float Value;
        // You could add types (e.g., Flat, Percentage) and other properties here
    }*/