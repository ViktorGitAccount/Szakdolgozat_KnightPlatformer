using Cinemachine;
using System.Collections;
using UnityEngine;

public class CoreFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("AfterImage Fx")]
    [SerializeField] private float afterImageCooldown;
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float colorLoseRate;
    public float afterImageCooldownTimer;


    [Header("Shake FX")]
    public CinemachineImpulseSource impulseSource;

    [Header("Flash FX")]
    [SerializeField] private Material hitMaterial;
    [SerializeField] private float flashDuration;
    private Material originalMaterial;


    [Space]
    [SerializeField] private ParticleSystem dustFx;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        originalMaterial = sr.material;

    }

    private void Update()
    {
        afterImageCooldownTimer -= Time.deltaTime;
    }

    public void CreateAfterImage()
    {
        if (afterImageCooldownTimer <= 0)
        {
            afterImageCooldownTimer = afterImageCooldown;
            GameObject newAfterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);

            var afterImageScript = newAfterImage.GetComponent<AfterImageFX>();
            if (afterImageScript != null)
            {
                afterImageScript.SetUpAfterImage(colorLoseRate, sr.sprite);
            }
        }
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMaterial;
        Color currentColor = sr.color;

        sr.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        sr.color = currentColor;
        sr.material = originalMaterial;

    }

    public void ShakeScreen()
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }



    public void PlayDustFx()
    {
        if (dustFx != null)
            dustFx.Play();
    }


}
