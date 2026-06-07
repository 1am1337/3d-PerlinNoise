
using System.Collections;

using UnityEngine;

namespace Perlin
{
    public class Noise : MonoBehaviour
    {

        public static float Gen3D(int x, int y, int z, float noiseScale)
        {
            float noiseXY = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);
            float noiseXZ = Mathf.PerlinNoise(x * noiseScale, z * noiseScale);
            float noiseYZ = Mathf.PerlinNoise(y * noiseScale, z * noiseScale);

            float noiseYX = Mathf.PerlinNoise(y * noiseScale, x * noiseScale);
            float noiseZX = Mathf.PerlinNoise(z * noiseScale, x * noiseScale);
            float noiseZY = Mathf.PerlinNoise(z * noiseScale, y * noiseScale);

            float noise = (noiseXY + noiseXZ + noiseYZ + noiseYX + noiseZX + noiseZY) / 6.0f;
            // float noise = (noiseXY + noiseXZ + noiseYZ) / 3.0f;

            // amp *= persistence;

            return (float)noise;
        }
    }

}
