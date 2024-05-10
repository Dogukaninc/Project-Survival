using System;
using TMPro;

public class ResourceHandler : MonoSingleton<ResourceHandler>
{

    public int wood;
    public int stone;
    public int scrap_metal;

    public TextMeshProUGUI woodResourceText;
    public TextMeshProUGUI stoneResourceText;
    public TextMeshProUGUI scrap_metalResourceText;

    public Action updateResourcesAction;

    private void OnEnable()
    {
        updateResourcesAction += UpdateResources;
    }

    private void OnDisable()
    {
        updateResourcesAction -= UpdateResources;
    }

    public void UpdateResources()//Odun, metal,tas parametre olarak girilip burdan ekleme çýkarma yapýlabilir
    {
        woodResourceText.text = wood.ToString();
        stoneResourceText.text = stone.ToString();
        scrap_metalResourceText.text = scrap_metal.ToString();
    }

}
