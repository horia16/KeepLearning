using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

static class Extentions
{
    public static Transform GetDeepChild(this Transform me, string name )
    {
        Transform tmp;

        foreach(Transform child in me)
        {
            if (child.name == name)
                return child;
            if ((tmp = child.GetDeepChild(name)) != null)
                return tmp;
        }
        return null;
    }

    public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }

    public static List<GameObject> GetChildrenWithTag(this Transform me,string tag)
    {
        List<GameObject> result = new List<GameObject>();

        foreach(Transform child in me)
        {
            if(child.tag == tag)
                result.Add(child.gameObject);
        }

        return result;
    }
    

}
