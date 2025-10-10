using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [Header("Referencias")]
    public Camera cam;
    public RectTransform crosshair;
    public LayerMask targetLayer;
    public AmmoUI ammoUI;
    public GameController gameController;
    public AudioSource sfxSource;
    public AudioClip fireSound;
    public AudioClip reloadSound;

    [Header("Atributos")]
    public int maxAmmo = 6;
    private int currentAmmo;
    public float fireRate = 0.2f;
    public float reloadTime = 1.5f;
    public Image hit;
    public Image fire;
    public Image reload;
    private bool canShoot = true;
    private bool reloading = false;

    private void Start()
    {
        hit.enabled = false;
        fire.enabled = true;
        reload.enabled = true;
        currentAmmo = maxAmmo;

        if (ammoUI != null)
            ammoUI.Initialize(maxAmmo);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (!canShoot || reloading || currentAmmo <= 0) return;
        Shoot();
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (reloading || currentAmmo == maxAmmo) return;
        StartCoroutine(Inputflash(reload));
        StartCoroutine(Reload());
    }

    private void Shoot()
    {
        currentAmmo--;
        canShoot = false;
        

        ammoUI?.UpdateAmmo(currentAmmo);
        gameController.totalShots++;
        sfxSource?.PlayOneShot(fireSound);

        Ray ray = cam.ScreenPointToRay(crosshair.position);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, targetLayer))
        {
            var car = hit.collider.GetComponent<CarController>();
            if (car != null)
            {
                car.TakeDamage();
                StartCoroutine(FlashHitIndicator());
            }
        }

        Invoke(nameof(ResetShoot), fireRate);
    }

    private void ResetShoot() => canShoot = true;

    private IEnumerator Reload()
    {
        reloading = true;
        sfxSource?.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        reloading = false;

        ammoUI?.UpdateAmmo(currentAmmo);
    }

    private IEnumerator FlashHitIndicator()
    {
        hit.enabled = true;
        yield return new WaitForSeconds(.2f);
        hit.enabled = false;
    }

    private IEnumerator Inputflash(Image input)
    {
        input.enabled = false;
        yield return new WaitForSeconds(fireRate);
        input.enabled = true;
    }
}
