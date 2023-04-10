using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float jumpForce = 5;
    public float groundDistance = .5f;
    private Rigidbody rb;
    bool canDoubleJump;

   
    private int count;
    private float movementX;
    private float movementY;
    private float movementZ;
    // Start is called before the first frame update

    bool isGrounded(){
        return Physics.Raycast(transform.position, Vector3.down, groundDistance);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        
    }

    void SetCountText(){
        countText.text = "Count: " + count.ToString();
        
        if(count >= 13){
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate(){
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);      
    }
    void Update(){
        if(Input.GetKeyDown("space")){
            if(isGrounded()){
                rb.velocity += Vector3.up * jumpForce;
                canDoubleJump = true;
            }
            else if(canDoubleJump){
                rb.velocity += Vector3.up * jumpForce;
                canDoubleJump = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp")){
            other.gameObject.SetActive(false);
            count = count + 1;
            
            SetCountText();
        }
        
    }
}
