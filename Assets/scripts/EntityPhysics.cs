using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JamProject
{
    public class EntityPhysics : MonoBehaviour
    {
        public Vector3 velocity, checkOffset;
        public bool grounded, groundCheck;
        public float gravityVal, lastYvel, floorSnapVal, checkDis;
        public Rigidbody rbComponent;
        // Start is called before the first frame update
        void Start()
        {
            rbComponent = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void FixedUpdate()
        {
            CheckGround();
            if (grounded)
            {
                velocity.y = 0;
            }
            velocity.y -= gravityVal;
            rbComponent.velocity = velocity;
        }


        private void LateUpdate()
        {
            lastYvel = velocity.y;
        }
        void CheckGround()
        {
            RaycastHit h1, h2, h3;
            float hitnumber = 0, yval = 0;

            if (Physics.Raycast(transform.position + checkOffset, Vector3.down, out h1, checkDis, LayerMask.GetMask("Solid"))) { hitnumber++; yval += h1.point.y; }
            if (Physics.Raycast(transform.position + checkOffset + Vector3.right * 0.25f, Vector3.down, out h2, checkDis, LayerMask.GetMask("Solid"))) { hitnumber++; yval += h2.point.y; }
            if (Physics.Raycast(transform.position + checkOffset - Vector3.right * 0.25f, Vector3.down, out h3, checkDis, LayerMask.GetMask("Solid"))) { hitnumber++; yval += h3.point.y; }

            grounded = hitnumber > 0;
            
        }
    }
}
