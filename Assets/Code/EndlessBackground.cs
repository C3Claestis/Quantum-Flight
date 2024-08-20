using System;
using UnityEngine;

public class EndlessBackground : MonoBehaviour
{
    private float[] scrollSpeed = {0.5f, 1.5f, 2.5f};
    private Material material;
    private Vector2 offset;
    private int gearMachine = 1;

    public void SetSpeed(int SetSpeed) => this.gearMachine = SetSpeed;
    void Start()
    {
        // Dapatkan material dari sprite renderer
        material = GetComponent<Renderer>().material;
        offset = new Vector2(0, 0);
    }

    void Update()
    {
        // Menghitung offset berdasarkan waktu dan kecepatan
        offset.x += scrollSpeed[gearMachine] * Time.deltaTime;

        // Terapkan offset ke material
        material.mainTextureOffset = offset;
    }
}
