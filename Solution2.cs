using System.Collections.Generic;
using UnityEngine;

public class Solution2 : MonoBehaviour
{
    public string characterName;
    public int characterLevel;
    public int characterConstitution;
    public bool isHillDwarf;
    public bool hasToughFeat;
    public bool averageHP;

    public CharacterClass characterClass;
    private Character character;

    private void Start()
    {
        character = new Character(characterName, characterClass, characterLevel, characterConstitution, isHillDwarf, hasToughFeat, averageHP);
        character.CalculateHP();

        character.UpdateHP();
        Debug.Log(character.ToString());
    }

    private void Update()
    {

    }
}

[System.Serializable]
public struct CharacterClass
{
    public string name;
    public int hitDie;

    public CharacterClass(string name, int hitDie)
    {
        this.name = name;
        this.hitDie = hitDie;
    }
}

public class Character
{
    public string characterName;
    public CharacterClass characterClass;
    public int characterLevel;
    public int characterConstitution;
    public bool isHillDwarf;
    public bool hasToughFeat;
    public bool averageHP;
    

    public int characterHP;

    private int[] abilityModifiers = new int[]
    {
        -5, -4, -4, -3, -3, -2, -2, -1, -1, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5,
        6, 6, 7, 7, 8, 8, 9, 9, 10
    };

    public Character(string characterName, CharacterClass characterClass, int characterLevel, int characterConstitution, bool isHillDwarf, bool hasToughFeat, bool averageHP)
    {
        this.characterName = characterName;
        this.characterClass = characterClass;
        this.characterLevel = characterLevel;
        this.characterConstitution = characterConstitution;
        this.isHillDwarf = isHillDwarf;
        this.hasToughFeat = hasToughFeat;
        this.averageHP = averageHP;
        this.characterHP = 0;
    }

    public void CalculateHP()
    {
        int conModifier = abilityModifiers[characterConstitution - 1];
        characterHP = conModifier * characterLevel;

        ApplyDwarfBonus();
        ApplyToughFeat();
        CalculateHitPointsBasedOnLevel();
    }

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

    private void CalculateHitPointsBasedOnLevel()
    {
        int hitDie = characterClass.hitDie;

        if (!averageHP)
        {
            for (int i = 0; i < characterLevel; i++)
            {
                characterHP += Random.Range(1, hitDie + 1);
            }
        }
        else
        {
            characterHP += ((hitDie / 2) + 1) * characterLevel;
        }
    }

    public void UpdateHP()
    {
        CalculateHP();
    }

    public override string ToString()
    {
        return $"My character {characterName} is a level {characterLevel} {characterClass.name} " +
               $"with a CON score of {characterConstitution} and {(isHillDwarf ? "is" : "is not")} " +
               $"a Hill Dwarf and {(hasToughFeat ? "has" : "does not have")} the Tough feat. " +
               $"I want the HP {(averageHP ? "average" : "rolled")}. Your HP is {characterHP}.";
    }
}
