using System.Collections.Generic;
using Random = System.Random;

namespace _Scripts
{
    public static class Extensions
    {
        // Shuffles a list
        public static void Shuffle<T>(this IList<T> list)  
        {  
            Random rng = new Random();
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
        
        // updates the position vector3 z value to new z, leaving alone x and y
        public static void SetZ(this UnityEngine.Transform transform, float z)
        {
            UnityEngine.Vector3 pos = transform.position;
            transform.position = new UnityEngine.Vector3(pos.x, pos.y, z);
        }
        
        // updates the local position vector3 z value to new z, leaving alone x and y
        public static void SetZLocal(this UnityEngine.Transform transform, float z)
        {
            UnityEngine.Vector3 pos = transform.localPosition;
            transform.localPosition = new UnityEngine.Vector3(pos.x, pos.y, z);
        }
    }
}