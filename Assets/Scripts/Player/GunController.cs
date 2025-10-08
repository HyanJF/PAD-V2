using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Referencias")]
    public Camera cam;
    public RectTransform crosshair;
    public LayerMask targetLayer;
    public AmmoUI ammoUI;

    [Header("Atributos")]
    public int maxAmmo = 6;
    private int currentAmmo;
    public float fireRate = 0.3f;
    public float reloadTime = 1.5f;

    private bool canShoot = true;
    private bool reloading = false;

    private void Start()
    {
        currentAmmo = maxAmmo;

        if (ammoUI != null)
            ammoUI.Initialize(maxAmmo);
    }

    public void OnFire()
    {
        if (!canShoot || reloading || currentAmmo <= 0) return;
        Shoot();
    }

    public void OnReload()
    {
        if (reloading || currentAmmo == maxAmmo) return;
        StartCoroutine(Reload());
    }

    private void Shoot()
    {
        currentAmmo--;
        canShoot = false;

        ammoUI?.UpdateAmmo(currentAmmo);

        Ray ray = cam.ScreenPointToRay(crosshair.position);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, targetLayer))
        {
            var car = hit.collider.GetComponent<CarController>();
            if (car != null)
                car.TakeDamage();
        }

        Invoke(nameof(ResetShoot), fireRate);
    }

    private void ResetShoot() => canShoot = true;

    private System.Collections.IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        reloading = false;

        ammoUI?.UpdateAmmo(currentAmmo);
    }
}
