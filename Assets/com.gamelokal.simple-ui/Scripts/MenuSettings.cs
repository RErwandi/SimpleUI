using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLokal.SimpleUI
{
    [CreateAssetMenu(menuName = "Joyseed/Menu Settings", fileName = "MenuSettings", order = 1)]
    public class MenuSettings : ScriptableObject
    {
        public List<Menu> availableMenu = new List<Menu>();

        public T GetPrefab<T>() where T : Menu
        {
            foreach (var menu in availableMenu)
            {
                if (menu.GetType() == typeof(T))
                {
                    return (T)menu;
                }
            }
            
            Debug.LogWarning("Prefab not found for type " + typeof(T));
            return null;
        }
    }
}