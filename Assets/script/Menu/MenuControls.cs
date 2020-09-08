using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour {

    public List<GameObject> buttons = new List<GameObject>();
    public int buttonSelected = 0;

    public Canvas canvasMainMenu;
    public Canvas canvasControlli;

    public bool isControlOpen = false;


    private bool isMoving = false;
    

    private void Update()
    {
       if (Input.GetAxis("LeftStickVerticalPlayer1") > 0 && isMoving == false && buttonSelected < 2 && isControlOpen == false)
        {
            isMoving = true;
            ++buttonSelected;
        }

        if (Input.GetAxis("LeftStickVerticalPlayer1") < 0 && isMoving == false && buttonSelected > 0 && isControlOpen == false)
        {
            isMoving = true;
            --buttonSelected;
        }

        if (Input.GetAxis("LeftStickVerticalPlayer1") == 0 && isControlOpen == false)
        {
            isMoving = false;
            
        }

        if (Input.GetButtonDown("Player1B"))
        {
            isControlOpen = false;
        }

            switch (buttonSelected)
        {
            case 0:
                if (Input.GetButtonDown("Player1A"))
                {
                    SceneManager.LoadScene("ShinyScene");
                }
                break;

            case 1:
                if (Input.GetButtonDown("Player1A"))
                {
                    isControlOpen = true;
                 
                }
                break;
        

            case 2:
                if (Input.GetButtonDown("Player1A"))
                {
                    Application.Quit();
                }
                break;
        }

        if (isControlOpen)
        {
            canvasMainMenu.gameObject.SetActive(false);
            canvasControlli.gameObject.SetActive(true);
        }
        else
        {
            canvasMainMenu.gameObject.SetActive(true);
            canvasControlli.gameObject.SetActive(false);
        }

    }

}
