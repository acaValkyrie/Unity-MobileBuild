using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimitter : MonoBehaviour
{
    class Wall
    {
        private GameObject back;
        private GameObject front;
        private GameObject right;
        private GameObject left;

        public float zMax;
        public float zMin;
        public float xMax;
        public float xMin;

        public Wall(GameObject backObj, GameObject frontObj, GameObject rightObj, GameObject leftObj)
        {
            this.back = backObj;
            this.front = frontObj;
            this.right = rightObj;
            this.left = leftObj;
            this.zMax = back.transform.position.z;
            this.zMin = front.transform.position.z;
            this.xMax = right.transform.position.x;
            this.xMin = left.transform.position.x;
        }
    }
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject leftWall;
    private Wall _wall;
    
    private Camera _camera;
    private float _cameraHeight;
    [SerializeField] private GameObject floor;
    private float _floorHeight;

    [SerializeField] private GameObject player;
    
    // Start is called before the first frame backdate
    void Start()
    {
        _wall = new Wall(backWall, frontWall, rightWall, leftWall);
        _camera = Camera.main;
        _cameraHeight = _camera.transform.position.y;
        _floorHeight = floor.transform.position.y;
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
        if (playerPos.z < _wall.zMax    && playerPos.z > _wall.zMin && 
            playerPos.x < _wall.xMax    && playerPos.x > _wall.xMin && 
            playerPos.y < _cameraHeight && playerPos.y > _floorHeight)
            return;
        WrapInRange(ref playerPos.z, _wall.zMin, _wall.zMax);
        WrapInRange(ref playerPos.x, _wall.xMin, _wall.xMax);
        WrapInRange(ref playerPos.y, _floorHeight, _cameraHeight);
        player.transform.position = playerPos;
    }

    void WrapInRange(ref float num, float min, float max)
    {
        if (num < min) num = max;
        if (num > max) num = min;
    }
}
