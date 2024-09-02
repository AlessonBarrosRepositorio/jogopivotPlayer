using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controle;
    public Transform cam;

    public float velocidade = 6f;

    public float tempoGiroSuave = 0.1f;
    float giroSuaveVelocidade;

    void Start()
    {
        // Torna o cursor invisível
        Cursor.visible = false;

        // Trava o cursor no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
    }



    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3  direction = new Vector3(horizontal,0f,vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float ancoraAngulo = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, ancoraAngulo, ref giroSuaveVelocidade, tempoGiroSuave);
            transform.rotation = Quaternion.Euler(0f, angulo, 0f);

            Vector3 moverDir = Quaternion.Euler(0f, ancoraAngulo, 0f) * Vector3.forward;
            controle.Move(moverDir.normalized * velocidade * Time.deltaTime);
        }
    }
}
