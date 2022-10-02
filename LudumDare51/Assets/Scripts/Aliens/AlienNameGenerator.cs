using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienNameGenerator : MonoBehaviour
{
    readonly string[] PREFIXES = {"Pl", "Gr", "Wr", "Kl", "X", "Sp", "B"}; 
    readonly string[] MIDDLE = {"ar", "un", "el", "ol", "is", "am"}; 
    readonly string[] POSTFIXES = {"ss", "kle", "tish", "pas", "dor", "boo"};

    readonly string[] COMEDY_NAMES = {"Dave", "George", "John", "Ethel", "Agnes", "Margaret"};

    List<List<int>> m_excludedSecondIndices = new List<List<int>>();
    List<List<int>> m_excludedThirdIndices = new List<List<int>>();

    int m_comedyAlienIndex;

    public void ResetNames(int numAliens)
    {
        m_comedyAlienIndex = Random.Range(0, numAliens);

        m_excludedSecondIndices.Clear();
        m_excludedThirdIndices.Clear();
        for (int i = 0; i < PREFIXES.Length; ++i)
            m_excludedSecondIndices.Add(new List<int>());
        for (int i = 0; i < MIDDLE.Length; ++i)
            m_excludedThirdIndices.Add(new List<int>());
    }

    string RandomArrayElement(string[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    int GetIndex(int listLen, List<int> exclusionList)
    {
        int randomIndex = Random.Range(0, listLen - exclusionList.Count);
        foreach(int usedIndex in exclusionList)
        {
            if(randomIndex >= usedIndex)
                ++randomIndex;
        }
        exclusionList.Add(randomIndex);
        if(exclusionList.Count == listLen)
            exclusionList.Clear();

        return randomIndex;
    }

    public string GetName(int index)
    {
        if(index == m_comedyAlienIndex)
            return RandomArrayElement(COMEDY_NAMES);

        int firstIndex = Random.Range(0, PREFIXES.Length);

        int secondIndex = GetIndex(MIDDLE.Length, m_excludedSecondIndices[firstIndex]);

        int thirdIndex = GetIndex(POSTFIXES.Length, m_excludedThirdIndices[secondIndex]);

        return PREFIXES[firstIndex] + MIDDLE[secondIndex] + POSTFIXES[thirdIndex];
    }
}
