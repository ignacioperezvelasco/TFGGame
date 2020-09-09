using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    #region ENUMS
    enum actions { ATTACK, DEFEND, INTERACT, JUMP, MOVE }
    enum skills { }
    #endregion

    #region VARIABLES

    //private////////////
    actions currentAction;
    //Movement
    private Rigidbody playerBody;
    Transform cam;
    InputManager myInput;
    /// //
    
    //public/////////////
    public int myCrystals;
    public int myCrystalsPowder;
    public Weapons CurrentWeapon;
    #endregion

    #region METHODS
    private void Start()
    {

        //Getting components //Input
        myInput = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InputManager>();
        //Rb
        playerBody = GetComponent<Rigidbody>();

        //Initialize
        speed = 3;
    }

    void GetInpuCOntroller()
    {
    }

    void HandleGamePadInput()
    {
    }

    void HandleKeyboardInput()
    {
    }

    void AddMovement()
    {
        playerBody.velocity = new Vector3 (myInput.inputVector.x * speed,playerBody.velocity.y, myInput.inputVector.z * speed);
    }

    void Skills(skills skill)
    {
    }

    void Jump()
    {
    }

    void Dash()
    {
    }

    void Attack()
    {
    }

    void Protect()
    {
    }

    private void Update()
    {
        //movement
        AddMovement();
    }

    public void StopMovement()
    {
    }

    
    #endregion
}
