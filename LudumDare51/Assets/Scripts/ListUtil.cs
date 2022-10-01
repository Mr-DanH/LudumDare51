using System.Collections.Generic;
using UnityEngine;

public static class ListUtil
{
    public static T RandomElement<T>(this IList<T> list)
    {
        if (list.Count == 0) 
            throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");

        return list[Random.Range(0, list.Count)];
    }

    public static T RandomElement<T>(this List<T> list, System.Predicate<T> match)
    {
        if (list.Count == 0) 
            throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");

        List<T> subList = list.FindAll(match);
        if (subList.Count == 0)
            return default(T);

        return list[Random.Range(0, list.Count)];
    }
}