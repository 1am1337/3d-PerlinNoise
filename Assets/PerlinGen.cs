
using System.Collections;

using UnityEngine;

namespace Perlin
{
    public class Noise : MonoBehaviour
    {

        public static float Gen3D(float x, float y, float z)
        {
            float noiseXY = Mathf.PerlinNoise(x, y);
            float noiseXZ = Mathf.PerlinNoise(x, z);
            float noiseYZ = Mathf.PerlinNoise(y, z);
            // print((noiseXY, noiseXZ, noiseYZ));
            // print(Mathf.PerlinNoise(x, y));
            // float noiseYX = Mathf.PerlinNoise(y, x);
            // float noiseZX = Mathf.PerlinNoise(z, x);
            // float noiseZY = Mathf.PerlinNoise(z, y);

            // noise = (noiseXY + noiseXZ + noiseYZ + noiseYX + noiseZX + noiseZY) / 6.0f;
            float noise = (noiseXY + noiseXZ + noiseYZ) / 3.0f;
            // print(noise);
            // print((x, y, z));
            // amp *= persistence;
            // freq *= 2.0f;
            // print(noise / oct);
            return (float)noise;
        }
    }

}
