using BepInEx;
using GorillaExtensions;
using GorillaLocomotion;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using ZkMenu.src.Mods;
using ZkMenu.src.Patching;
using static ZkMenu.src.GlobalMethods;

namespace ZkMenu.src
{
    public class Main
    {
        public static void start()
        {
            //called by loader
            if (PCInteraction.ThirdPersonCamera == null)
            {
                try
                {
                    PCInteraction.ThirdPersonCamera = FindGameObject("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
                }
                catch
                {
                    PCInteraction.ThirdPersonCamera = FindGameObject("Shoulder Camera").GetComponent<Camera>();
                }
        }


        public static void enabled()
        {
            //called by loader
            Patcher.Patch();
        }

        public static void disabled()
        {
            //called by loader
            Patcher.Unpatch();
        }
        public static void awake()
        {
            //called by loader
        }

        public static void update()
        {
            //called by loader
            if (ActiveChecker.IsPCClickActive)
            {
                PCInteraction.PCButtonClick();
            }
            if (ActiveChecker.IsFlyActive)
            {
                FlyController.WASDFly();
            }
        }

        public static void gui()
        {
            UI.StartUI();
        }
    }
}
