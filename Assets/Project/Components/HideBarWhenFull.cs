using Microlight.MicroBar;
using UnityEngine;
using UnityEngine.UI;

public class HideBarWhenFull : MonoBehaviour
{
    [SerializeField] private MicroBar bar;

    private void Update()
    {
        if (bar == null) return;

        if (bar.CurrentValue >= bar.MaxValue)
        {
            HideBar();
        }
        else
        {
            ShowBar();
        }
    }

    private void HideBar()
    {
        bar.enabled = false;
        foreach (Image image in bar.GetComponentsInChildren<UnityEngine.UI.Image>())
        {
            image.enabled = false;
        }
    }
    private void ShowBar()
    {
        bar.enabled = true;
        foreach (Image image in bar.GetComponentsInChildren<UnityEngine.UI.Image>())
        {
            image.enabled = true;
        }
    }
}
