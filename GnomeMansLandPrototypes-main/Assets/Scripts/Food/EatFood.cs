using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : MonoBehaviour
{
    public enum FoodType
    {
        Apple,
        Cherry
    }
    // public GameObject player; //our player
    [SerializeField] private CharacterController2D charController;
    [SerializeField] private PlayerMovement playerMove;

    public void applyEffect(FoodType foodType)
    {
        switch (foodType)
        {
            case FoodType.Apple:
                StartCoroutine("AppleEffect");
                break;
            case FoodType.Cherry:
                StartCoroutine("CherryEffect");
                break;
            default:
                return;
        }
    }

    IEnumerator AppleEffect()
    {
        float orig = charController.m_JumpForce;
        charController.m_JumpForce = 800f;
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("AppleEffect: " + i);
            yield return new WaitForSeconds(1f);
        }
        charController.m_JumpForce = orig;
    }

    IEnumerator CherryEffect()
    {
        float orig = playerMove.runSpeed;
        playerMove.runSpeed = 80f;
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("CherryEffect: " + i);
            yield return new WaitForSeconds(1f);
        }
        playerMove.runSpeed = orig;
    }
}