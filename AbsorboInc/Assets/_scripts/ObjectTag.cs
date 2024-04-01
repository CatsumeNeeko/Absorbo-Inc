using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTag : MonoBehaviour
{
    public string tagToAssign = "Ground";
    private void Start()
    {
        AssignTagRecursively(transform);
    }
    void AssignTagRecursively(Transform parent)
    {
        parent.gameObject.tag = tagToAssign;

        foreach (Transform child in parent)
        {
            // Recursively call the function for each child
            AssignTagRecursively(child);
        }
    }
}
