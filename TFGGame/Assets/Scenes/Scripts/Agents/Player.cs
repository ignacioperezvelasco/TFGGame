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

    //public/////////////
    public int myCrystals;
    public int myCrystalsPowder;
    public Weapons CurrentWeapon;
    #endregion

    #region METHODS
    private void Start()
    {            
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

    public void StopMovement()
    {
    }

    
    #endregion
}
