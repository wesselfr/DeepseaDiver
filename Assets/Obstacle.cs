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

    [SerializeField]
    private Renderer m_Renderer;

    public void DisableObject()
    {
        SpawnParticle();
        m_Root.SetActive(false);
        ResetMaterial();
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

    public void ResetMaterial()
    {
        foreach (Material mat in m_Renderer.materials)
        {
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            Color newColor = mat.color;
            newColor.a = 1f;
            mat.SetColor("_Color", newColor);
        }

    }

    public void Fade()
    {
        StartCoroutine(FadeModel(0.1f));
    }

    public IEnumerator FadeModel(float speed)
    {
        foreach (Material mat in m_Renderer.materials)
        {
            mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        }

        float alpha = 1f;
        while (alpha > 0)
        {
            alpha -= speed * Time.deltaTime;
            foreach(Material mat in m_Renderer.materials)
            {
                Color newColor = mat.color;
                newColor.a = Mathf.Clamp(alpha, 0.0f, 1.0f);
                mat.SetColor("_Color", newColor);
            }
            yield return new WaitForSeconds(0.01f);
        }
        ResetMaterial();
    }

}
