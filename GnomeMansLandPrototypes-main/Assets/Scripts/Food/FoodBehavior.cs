using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehavior : MonoBehaviour
{
    private EatFood foodEater;
    public EatFood.FoodType foodType;
    private GameObject player; //our player
   
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        foodEater = player.GetComponent<EatFood>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            foodEater.applyEffect(foodType);
        }
        Destroy(transform.gameObject);  // kills self
    }

}
