using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimitter : MonoBehaviour
{
    class Wall
    {
        private GameObject up;
        private GameObject down;
        private GameObject right;
        private GameObject left;

        public float upLimit;
        public float downLimit;
        public float rightLimit;
        public float leftLimit;

        public Wall(GameObject upObj, GameObject downObj, GameObject rightObj, GameObject leftObj)
        {
            this.up = upObj;
            this.down = downObj;
            this.right = rightObj;
            this.left = leftObj;
            this.upLimit = up.transform.position.z;
            this.downLimit = down.transform.position.z;
            this.rightLimit = right.transform.position.x;
            this.leftLimit = left.transform.position.x;
        }
    }
    [SerializeField] private GameObject upWall;
    [SerializeField] private GameObject downWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject leftWall;
    private Wall _wall;
    
    [SerializeField] private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        _wall = new Wall(upWall, downWall, rightWall, leftWall);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        if (playerPos.z < _wall.upLimit && playerPos.z > _wall.downLimit && playerPos.x < _wall.rightLimit && playerPos.x > _wall.leftLimit && playerPos.y < 10.0f && playerPos.y > 0.0f)
            return;
        if (playerPos.z > _wall.upLimit)
            playerPos.z = _wall.downLimit;
        if (playerPos.z < _wall.downLimit)
            playerPos.z = _wall.upLimit;
        if (playerPos.x > _wall.rightLimit)
            playerPos.x = _wall.leftLimit;
        if(playerPos.x < _wall.leftLimit)
            playerPos.x = _wall.rightLimit;
        if(playerPos.y < 0.0f)
            playerPos.y = 10.0f;
        if(playerPos.y > 10.0f)
            playerPos.y = 0.0f;
        player.transform.position = playerPos;
    }
}
