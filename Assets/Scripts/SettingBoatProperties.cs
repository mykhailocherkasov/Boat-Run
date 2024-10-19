using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RendererAndMaterialToBeSet
{
    public Renderer Renderer;
    public Material MaterialToBeSet;

    public RendererAndMaterialToBeSet(Renderer renderer, Material materialToBeSet)
    {
        Renderer = renderer;
        MaterialToBeSet = materialToBeSet;
    }

    public void SetMaterialToRenderer()
    {
        Renderer.material = MaterialToBeSet;
    }
}

public class SettingBoatProperties : MonoBehaviour
{
    [SerializeField] private float m_WoodArmorCoefficient = 1;
    [SerializeField] private float m_SteelArmorCoefficient = 0.5f;
    [SerializeField] private List<RendererAndMaterialToBeSet> m_RenderersWithWoodArmor;
    [SerializeField] private List<RendererAndMaterialToBeSet> m_RenderersWithSteelArmor;
    [SerializeField] private Player m_Player;

    [SerializeField] private GameObject m_SmallCannon;
    [SerializeField] private GameObject m_BigCannon;

    private List<GameObject> m_AllCannons;

    private void Awake()
    {
        m_AllCannons = new() { m_SmallCannon, m_BigCannon };
        SetWoodArmor();
        SetSmallCannon();
    }

    public void SetWoodArmor()
    {
        m_RenderersWithWoodArmor.ForEach(oneRenderer => oneRenderer.SetMaterialToRenderer());
        m_Player.ArmorCoefficient = m_WoodArmorCoefficient;
    }
    public void SetSteelArmor()
    {
        m_RenderersWithSteelArmor.ForEach(oneRenderer => oneRenderer.SetMaterialToRenderer());
        m_Player.ArmorCoefficient = m_SteelArmorCoefficient;
    }

    public void SetCannon(GameObject cannonToBeSet)
    {
        m_AllCannons.ForEach(cannon => cannon.SetActive(false));
        cannonToBeSet.SetActive(true);
    }

    public void SetSmallCannon() => SetCannon(m_SmallCannon);
    public void SetBigCannon() => SetCannon(m_BigCannon);
}