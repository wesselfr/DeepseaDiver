using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField]
    private GameObject m_Root, m_ParticleHolder;

    [SerializeField]
    private GameObject m_ParticleEffectToSpawn;

    [SerializeField]
    private bool m_DisableOnPlayerTouch;

    public void DisableObject()
    {
        SpawnParticle();
        m_Root.SetActive(false);
    }

    public void PlayerDeath()
    {
        SpawnParticle();
        if (m_DisableOnPlayerTouch)
        {
            m_Root.SetActive(false);
        }
    }

    private void SpawnParticle()
    {
        if (m_ParticleEffectToSpawn != null)
        {
            Instantiate(m_ParticleEffectToSpawn, m_ParticleHolder.transform);
        }
    }

}
