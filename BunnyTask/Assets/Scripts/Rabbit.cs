using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    [SerializeField] GameObject littleRabit;
    [SerializeField] GameObject muscleRabit;
    [SerializeField] GameObject digHole;

    [SerializeField] Animator animator;

    Bank bank;
    SceneController sceneController;
    void Start()
    {
        bank = FindObjectOfType<Bank>();
        sceneController = FindObjectOfType<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Carrot")
        {
            Destroy(other.gameObject);
            bank.IncreaseCarrotAmount();

            if (bank.carrotAmount % 5 == 0)
            {
                TransformToMuscleRabbit();
            }
        }
        if (other.gameObject.tag == "Traps" && !muscleRabit.activeInHierarchy)
        {
            //Destroy(other.gameObject);
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            animator.SetBool("isDead", true);
            sceneController.ShowLoseScreen();
        }
        else if (other.gameObject.tag == "Traps" && muscleRabit.activeInHierarchy)
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Finish")
        {
            sceneController.ShowWinScreen();
        }
    }

    public void DigOrUp()
    {
        if (littleRabit.activeInHierarchy)
        {
            GetComponent<CapsuleCollider>().enabled = false;
            littleRabit.SetActive(false);
            StartCoroutine(InstantiateDigHole());
        }
        else if (!littleRabit.activeInHierarchy && !muscleRabit.activeInHierarchy)
        {
            GetComponent<CapsuleCollider>().enabled = true;
            littleRabit.SetActive(true);
            StopAllCoroutines();
        }
    }

    IEnumerator InstantiateDigHole()
    {
        while (true)
        {
            GameObject newDigHole = Instantiate(digHole, transform.position, Quaternion.identity);
            Destroy(newDigHole, 1f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void TransformToMuscleRabbit()
    {
        muscleRabit.SetActive(true);
        littleRabit.SetActive(false);
        Invoke("TransformToLittleRabbit", 7f);
    }

    private void TransformToLittleRabbit()
    {
        muscleRabit.SetActive(false);
        littleRabit.SetActive(true);
    }
}
