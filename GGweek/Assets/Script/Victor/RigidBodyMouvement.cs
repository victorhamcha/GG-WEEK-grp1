using System;
using UnityEngine;
using System.Collections;

    public class RigidBodyMouvement : MonoBehaviour
    {
        [Header("Movement")]
        public float movementSpeed;
        public float airMovementSpeed;
        public float maxSpeed;

        [Header("Friction")]
        public float friction;
        public float airFriction;


        [Header("Gravity")]
        public float extraGravity;

        [Header("Ground Detection")]
        public LayerMask whatIsGround;
        public float checkYOffset;
        public float checkRadius;
        public float groundTimer;

        [Header("Jumping")]
        public float jumpForce;
        public float jumpCooldown;

        [Header("Data")]
        public Rigidbody _rb;



        
        private bool _realGrounded;
        private float _jumpCooldown;
        public bool canMove = true;
     
        private void Start()
        {
          
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
          if(canMove)
            {
                ApplyMovement();
                ApplyFriction();
                ApplyGravity();
            }
            
        }

        private void Update()
        {
            Jumping();
           
        }

        private bool IsGrounded()
        {
            return Physics.CheckCapsule(GetComponent<CapsuleCollider>().bounds.center, new Vector3(GetComponent<CapsuleCollider>().bounds.center.x, GetComponent<CapsuleCollider>().bounds.min.y, GetComponent<CapsuleCollider>().bounds.center.z), GetComponent<CapsuleCollider>().radius * 0.9f, whatIsGround);

        }
   

        private void ApplyMovement()
        {
            Vector2 axis = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            ).normalized;

            float speed = IsGrounded() ? movementSpeed : airMovementSpeed;
            Vector3 vertical = axis.y * speed * Time.fixedDeltaTime * transform.forward;
            Vector3 horizontal = axis.x * speed * Time.fixedDeltaTime * transform.right;
           

            
                if (CanApplyForce(vertical, axis))
                    _rb.velocity += vertical;

                if (CanApplyForce(horizontal, axis))
                    _rb.velocity += horizontal;
            

        }

        private void ApplyFriction()
        {
            Vector3 vel = _rb.velocity;
            float target = IsGrounded() ? friction : airFriction;
            vel.x = Mathf.Lerp(vel.x, 0f, target * Time.fixedDeltaTime);
            vel.z = Mathf.Lerp(vel.z, 0f, target * Time.fixedDeltaTime);
            _rb.velocity = vel;
        }



        private void ApplyGravity()
        {
            Vector3 vel = _rb.velocity;
            vel.y -= Mathf.Abs(vel.y) * Time.fixedDeltaTime * extraGravity;
            _rb.velocity = vel;
        }

        private void Jumping()
        {

            _jumpCooldown -= Time.deltaTime;
            if (!IsGrounded() || !(_jumpCooldown <= 0) || !Input.GetKey(KeyCode.Space)) return;
            Vector3 vel = _rb.velocity;
            vel.y = jumpForce;
            _rb.velocity = vel;
            _jumpCooldown = jumpCooldown;
        }

        

       

        private bool CanApplyForce(Vector3 target, Vector2 axis)
        {
            Vector2 targetC = Get2DVec(target).normalized;
            Vector2 velocityC = Get2DVec(_rb.velocity).normalized;
            float dotProduct = Vector2.Dot(velocityC, targetC);
            return dotProduct <= 0 || dotProduct * Get2DVec(_rb.velocity).magnitude < maxSpeed * GetAxisForce(axis);
        }

        private static float GetAxisForce(Vector2 axis)
        {
            return (int)axis.x != 0 ? Mathf.Abs(axis.x) : Mathf.Abs(axis.y);
        }

        private static Vector2 Get2DVec(Vector3 vec)
        {
            return new Vector2(vec.x, vec.z);
        }
    }

