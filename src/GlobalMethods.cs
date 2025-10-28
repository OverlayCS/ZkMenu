using GorillaLocomotion;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ZkMenu.src
{
    public static class GlobalMethods
    {
        private static readonly Dictionary<string, GameObject> foundObjectPool = new Dictionary<string, GameObject>();
        public static GameObject FindGameObject(string find)
        {
            if (foundObjectPool.TryGetValue(find, out GameObject go))
            {
                return go;
            }

            GameObject tgo = GameObject.Find(find); 

            if (tgo != null)
            {
                foundObjectPool.Add(find, tgo);
            }

            return tgo;
        }

        private static int? noInvisLayerMask;
        public static int NoInvisLayerMask()
        {
            noInvisLayerMask ??= ~(
                1 << LayerMask.NameToLayer("TransparentFX") |
                1 << LayerMask.NameToLayer("Ignore Raycast") |
                1 << LayerMask.NameToLayer("Zone") |
                1 << LayerMask.NameToLayer("Gorilla Trigger") |
                1 << LayerMask.NameToLayer("Gorilla Boundary") |
                1 << LayerMask.NameToLayer("GorillaCosmetics") |
                1 << LayerMask.NameToLayer("GorillaParticle"));

            return noInvisLayerMask ?? GTPlayer.Instance.locomotionEnabledLayers;
        }
    }
}
