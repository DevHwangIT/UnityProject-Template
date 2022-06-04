using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyLibrary.Utility
{
    public class UnityUtility
    {
        public static int GetLayerMask(string layerName)
        {
            int mask = 1 << LayerMask.NameToLayer(layerName);
            return mask;
        }

        //텍스쳐 사이즈 변경
        public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
        {
            Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
            Color32[] rpixels = result.GetPixels32(0);
            float incX = 1.0f / targetWidth;
            float incY = 1.0f / targetHeight;
            for (int px = 0; px < rpixels.Length; px++)
            {
                rpixels[px] = source.GetPixelBilinear(incX * ((float) px % targetWidth),
                    incY * ((float) Mathf.Floor(px / targetWidth)));
            }

            result.SetPixels32(rpixels, 0);
            result.Apply();
            return result;
        }

        //자신 제외 자식 오브젝트
        public static List<Transform> GetChildTransform(GameObject parent)
        {
            List<Transform> transforms = new List<Transform>();
            var child = parent.GetComponentsInChildren<Transform>();
            for (int i = 0; i < child.Length; ++i)
            {
                if (child[i].gameObject != parent)
                {
                    transforms.Add(child[i]);
                }
            }

            return transforms;
        }

        //자식중 해당 이름
        public static Transform GetFindChildTransform(Transform parent, string name)
        {
            var child = parent.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < child.Length; ++i)
            {
                if (child[i].name.Equals(name))
                {
                    return child[i];
                }
            }

            return null;
        }

        //자식중 해당 컴퍼넌트
        public static T[] GetFineChildTransform<T>(Transform parent) where T : Component
        {
            var child = parent.GetComponentsInChildren<T>(true);
            return child;
        }

        //리스트 랜덤 셔플
        public static List<T> Shuffle<T>(List<T> list)
        {
            var shuffled = list.OrderBy(a => Guid.NewGuid()).ToList();
            return shuffled;
        }

        public static void AnimationRestart(Animator anim, string aniName)
        {
            anim.Play(aniName, -1, 0f);
        }
    }
}