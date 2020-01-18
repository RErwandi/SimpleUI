using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLokal.SimpleUI
{
    [Serializable]
    public class MenuStackElement
    {
        public Transform parent;
        public List<Menu> stack;
    }
}