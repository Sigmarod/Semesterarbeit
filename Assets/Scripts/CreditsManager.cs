using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public void back(){
        SceneManager.LoadScene("StartMenu"); 
    }

    public void openLinkRaycast(){
        Application.OpenURL("https://www.youtube.com/watch?v=THnivyG0Mvo");
    }

    public void openLinkObjectPooling(){
        Application.OpenURL("https://www.youtube.com/watch?v=tdSmKaJvCoA");
    }

    public void openLinkFPSMovement(){
        Application.OpenURL("https://www.youtube.com/watch?v=_QajrabyTJc");
    }

    public void openLinkRevolver(){
        Application.OpenURL("https://assetstore.unity.com/packages/3d/props/guns/revolver-base-95137");
    }

    public void openLinkRocketLauncher(){
        Application.OpenURL("https://assetstore.unity.com/packages/3d/props/guns/stylized-rocket-launcher-complete-kit-with-visual-effects-and-so-178718");
    }

    public void openLinkTeleporter(){
        Application.OpenURL("https://assetstore.unity.com/packages/tools/custom-teleporter-pad-script-20098");
    }

     public void openLinkKeyboardPrompts(){
        Application.OpenURL("https://thoseawesomeguys.com/prompts/");
    }
}
