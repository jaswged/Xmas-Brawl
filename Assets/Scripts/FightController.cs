using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FightController : MonoBehaviour {
    private Vector2 _iMovement;
    private float _moveSpeed = 10f;
    private Vector2 oldPosition;
    private bool _facingRight = true;  // For determining which way the player is currently facing.
    private bool _isAlreadyAttacking;
    [SerializeField] private float jumpHeight = 2;
    
    public Transform attackPos;
    public float attackRange = 2f;
    
    
    #region Animator
    private Animator _anim;
    private static readonly int SPEED = Animator.StringToHash("speed");
    #endregion

    private void Awake() {
        _anim = GetComponent<Animator>();
        oldPosition = transform.position;
    }

    private void OnMove(InputValue value) {
        _iMovement = value.Get<Vector2>();
    }
    
    private void OnJump() {
        Debug.Log("Jump! Moving up");
        transform.Translate(transform.up * jumpHeight);
    }
    
    private void OnPunch() {
        if (!_isAlreadyAttacking) {
            _isAlreadyAttacking = true;
            StartCoroutine(Attack(false));
        }
    }
    
    private void OnBlock() {
        Debug.Log("Block");
    }
    
    private void OnKick() {
        if (!_isAlreadyAttacking) {
            _isAlreadyAttacking = true;
            StartCoroutine(Attack(true));
        }
    }

    private void Update() {
        Move();
    }

    private void Move() {
        var movement = _moveSpeed * Time.deltaTime * _iMovement;
        transform.Translate(movement);
        
        var horizontalChange = _moveSpeed * _iMovement.x * Time.deltaTime;
        // If the input is moving the player right and the player is facing left...
        if (horizontalChange > 0 && !_facingRight) {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontalChange < 0 && _facingRight){
            Flip();
        }
        
        //_anim.SetFloat(SPEED, movement);
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
    private IEnumerator Attack(bool isKick) {
        _anim.SetTrigger(isKick ? "isKicking" : "isPunching");

        yield return new WaitForSeconds(.5f);
        
        Debug.DrawLine(transform.position, _facingRight ? Vector2.right: Vector2.left, Color.green);
        LayerMask mask = LayerMask.GetMask("Players");
        
       // From https://www.youtube.com/watch?v=1QfxdUpVh5I
       Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, mask);
       foreach (var enemy in enemiesToDamage) {
           if (enemy.transform.CompareTag(Tags.PLAYER)) continue;
           var enemyHealth = enemy.transform.GetComponentInParent<HasHealth>();
           if(enemyHealth != null)
               enemyHealth.ChangeHealth(isKick ? -30 : -20);
       }
       
        _isAlreadyAttacking = false;
    }

    private void OnDrawGizmos() {
        
    }

    private void OnDrawGizmosSelected() {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
