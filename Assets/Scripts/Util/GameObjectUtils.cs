using UnityEngine;
using UnityEditor;

public static class GameObjectUtils
{
    public static GameObject FindInHierachyByName(GameObject inGameObject, string withName)
    {
        Component[] components = inGameObject.GetComponentsInChildren<Component>();

        foreach (Component component in components)
        {
            if (component.gameObject.name == withName)
            {
                return component.gameObject;
            }
        }

        throw new MissingComponentException("No component with name " + withName + " exists in the gameobject hierarchy");
    }
}