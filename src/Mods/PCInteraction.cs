using GorillaNetworking;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine.InputSystem;
using UnityEngine;
using static ZkMenu.src.GlobalMethods;

namespace ZkMenu.src.Mods
{
    public static class PCInteraction
    {
        private static float keyboardDelay = 0f;
        private const float KeyboardCooldown = 0.1f;
        private const float RayDistance = 512f;
        public static Camera ThirdPersonCamera;

        public static void PCButtonClick()
        {
            if (!Mouse.current.leftButton.isPressed)
                return;

            Ray ray = ThirdPersonCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, RayDistance, NoInvisLayerMask()))
                return;

            if (Time.time <= keyboardDelay)
                return;

            Component[] components = hit.collider.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                Component component = components[i];
                Type compType = component.GetType();
                string compName = compType.Name;

                if (IsPressableButton(compType, compName))
                {
                    InvokeTriggerEnter(component);
                }
                else if (compName == "GorillaKeyboardButton")
                {
                    HandleKeyboardButton(component);
                }
            }
        }

        private static bool IsPressableButton(Type compType, string compName)
        {
            // Compatible with both subclassed and name-based button checks
            if (typeof(GorillaPressableButton).IsAssignableFrom(compType))
                return true;

            return compName == "GorillaPressableButton" ||
                   compName == "GorillaPlayerLineButton" ||
                   compName == "CustomKeyboardKey";
        }

        private static void InvokeTriggerEnter(Component component)
        {
            MethodInfo triggerMethod = component.GetType()
                .GetMethod("OnTriggerEnter", BindingFlags.NonPublic | BindingFlags.Instance);

            if (triggerMethod == null)
                return;

            Collider rightHand = GetRightHandCollider();
            if (rightHand != null)
                triggerMethod.Invoke(component, new object[] { rightHand });
        }

        private static void HandleKeyboardButton(Component component)
        {
            keyboardDelay = Time.time + KeyboardCooldown;

            GorillaKeyboardBindings binding = Traverse.Create(component)
                .Field("Binding").GetValue<GorillaKeyboardBindings>();

            if (GameEvents.OnGorrillaKeyboardButtonPressedEvent != null)
                GameEvents.OnGorrillaKeyboardButtonPressedEvent.Invoke(binding);
        }

        private static Collider GetRightHandCollider()
        {
            GameObject obj = FindGameObject("Player Objects/Player VR Controller/GorillaPlayer/TurnParent/RightHandTriggerCollider");
            if (obj == null)
                return null;

            return obj.GetComponent<Collider>();
        }
    }
}
