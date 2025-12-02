using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class FindExtension
{
    public static T FindChild<T>(this Transform transform, string name) where T : MonoBehaviour
    {
        T[] children = transform.GetComponentsInChildren<T>();
        foreach(var child in children)
        {
            if(child.name == name)
                return child;
        }

        return null;
    }

    public static T FindParent<T>(this Transform transform, string name) where T : MonoBehaviour
    {
        T[] parents = transform.GetComponentsInParent<T>();
        foreach(var parent in parents)
        {
            if(parent.name == name)
                return parent;
        }

        return null;
    }
}
