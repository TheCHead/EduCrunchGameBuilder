using System;
using System.Collections.Generic;
using System.Linq;


public static class Utilities
{
    public static T[] ShuffleArray<T>(T[] array)
    {
        T[] rndArray = array.OrderBy(x => Guid.NewGuid()).ToArray();
        return rndArray;
    }
    
    public static List<T> ShuffleList<T>(List<T> list)
    {
        List<T> rndList = list.OrderBy(x => Guid.NewGuid()).ToList();
        return rndList;
    }
}
