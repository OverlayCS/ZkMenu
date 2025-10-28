using BepInEx;
using GorillaLocomotion;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem;
using UnityEngine;

namespace ZkMenu.src.Mods
{
    public static class FlyController
    {
        private static float startX = -1f;
        private static float startY = -1f;
        private static float refMouseX;
        private static float refMouseY;

        private static Vector3 lastPosition = Vector3.zero;

        private const float BaseSpeed = 10f;
        private const float LookSensitivity = 1.33f;
        private const float MaxPitch = 90f;

        public static void WASDFly()
        {
            var rb = GorillaTagger.Instance.rigidbody;
            Transform parent = GTPlayer.Instance.rightControllerTransform.parent;
            Quaternion headRot = GorillaTagger.Instance.headCollider.transform.rotation;

            bool w = UnityInput.Current.GetKey(KeyCode.W);
            bool a = UnityInput.Current.GetKey(KeyCode.A);
            bool s = UnityInput.Current.GetKey(KeyCode.S);
            bool d = UnityInput.Current.GetKey(KeyCode.D);
            bool space = UnityInput.Current.GetKey(KeyCode.Space);
            bool ctrl = UnityInput.Current.GetKey(KeyCode.LeftControl);
            bool shift = UnityInput.Current.GetKey(KeyCode.LeftShift);
            bool alt = UnityInput.Current.GetKey(KeyCode.LeftAlt);

            Vector3 inputDir = Vector3.zero;

            if (w) inputDir += parent.forward;
            if (s) inputDir -= parent.forward;
            if (a) inputDir -= parent.right;
            if (d) inputDir += parent.right;
            if (space) inputDir += Vector3.up;
            if (ctrl) inputDir -= Vector3.up;

            // Stop physics drift
            if (inputDir != Vector3.zero)
                rb.linearVelocity = Vector3.zero;

            // Handle mouse look when right-click held
            if (Mouse.current.rightButton.isPressed)
            {
                HandleMouseLook(parent);
            }
            else
            {
                startX = -1f;
                startY = -1f;
            }

            float speed = BaseSpeed;
            if (shift) speed *= 2f;
            else if (alt) speed *= 0.5f;

            if (inputDir != Vector3.zero)
            {
                rb.transform.position += inputDir.normalized * (speed * Time.deltaTime);
                lastPosition = rb.transform.position;
            }
            else if (lastPosition != Vector3.zero)
            {
                rb.transform.position = lastPosition;
            }

            // Sync head rotation to VR rig
            VRRig.LocalRig.head.rigTarget.transform.rotation = headRot;
        }

        private static void HandleMouseLook(Transform parent)
        {
            Quaternion currentRot = parent.rotation;
            Vector3 euler = currentRot.eulerAngles;
            Vector2 mousePos = Mouse.current.position.ReadValue();

            if (startX < 0f)
            {
                startX = euler.y;
                refMouseX = mousePos.x / Screen.width;
            }
            if (startY < 0f)
            {
                startY = euler.x;
                refMouseY = mousePos.y / Screen.height;
            }

            float deltaX = (mousePos.x / Screen.width - refMouseX) * 360f * LookSensitivity;
            float deltaY = (mousePos.y / Screen.height - refMouseY) * 360f * LookSensitivity;

            float newPitch = Mathf.Clamp(startY - deltaY, -MaxPitch, MaxPitch);
            float newYaw = startX + deltaX;

            parent.rotation = Quaternion.Euler(newPitch, newYaw, euler.z);
        }
    }
}
