using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace CasualLevelsDemo
{
    [ExecuteAlways]
    public class CartoonWater : MonoBehaviour
    {
        private static readonly int DirectionHash = Shader.PropertyToID("_Direction");
        private static readonly int TempHash = Shader.PropertyToID("_Temp");

        private static List<WaterAffector> Affectors = new List<WaterAffector>();
        private static List<Renderer> Renderers = new List<Renderer>();
        private CommandBuffer commandBuffer;

        [SerializeField] private int resolution = 1024;

        private Material maskMaterial;
        private Material blurMaterial;

        private RenderTexture target;

        private void Start()
        {
            commandBuffer = new CommandBuffer();
            commandBuffer.name = "Simple water";
            RenderMask();
        }

        private void OnEnable()
        {
            commandBuffer = new CommandBuffer();
            target = new RenderTexture(resolution, resolution, 0, RenderTextureFormat.ARGB32);
        }

        private void OnDisable()
        {
            commandBuffer.Dispose();
            DestroyImmediate(maskMaterial);
            DestroyImmediate(blurMaterial);
            DestroyImmediate(target);
        }

        private void OnValidate()
        {
            DestroyImmediate(target);
            target = new RenderTexture(resolution, resolution, 0, RenderTextureFormat.ARGB32);
        }

#if UNITY_EDITOR
        private void LateUpdate()
        {
            if (Application.isPlaying)
                return;

            RenderMask();
        }
#endif

        private void RenderMask()
        {
            if (maskMaterial == null)
                maskMaterial = new Material(Resources.Load<Shader>("Simple water/WaterMask"));

            if (blurMaterial == null)
                blurMaterial = new Material(Resources.Load<Shader>("Simple water/WaterBlur"));

            WaterAffector.FetchAffectors(Affectors);
            commandBuffer.Clear();

            var renderPosition = transform.position + Vector3.up * 100.0f;
            var viewMatrix = Matrix4x4.TRS(renderPosition, Quaternion.Euler(90, 0, 0), Vector3.one);
            var scaleMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, 1, -1));

            var scale = transform.localScale * 0.5f;
            var projection = Matrix4x4.Ortho(-scale.x, scale.x, -scale.y, scale.y, 0.01f, 100.0f);
            commandBuffer.SetRenderTarget(target);
            commandBuffer.ClearRenderTarget(true, true, Color.black);
            commandBuffer.SetViewProjectionMatrices(scaleMatrix * viewMatrix.inverse, projection);

            foreach (var waterAffector in Affectors)
            {
                waterAffector.GetComponents(Renderers);
                foreach (var renderer in Renderers)
                    commandBuffer.DrawRenderer(renderer, maskMaterial);
            }

            commandBuffer.GetTemporaryRT(TempHash, target.width, target.height, 0, FilterMode.Bilinear,
                                         RenderTextureFormat.ARGB32);

            for (var index = 0; index < 10; index++)
            {
                commandBuffer.SetGlobalVector(DirectionHash, new Vector4(1, 0, 0, 0));
                commandBuffer.Blit(target, TempHash, blurMaterial);

                commandBuffer.SetGlobalVector(DirectionHash, new Vector4(0, 1, 0, 0));
                commandBuffer.Blit(TempHash, target, blurMaterial);
            }

            commandBuffer.ReleaseTemporaryRT(TempHash);

            Graphics.ExecuteCommandBuffer(commandBuffer);

            GetComponent<MeshRenderer>().sharedMaterial.mainTexture = target;
        }
    }
}