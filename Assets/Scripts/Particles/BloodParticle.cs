using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BloodParticle : MonoBehaviour, IPoolable
{
    private ParticleSystem _particle;
    private BloodParticlePool _pool;

    public void BackToPool()
    {
        _pool.BackObjectToPool(this);
    }
    
    void Awake()
    {
        _pool = (BloodParticlePool)BloodParticlePool.Instance;
        
        _particle = GetComponent<ParticleSystem>();
    }

    private void OnDisable()
    {
        _particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        BackToPool();
    }

    public void Play()
    {
        _particle.Play(true);
    }
}
