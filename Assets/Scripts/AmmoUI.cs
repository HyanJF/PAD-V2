using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [Header("Imagen de las balas (con Fill Amount)")]
    public Image ammoFillImage;

    private int maxAmmo;

    public void Initialize(int maxAmmo)
    {
        this.maxAmmo = maxAmmo;
        UpdateAmmo(maxAmmo);
    }

    public void UpdateAmmo(int currentAmmo)
    {
        if (ammoFillImage == null || maxAmmo <= 0) return;

        float fill = Mathf.Clamp01((float)currentAmmo / maxAmmo);
        ammoFillImage.fillAmount = fill;
    }
}
