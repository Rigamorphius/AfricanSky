using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour {

   [SerializeField] AudioSource hoverAudio;

    private void Start()
    {
        hoverAudio = GetComponent<AudioSource>();
    }

    public void QuitGame()
    {
# if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
       Application.Quit();
#endif
    }

    public void PlaySound(AudioSource source)
    {
        Debug.Log("sound played");
        hoverAudio.PlayOneShot(hoverAudio.clip);
    }

}
