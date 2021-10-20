using UnityEngine;

    public class Player : Tile
    {
        #region Objects
        private float speed = 3f;

        private Rigidbody2D rb;
        private Animator animator;
        #endregion

        #region Properties
        /// <summary>
        /// Ultima direccion de desplazamiento del personaje
        /// </summary>
        private Vector2 PrevDirection { get; set; }
        /// <summary>
        /// Direccion de desplazamiento del personaje
        /// </summary>
        public Vector2 Direction { get; set; }
        #endregion


        #region Unity Methods
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            Set_Animation();
        }
        private void FixedUpdate()
        {
            this.transform.Translate(Direction * speed * Time.fixedDeltaTime);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Setea la animacion del personaje
        /// </summary>
        public void Set_Animation()
        {
            animator.SetFloat("prevHorizontal", PrevDirection.x);
            animator.SetFloat("prevVertical", PrevDirection.y);
            animator.SetFloat("Horizontal", Direction.x);
            animator.SetFloat("Vertical", Direction.y);
            animator.SetFloat("Speed", Direction.sqrMagnitude);

            if (Direction != Vector2.zero)
                PrevDirection = Direction;
        }
        #endregion
    }
