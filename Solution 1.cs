using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Solution1 : MonoBehaviour
{
    public string characterName;
    public string characterClass;
    public int characterLevel;
    public int characterConstitution;
    public bool isHillDwarf;
    public bool hasToughFeat;
    public bool averageHP;

    private int characterHP;

    private List<string> characterClasses = new List<string>
    {
        "Artificer", "Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk",
        "Ranger", "Rogue", "Paladin", "Sorcerer", "Wizard", "Warlock"
    };

    public Dictionary<string, int> hitDice = new Dictionary<string, int>
    {
        {"Artificer", 8 }, {"Barbarian", 12 }, {"Bard", 8}, {"Cleric", 8}, {"Druid", 8 },
        {"Fighter", 10}, {"Monk", 8}, {"Ranger", 10}, {"Rogue", 8}, {"Paladin", 10},
        {"Sorcerer", 6}, {"Wizard", 6}, {"Warlock", 8}
    };

    private int[] abilityModifiers = new int[]
    {
        -5, -4, -4, -3, -3, -2, -2, -1, -1, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5,
        6, 6, 7, 7, 8, 8, 9, 9, 10
    };

    private void ApplyDwarfBonus()
    {
        if (isHillDwarf)
        {
            characterHP += characterLevel;
        }
    }

    private void ApplyToughFeat()
    {
        if (hasToughFeat)
        {
            characterHP += characterLevel * 2;
        }
    }

    private void CalculateHP()
    {
        int hitDie = hitDice[characterClass];

        if (!averageHP) 
        {
            for (int i = 0; i < characterLevel; i++)
            {
                characterHP += UnityEngine.Random.Range(1, hitDie + 1); 
            }
        }
        else 
        {
            characterHP += (hitDie / 2 + 1) * characterLevel; 
        }
    }

    private void Update()
    {
        int conModifier = abilityModifiers[characterConstitution - 1];
        characterHP = conModifier * characterLevel;

        ApplyDwarfBonus();
        ApplyToughFeat();
        CalculateHP();

        Debug.Log($"My character {characterName} is a level {characterLevel} {characterClass} " +
                  $"with a CON score of {characterConstitution} and {(isHillDwarf ? "is" : "is not")}" +
                  $"a Hill Dwarf and {(hasToughFeat ? "has" : "does not have")} the Tough feat. " +
                  $"I want the HP {(averageHP ? "average" : "rolled")}. Your HP is {characterHP}.");
    }
}
}
