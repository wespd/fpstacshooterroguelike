using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class LockToHexGrid : MonoBehaviour
{
    public Vector3 size = new Vector3(1,1,1);
    public Vector3 offset = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if(!EditorApplication.isPlaying)
        {
            Vector3 currentPosition = transform.position;

            float snappedX = Mathf.Round(currentPosition.x/size.x) * size.x + offset.x;
            float snappedY = Mathf.Round(currentPosition.y/size.y) * size.y + offset.y;
            float snappedZ = Mathf.Round((currentPosition.z - .1f * size.z)/size.z) * size.z + offset.z;
            
            if((snappedX % (size.x * 2)) == size.x || (snappedX % (size.x * 2)) == -size.x  !| (snappedZ == 0 && !(snappedX % (size.x * 2) == 0)))
            {
                snappedZ += .5f * size.x;
            }
            snappedX -= .15f * snappedX;
            

            Vector3 snappedPosition = new Vector3(snappedX, snappedY, snappedZ);
            transform.position = snappedPosition;
        }
    }
}
