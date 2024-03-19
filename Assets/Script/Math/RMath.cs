using System.Collections.Generic;
using UnityEngine;

public static class RMath 
{
    public static List<Transform> AddAllChildren(Transform parrent)
    {
        List<Transform> children = new List<Transform>();
      
            foreach (Transform child in parrent)
            {
                children.Add(child);

                if (child.childCount > 0)
                {
                children.AddRange(AddAllChildren(child)); 
                }
            }
        return children;
    }
}
