using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Hangman : MonoBehaviour
    {
        public float speed = 4.0f;
        public float jumpForceMult = 100.0f;
        public float maxVelocity = 20.0f;

        public GameObject head;
        public GameObject body;
        public GameObject lArm;
        public GameObject rArm;
        public GameObject lLeg;
        public GameObject rLeg;
        public GameObject lEye;
        public GameObject rEye;
        public GameObject mouth;


        public GameObject headBlood;
        public GameObject rArmBlood;
        public GameObject lArmBlood;
        public GameObject rLegBlood;
        public GameObject lLegBlood;

        private Rigidbody2D _rigidBody2D;
        private bool _grounded;
        private bool _moving;
        private int _wongCount;

        
        private float _initHeight;
        private bool _waitingToLand;

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (_waitingToLand && coll.gameObject.tag == "ground")
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity);

                if(hit.collider != null)
                {
                    _initHeight = Vector2.Distance(hit.point, transform.position);
                    _grounded = true;
                }
                else
                {
                    Debug.LogError("Can't see the ground!!");
                    _initHeight = transform.position.y;
                }
            }
            else if(coll.gameObject.tag == "ground" && !_grounded)
            {
                _grounded = true;
            }
        }





        public int WrongCount
        {
            get { return _wongCount; }
            set
            {
                _wongCount = value;

                switch(value)
                {
                    case 1:
                        lArm.gameObject.SetActive(false);
                        lArmBlood.gameObject.SetActive(true);
                        break;
                    case 2:
                        rArm.gameObject.SetActive(false);
                        rArmBlood.gameObject.SetActive(true);
                        break;
                    case 3:
                        lLeg.gameObject.SetActive(false);
                        lLegBlood.gameObject.SetActive(true);
                        break;
                    case 4:
                        rLeg.gameObject.SetActive(false);
                        rLegBlood.gameObject.SetActive(true);
                        break;
                    case 5:
                        body.gameObject.SetActive(false);
                        headBlood.gameObject.SetActive(true);

                        lLegBlood.gameObject.SetActive(false);
                        rLegBlood.gameObject.SetActive(false);
                        lArmBlood.gameObject.SetActive(false);
                        rArmBlood.gameObject.SetActive(false);
                        break;
                    case 6:
                        mouth.gameObject.SetActive(false);
                        break;
                    case 7:
                        lEye.gameObject.SetActive(false);
                        break;
                    case 8:
                        rEye.gameObject.SetActive(false);
                        break;
                    case 9:
                        head.gameObject.SetActive(false);
                        break;
                    case 0:
                        lArm.gameObject.SetActive(true);
                        rArm.gameObject.SetActive(true);
                        lLeg.gameObject.SetActive(true);
                        rLeg.gameObject.SetActive(true);
                        body.gameObject.SetActive(true);
                        head.gameObject.SetActive(true);
                        break;
                }
            }
        }

        public bool IsDead
        {
            get
            {
                return _wongCount >= 9;
            }
        }

        

        void Start()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _moving = false;
            _wongCount = 0;
            _grounded = true;            
        }

        void Update()
        {
            float hor = Input.GetAxis("Horizontal");            
           
            if(hor != 0)
            {
                _rigidBody2D.AddForce(Mathf.Sign(hor) * speed * Vector2.right * _rigidBody2D.mass * 10);
                _moving = true;
            }

            


            if(Input.GetAxis("Jump") > 0 && _grounded)
            {
                _grounded = false;
                StartCoroutine(Land());

                Vector2 f = Vector2.up * jumpForceMult * _rigidBody2D.mass * 6;
                if (_wongCount > 5)
                    f *= 1.85f;

                _rigidBody2D.AddForce(f);               
            }
            
            //cap horizontal speed
            float horizontalSpeed = Mathf.Max(
                Vector2.Dot(Vector2.right, _rigidBody2D.velocity),
                Vector2.Dot(-Vector2.right, _rigidBody2D.velocity));




            if (_rigidBody2D.velocity.magnitude > maxVelocity)
                _rigidBody2D.velocity = _rigidBody2D.velocity.normalized * maxVelocity;
           
        }


        #region Coroutines
        IEnumerator Land()
        {            
            WaitForSeconds wait = new WaitForSeconds(0.75f);
            yield return wait;

            _waitingToLand = true;          
        }

        IEnumerator KillMoving()
        {
            WaitForSeconds wait = new WaitForSeconds(0.75f);
            yield return wait;

            _moving = true;

            float hor = Input.GetAxis("Horizontal");        
            while(Mathf.Abs(hor) > -.1f)
            {
                yield return false;
            }

            _moving = false;
        }

        #endregion

    }
}
