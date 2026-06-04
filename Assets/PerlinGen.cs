using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PerlinGen
{
    public class Perlin : MonoBehaviour
    {
        public static float PerlinGen3D(float x, float y, float z)
        {
            float noise = 0.0f;
            float noiseXY = Mathf.PerlinNoise(x, y);
            float noiseXZ = Mathf.PerlinNoise(x, z);
            float noiseYZ = Mathf.PerlinNoise(y, z);

            float noiseYX = Mathf.PerlinNoise(y, x);
            float noiseZX = Mathf.PerlinNoise(z, x);
            float noiseZY = Mathf.PerlinNoise(z, y);

            noise = (noiseXY + noiseXZ + noiseYZ + noiseYX + noiseZX + noiseZY) / 6.0f;
            // amp *= persistence;
            // freq *= 2.0f;
            // print(noise / oct);
            return noise;
        }
    }
}
