using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualisation : MonoBehaviour
{
    [SerializeField]
    Material DiskMat;
    [SerializeField]
    int dangerLimit = 10;
    [SerializeField]
    float alpha = 0.5f;

    float volume = 10;
    float progression = 0.0f;
    float colourProgression = 0.0f;
    float radius = 0;
    Color colorStart;
    Color colorEnd;

    GameObject disk;

    private void Start()
    {
        colorEnd = new Color(1.0f, 0.0f, 0.0f, alpha);
        colorStart = new Color(0.0f, 1.0f, 0.0f, alpha);

        disk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        disk.transform.localScale = new Vector3(1.25f, 0.01f, 1f);
        disk.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        disk.GetComponent<CapsuleCollider>().enabled = false;
        disk.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        disk.GetComponent<MeshRenderer>().material = DiskMat;
        disk.transform.SetParent(transform);
    }

    private void Update()
    {
        float dangerLevel = 1-(volume / dangerLimit);
        if (dangerLevel < 0)
            dangerLevel = 0;

        //print(volume);

        progression += 0.4f * Time.deltaTime;

        colourProgression += 0.4f * Time.deltaTime;

        if (progression > 1)
        {
            Destroy(gameObject);
        }

        var shape = this.GetComponent<ParticleSystem>().shape;
        
        radius = Mathf.Lerp(0.1f, volume, progression);
        shape.radius = radius;
        shape.donutRadius = Mathf.Lerp(0.1f, volume/20, progression);

        disk.transform.localScale = new Vector3(radius*2, 0.01f, radius*2);
        Collider[] col = Physics.OverlapSphere(disk.transform.position, radius, (1 << 9));
        if(col.Length != 0)
        {
            foreach(Collider c in col)
            {
                Blinky blink = c.GetComponent<Blinky>();
                if(blink != null)
                {
                    blink.alertEnemy();
                }
            }
        }
        Material material = disk.GetComponent<MeshRenderer>().material;
        material.color = Color.Lerp(colorStart, colorEnd, colourProgression - dangerLevel);
    }

    public void setVolume(float vol, int stickyMultiplier)
    {
        float mult = 1 + (stickyMultiplier / 20);
        volume = vol * mult;
    }
}
