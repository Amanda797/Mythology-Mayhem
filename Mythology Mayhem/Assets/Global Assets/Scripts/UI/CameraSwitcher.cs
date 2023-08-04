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
    //Toggle Players
    twoDPlayer.SetActive(!twoDPlayer.activeSelf);
    threeDPlayer.SetActive(!threeDPlayer.activeSelf); 
  }

}
