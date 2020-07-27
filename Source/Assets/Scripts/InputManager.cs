using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class InputManager : MonoBehaviour
{
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    [HideInInspector] public bool interact;
    [HideInInspector] public bool XLDown;
    [HideInInspector] public bool BJDown;
    [HideInInspector] public bool YIDown;

    bool readyToClear; //Esta variable indicará si el input debe limpiarse o no en el siguiente Update().

    [Header("Offset mínimo del stick")]
    [Tooltip("Sirve para que el stick solo funcione cuando llegue a una posicion mínima.")]
    public float minStickOffset = 0.2f;

    // Update is called once per frame
    void Update()
    {
        ClearInput();

        ProcessInput();

        //Clampeamos los ejes x e y
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
        vertical = Mathf.Clamp(vertical, -1f, 1f);
    }

    private void FixedUpdate()
    {
        readyToClear = true; //Este flag permite que el input se limpie durante
                             //el Update y asegura que no se va a perder ningún input.
    }

    void ClearInput()
    {
        if (!readyToClear)
        {
            return;
        }

        //Limpiamos las variables
        horizontal = 0f;
        vertical = 0f;
        interact = false;
        XLDown = false;
        BJDown = false;
        YIDown = false;

        readyToClear = false;
    }

    void ProcessInput()
    {
        //Acumulamos horizontal y vertical
        if (Input.GetAxisRaw("Horizontal") > minStickOffset || Input.GetAxisRaw("Horizontal") < -minStickOffset)
        {
            horizontal += Input.GetAxisRaw("Horizontal");
        }

        if (Input.GetAxisRaw("Vertical") > minStickOffset || Input.GetAxisRaw("Vertical") < -minStickOffset)
        {
            vertical += Input.GetAxisRaw("Vertical");
        }

        //Acumulamos inputs de los botones
        interact = interact || Input.GetButtonDown("Interact");
        XLDown = XLDown || Input.GetButtonDown("XLDown");
        BJDown = BJDown || Input.GetButtonDown("BJDown");
        YIDown = YIDown || Input.GetButtonDown("YIDown");
    }

}
