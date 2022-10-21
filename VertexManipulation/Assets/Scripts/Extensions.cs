
using UnityEngine;


public static class Extensions
{
    //<summary> get the center position of the given gameobjects
    public static Vector3 Center<T>(this T[] monobehaviours)where T:MonoBehaviour
    {

        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;
        float minZ = float.MaxValue;
        float maxZ = float.MinValue;

        for (int i = 0; i < monobehaviours.Length; i++)
        {
            Vector3 position = monobehaviours[i].transform.localPosition;
            if (position.x < minX)
            {
                minX = position.x;
            }
            if (position.x > maxX)
            {
                maxX = position.x;
            }

            if (position.y < minY)
            {
                minY = position.y;
            }
             if (position.y > maxY)
            {
                maxY = position.y;
            }

            if (position.z < minZ)
            {
                minZ = position.z;
            }
            if (position.z > maxZ)
            {
                maxZ = position.z;
            }
        }


        float midX = (minX + maxX) / 2;
        float midY = (minY + maxY) / 2;
        float midZ = (minZ + maxZ) / 2;

        return new Vector3(midX, midY, midZ);
    }

}
