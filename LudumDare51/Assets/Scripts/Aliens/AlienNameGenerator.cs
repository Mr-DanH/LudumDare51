using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienNameGenerator : MonoBehaviour
{
    readonly string[] PREFIXES = {"Pl", "Gr", "Wr", "Kl", "X", "Sp", "B"}; 
    readonly string[] MIDDLE = {"ar", "un", "el", "ol", "is", "am"}; 
    readonly string[] POSTFIXES = {"ss", "kle", "tish", "pas", "dor", "boo"};

    readonly string[] COMEDY_NAMES = {"Dave", "George", "John", "Ethel", "Agnes", "Margaret"};

    int m_comedyAlienIndex;

    public void ResetNames(int numAliens)
    {
        m_comedyAlienIndex = Random.Range(0, numAliens);        
    }

    string RandomArrayElement(string[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public string GetName(int index)
    {
        if(index == m_comedyAlienIndex)
            return RandomArrayElement(COMEDY_NAMES);
        else
            return RandomArrayElement(PREFIXES) + RandomArrayElement(MIDDLE) + RandomArrayElement(POSTFIXES);
    }
}
