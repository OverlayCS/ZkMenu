using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using ZkMenu.src.Mods;

namespace ZkMenu.src
{
    public static class UI
    {
        private static bool showMenu = true; 
        private static Vector2 scrollPos;    
        private static Rect windowRect = new Rect(10, 10, 300, 400);
        public static void StartUI()
        {
            if (showMenu)
                windowRect = GUI.Window(0, windowRect, DrawMenuWindow, "Zk Mod Menu");
        }
        static void DrawMenuWindow(int windowID)
        {
            // Make the window draggable
            GUI.DragWindow(new Rect(0, 0, 300, 20));

            // Add a scroll view in case you add lots of buttons later
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUILayout.Width(290), GUILayout.Height(360));

            GUILayout.Space(10);

            if(GUILayout.Button("PC Click Toggle"))
            {
                ActiveChecker.IsPCClickActive = !ActiveChecker.IsPCClickActive;
            }

            if (GUILayout.Button("Fly Toggle"))
            {
                ActiveChecker.IsFlyActive = !ActiveChecker.IsFlyActive;
            }

            GUILayout.EndScrollView();
        }
    }
}
