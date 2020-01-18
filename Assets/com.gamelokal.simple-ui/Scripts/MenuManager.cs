using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameLokal.SimpleUI
{
    public class MenuManager : MonoBehaviour
    {
        #region Singleton

        private static MenuManager instance;
        public static MenuManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (MenuManager) FindObjectOfType(typeof(MenuManager));

                    if (FindObjectsOfType(typeof(MenuManager)).Length > 1)
                    {
                        Debug.LogWarning($"Multiple instances of {instance.gameObject.name} detected in scene.");
                        return instance;
                    }

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<MenuManager>();
                        singleton.name = $"{typeof(MenuManager)} (Singleton)";
                        
                        DontDestroyOnLoad(singleton);
                        Debug.LogWarning($"[Singleton] Creating an instance of {typeof(MenuManager)} with DontDestroyOnLoad");
                    }
                }
                
                return instance;
            }
        }
        #endregion

        public MenuSettings settings;
        public MenuStack menuStack;
        public bool isBackButtonEnabled = true;

        public UnityEvent OnStackEmpty;

        protected void Awake()
        {
            if (instance == null)
            {
                //If I am the first instance, make me the Singleton
                instance = this;
            }
            else
            {
                //If a Singleton already exists and you find
                //another reference in scene, destroy it!
                if(this != instance)
                {
                    Destroy(gameObject);
                }
            }
            menuStack.Construct();
        }
        
        public void CreateInstance<T>() where T : Menu
        {
            var prefab = settings.GetPrefab<T>();
            if (prefab)
            {
                Instantiate(prefab, GetParent(prefab));
            }
        }

        private Transform GetParent(Menu menu)
        {
            return menuStack.GetParent(menu.placement);
        }

        public void OpenMenu(Menu instance)
        {
            menuStack.Push(instance);
            instance.transform.SetAsLastSibling();
            instance.gameObject.SetActive(true);
        }

        public void CloseMenu(Menu menu)
        {
            if (menuStack.Count == 0 || menuStack.Peek()!= menu)
            {
                return;
            }
            
            CloseTopMenu();

            if (menuStack.Count == 0)
            {
                OnStackEmpty?.Invoke();
            }
        }

        private void CloseTopMenu()
        {
            var instance = menuStack.Pop();
            instance.gameObject.SetActive(false);
        }

        public void CloseAllMenus()
        {
            while (menuStack.Count > 0)
            {
                CloseTopMenu();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SendBackButtonMessage();
            }
        }

        public void SendBackButtonMessage()
        {
            if (menuStack.Count > 0 && isBackButtonEnabled)
            {
                menuStack.Peek().OnBackPressed();
            }
        }
    }
}