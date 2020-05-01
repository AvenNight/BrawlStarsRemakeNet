using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [SerializeField]
    private Image bar;

    public void BarChange(float fill)
    {
        bar.fillAmount = fill;
    }
}