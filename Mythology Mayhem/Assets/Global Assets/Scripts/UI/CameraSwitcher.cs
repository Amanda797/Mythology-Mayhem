using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
  public GameObject twoDPlayer;
  public GameObject threeDPlayer;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space)) 
    {
      Toggle2D3D(); 
    }
  }

  void Toggle2D3D() 
  {
    bool twoDScene = twoDPlayer.activeSelf;
    bool threeDScene = threeDPlayer.activeSelf;

    //Toggle Players
    twoDPlayer.SetActive(!twoDScene);
    threeDPlayer.SetActive(!threeDScene); 
  }

}
