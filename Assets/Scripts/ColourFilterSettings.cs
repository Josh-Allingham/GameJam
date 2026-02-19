using SOG.CVDFilter;
using UnityEngine;

public class ColourFilterSettings : MonoBehaviour
{
    public int currentFilterIndex;

    [SerializeField]
    private GameObject colourFilter;

    private void Start()
    {
        currentFilterIndex = GlobalSettings.ColFilter;
    }

    private void Update()
    {
        currentFilterIndex = GlobalSettings.ColFilter;
        SetFilter();
    }

    public void SetFilter()
    {
        colourFilter.GetComponent<CVDFilter>().ChangeProfile(currentFilterIndex);
    }
}
