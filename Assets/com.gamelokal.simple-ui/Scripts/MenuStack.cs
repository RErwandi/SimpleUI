using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameLokal.SimpleUI
{
    [Serializable]
    public class MenuStack
    {
        public MenuStackElement[] stacks;

        public Transform GetParent(MenuPlacement placement) => stacks[(int)placement].parent;
        private List<Menu> GetStack(MenuPlacement placement) => stacks[(int)placement].stack;

        public int Count { get { return stacks.Sum(element => element.stack.Count); } }

        public void Construct()
        {
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i].stack = new List<Menu>();
            }
        }

        public Menu Peek()
        {
            for (int i = stacks.Length - 1; i >= 0; i--)
            {
                var stack = GetStack((MenuPlacement) i);

                if (stack.Count > 0)
                {
                    return stack.Last();
                }
            }

            return null;
        }

        public Menu Pop()
        {
            for (int i = stacks.Length - 1; i >= 0; i--)
            {
                var stack = GetStack((MenuPlacement) i);

                if (stack.Count <= 0) continue;
                
                var last = stack.Last();
                stack.Remove(last);
                return last;
            }

            return null;
        }

        public void Push(Menu menu)
        {
            GetStack(menu.placement).Add(menu);
        }
    }
}