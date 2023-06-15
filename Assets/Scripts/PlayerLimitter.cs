using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimitter : MonoBehaviour
{
    class Box
    {
        public float zMax;
        public float zMin;
        public float xMax;
        public float xMin;
        public float yMax;
        public float yMin;

        public Box(GameObject backObj,  GameObject frontObj,
                   GameObject rightObj, GameObject leftObj, 
                   GameObject topObj,   GameObject bottomObj)
        {
            this.zMax = backObj.transform.position.z;
            this.zMin = frontObj.transform.position.z;
            this.xMax = rightObj.transform.position.x;
            this.xMin = leftObj.transform.position.x;
            this.yMax = topObj.transform.position.y;
            this.yMin = bottomObj.transform.position.y;
        }
    }
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject floor;
    private Camera _camera;
    private Box _box;

    [SerializeField] private GameObject player;
    
    // Start is called before the first frame backdate
    void Start()
    {
        _camera = Camera.main;
        _box = new Box(backWall, frontWall, rightWall, leftWall, _camera.gameObject, floor);
    }

    // Update is called once per frame
    void Update()
    {
        WrapCoordinatesInRange();
    }

    void WrapCoordinatesInRange()
    {
        Vector3 playerPos = player.transform.position;
        // if all in range, do nothing
        if (playerPos.z < _box.zMax && playerPos.z > _box.zMin && 
            playerPos.x < _box.xMax && playerPos.x > _box.xMin && 
            playerPos.y < _box.yMax && playerPos.y > _box.yMin)
            return;
        WrapInRange(ref playerPos.z, _box.zMin, _box.zMax);
        WrapInRange(ref playerPos.x, _box.xMin, _box.xMax);
        WrapInRange(ref playerPos.y, _box.yMin, _box.yMax);
        player.transform.position = playerPos;
    }

    void WrapInRange(ref float num, float min, float max)
    {
        if (num < min) num = max;
        if (num > max) num = min;
    }
}
