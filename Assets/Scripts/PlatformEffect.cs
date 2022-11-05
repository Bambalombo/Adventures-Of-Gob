using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffect : MonoBehaviour
{
    public GameObject ParticleSystemPrefab;

    private float startScale;
    public float scale, scaleY;
    private float ShrinkSpeed = 20;

    //[Header("Water Specific")]
    //public GameObject waterParticles;
    //public GameObject oilParticles;

    //public bool SpawnOil = true;

    void Start()
    {
        startScale = transform.localScale.x;
    }

    void Update()
    {
        if (scale > startScale)
            scale -= Time.deltaTime * ShrinkSpeed;
        scale = scale < startScale ? scale = startScale : scale = scale;

        scaleY = startScale - scale / 4;
        scaleY = scaleY > startScale ? scaleY = startScale : scaleY = scaleY;

        transform.localScale = new Vector3(scale, scaleY, startScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DoSquash();

        SpawnParticles();
    }

    private void SpawnParticles()
    {
        if (ParticleSystemPrefab != null)
        {
            GameObject ParticleSystemPrefabSpawn = Instantiate(ParticleSystemPrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(ParticleSystemPrefabSpawn, 3);
        }
        //else if (oilParticles != null)
        //{
        //    GameObject ParticleSystemPrefabSpawn = Instantiate(oilParticles, transform.position, Quaternion.identity) as GameObject;
        //    Destroy(ParticleSystemPrefabSpawn, 3);
        //}

        //else if (waterParticles != null)
        //{
        //    GameObject ParticleSystemPrefabSpawn = Instantiate(waterParticles, transform.position, Quaternion.identity) as GameObject;
        //    Destroy(ParticleSystemPrefabSpawn, 3);
        //}
    }

    public void DoSquash()
    {
        scale = startScale + 3;
    }
}