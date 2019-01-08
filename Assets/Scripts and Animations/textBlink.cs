using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
 
public class textBlink : MonoBehaviour
{
    
    private TextMeshPro m_Text;

    void Start()
    {
        m_Text = GetComponent<TextMeshPro>();
        StartCoroutine(BlinkText());
    }
    
    public IEnumerator BlinkText()
    {
        while (true)
        {
            m_Text.text = "";
            yield return new WaitForSeconds(.5f);
            m_Text.text = "OUT OF SERVICE";
            yield return new WaitForSeconds(.5f);
        }
    }

}