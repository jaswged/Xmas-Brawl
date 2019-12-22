using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float speed = 4f;
    [SerializeField] private Animator animator;
    
    private Rigidbody2D _rigidBody;
    private bool _facingRight = true;  // For determining which way the player is currently facing.
    private bool _isAlreadyAttacking;

    private void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        #region Movement
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        
        var horizontalChange = speed * horizontalInput * Time.deltaTime;
        var verticalChange =   speed * verticalInput * Time.deltaTime;
        
        Vector2 position = transform.position;
        position.x += horizontalChange;
        position.y += verticalChange;

        // If the input is moving the player right and the player is facing left...
        if (horizontalChange > 0 && !_facingRight) {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontalChange < 0 && _facingRight){
            Flip();
        }

        var movingSpeed = Mathf.Abs(horizontalChange + verticalChange);
        animator.SetFloat(Constants.SPEED, movingSpeed);
        
        _rigidBody.MovePosition(position);
        #endregion

        #region Attacks
        if (Input.GetMouseButtonDown(0) && !_isAlreadyAttacking) {
            _isAlreadyAttacking = true;
            StartCoroutine(Attack());
        }
        #endregion
    }
    
    private void Flip() {
        // Switch the way the player is labelled as facing.
        _facingRight  = !_facingRight;

        // Multiply the player's x local scale by -1.
        var transform1 = transform;
        Vector3 theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }

    // TODO this same method should be usable against us. Thus the separation of attack logic! @Taylor
    private IEnumerator Attack() {
        animator.SetTrigger(Constants.IS_ATTACKING_TRIGGER);
        yield return new WaitForSeconds(.5f);
        
        //Debug.DrawLine(transform.position, Vector2.left, Color.green); 
        var hits = Physics2D.RaycastAll(transform.position, _facingRight ? Vector2.right: Vector2.left, 1.5f);
        foreach (var v in hits) {
            //TODO use layer mask instead
            if (v.transform.CompareTag(Tags.PLAYER)) continue;
            var enemyHealth = v.transform.GetComponentInParent<HasHealth>();
            //var playerHealth = v.transform.GetComponentInParent<HasHealth>();
            enemyHealth.ChangeHealth(-20);
        }
        _isAlreadyAttacking = false;
    }
}
