using System;
using System.Collections.Generic;
using UnityEngine;

namespace CasualLevelsDemo
{
    [ExecuteAlways]
    public class WaterAffector : MonoBehaviour
    {
        private static List<WaterAffector> Affectors = new List<WaterAffector>();

        private void OnEnable()
        {
            Affectors.Add(this);
        }

        private void OnDisable()
        {
            Affectors.Remove(this);
        }

        public static void FetchAffectors(List<WaterAffector> target, Predicate<WaterAffector> predicate = null)
        {
            target.Clear();
            foreach (var waterAffector in Affectors)
                if (predicate == null || predicate(waterAffector))
                    target.Add(waterAffector);
        }
    }
}