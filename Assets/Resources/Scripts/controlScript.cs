using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Texture[] controlTextures;
    public Texture altArt;
    Renderer st;
    int textureIndex = 0;

    void Start()
    {
        st = gameObject.GetComponent<Renderer>();

        if(Random.Range(0f, 10f) > 9f)
        {
            st.material.SetTexture("_MainTex", altArt);
        }
        else
        {
            st.material.SetTexture("_MainTex", controlTextures[textureIndex]);
        }

        
    }

    bool pressed = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            if(!pressed)
            {
                textureIndex += 1;


                #if UNITY_STANDALONE
                    int lastTexture = controlTextures.Length;
                #else
                    int lastTexture = controlTextures.Length - 1;
                #endif

                if (textureIndex < lastTexture)
                {
                    st.material.SetTexture("_MainTex", controlTextures[textureIndex]);
                }
                else
                {
                    StartCoroutine(AsyncLoad());
                }
                pressed = true;
            }
        }
        else
        {
            pressed = false;
        }
    }


    IEnumerator AsyncLoad()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TestScene");

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
