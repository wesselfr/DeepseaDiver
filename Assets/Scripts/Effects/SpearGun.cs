using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearGun : MonoBehaviour {

    [SerializeField]
    private int m_BulletsLeft = 3;

    [SerializeField]
    private Transform m_SpearPosition;

    [SerializeField]
    private GameObject m_DisplaySpear;

    [SerializeField]
    private GameObject m_SpearPrefab;

    [SerializeField]
    private GameObject m_SpearIcon1, m_SpearIcon2, m_SpearIcon3;

    private bool m_CanShoot;

    public void Start()
    {
        GameManager.OnGameStart += ResetSpears;
    }
    public void ResetSpears()
    {
        m_BulletsLeft = 3;
        m_CanShoot = true;
        UpdateSpearIcon();
    }

    public void Shoot()
    {
        if (m_BulletsLeft > 0 && m_CanShoot)
        {
            m_BulletsLeft--;
            Spear bullet = Instantiate(m_SpearPrefab, m_SpearPosition.position, Quaternion.identity).GetComponent<Spear>();
            bullet.Initialize();
            StartCoroutine(ShootEffect());
            UpdateSpearIcon();
        }
    }

    void UpdateSpearIcon()
    {
        switch (m_BulletsLeft)
        {
            case 3:
                m_SpearIcon1.SetActive(true);
                m_SpearIcon2.SetActive(true);
                m_SpearIcon3.SetActive(true);
                break;
            case 2:
                m_SpearIcon1.SetActive(true);
                m_SpearIcon2.SetActive(true);
                m_SpearIcon3.SetActive(false);
                break;
            case 1:
                m_SpearIcon1.SetActive(true);
                m_SpearIcon2.SetActive(false);
                m_SpearIcon3.SetActive(false);
                break;
            case 0:
                m_SpearIcon1.SetActive(false);
                m_SpearIcon2.SetActive(false);
                m_SpearIcon3.SetActive(false);
                break;
        }
    }

    public IEnumerator ShootEffect()
    {
        m_DisplaySpear.SetActive(false);
        m_CanShoot = false;
        float time = 0.2f;
        while(time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }
        m_DisplaySpear.SetActive(true);
        m_CanShoot = true;
    }
}
