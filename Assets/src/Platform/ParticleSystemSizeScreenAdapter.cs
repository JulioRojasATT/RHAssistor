using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemSizeScreenAdapter : ScreenSizeAdapter {
    private ParticleSystem _particleSystem;

    ParticleSystem.MainModule psMainModule;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        psMainModule = _particleSystem.main;
    }

    public override void AutoAdapt()
    {
        transform.localScale = Vector3.one * AdaptUsingHeight(1);
        /*ParticleSystem.MinMaxCurve newCurve = new ParticleSystem.MinMaxCurve();
        psMainModule.startSize = newCurve;*/
        /*ParticleSystem.MinMaxCurve curve = psMainModule.startSize;
        if (/*curve==null && curve.curve == null && curve.curve.keys == null)
        {
            return;
        }
        for (int i = 0; i < curve.curve.keys.Length; i++)
        {
            curve.curve.keys[i].value = 0;
            curve.curve.keys[i].time = 255;
        }
        psMainModule.startSize = curve;*/
    }
}