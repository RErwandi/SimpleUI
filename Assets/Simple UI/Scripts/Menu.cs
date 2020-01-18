
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameLokal.SimpleUI
{
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        public static T Instance { get; private set; }

        public static Action OnOpen;
        public static Action OnClosed;

        private static bool isOpened;

        protected virtual void Awake()
        {
            Instance = (T) this;
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }

        public static T Open()
        {
            if (Instance == null)
            {
                MenuManager.Instance.CreateInstance<T>();
            }

            if (!isOpened)
            {
                MenuManager.Instance.OpenMenu(Instance);
                isOpened = true;
                OnOpen?.Invoke();
            }

            return Instance;
        }

        public static void Close()
        {
            if (Instance == null)
            {
                return;
            }
            
            isOpened = false;
            Instance.CloseMenu();
        }

        public override void CloseMenu()
        {
            MenuManager.Instance.CloseMenu(this);
            OnClosed?.Invoke();

            if (destroyOnClose)
            {
                Destroy(this.gameObject);
            }
        }

        public override void OnBackPressed()
        {
            Close();
        }
    }

    public abstract class Menu : MonoBehaviour
    {
        public MenuPlacement placement;
        public bool destroyOnClose = false;

        public abstract void OnBackPressed();
        public abstract void CloseMenu();

        protected MenuManager MenuManager => MenuManager.Instance;
    }

    public enum MenuPlacement
    {
        Panel,
        Popup
    }
}