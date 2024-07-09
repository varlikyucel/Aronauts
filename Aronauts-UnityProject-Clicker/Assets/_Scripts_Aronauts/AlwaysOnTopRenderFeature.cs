using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AlwaysOnTopRenderFeature : ScriptableRendererFeature
{
    class AlwaysOnTopRenderPass : ScriptableRenderPass
    {
        private FilteringSettings filteringSettings;
        private ShaderTagId shaderTagId = new ShaderTagId("UniversalForward");
        private RenderStateBlock renderStateBlock;

        public AlwaysOnTopRenderPass(RenderQueueRange renderQueueRange, RenderPassEvent renderPassEvent)
        {
            this.renderPassEvent = renderPassEvent;
            filteringSettings = new FilteringSettings(renderQueueRange);
            renderStateBlock = new RenderStateBlock(RenderStateMask.Depth)
            {
                depthState = new DepthState(true, CompareFunction.Always)
            };
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var drawingSettings = CreateDrawingSettings(shaderTagId, ref renderingData, SortingCriteria.CommonOpaque);
            context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings, ref renderStateBlock);
        }
    }

    [SerializeField] private RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
    private AlwaysOnTopRenderPass alwaysOnTopRenderPass;

    public override void Create()
    {
        alwaysOnTopRenderPass = new AlwaysOnTopRenderPass(RenderQueueRange.opaque, renderPassEvent);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(alwaysOnTopRenderPass);
    }
}
