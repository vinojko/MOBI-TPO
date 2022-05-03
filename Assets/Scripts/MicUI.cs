using UnityEngine;
using UnityEngine.UI;
/* http://www.Mousawi.Dev By @AbdullaMousawi*/
public class MicUI : MonoBehaviour
{
    public Text healthText;
    public Image ringHealthBar;
    public Image[] healthPoints;

    float health, maxHealth = 1;
    float lerpSpeed;
    public float Amplifier = 3.5f;

    private void Start()
    {
        health = 1;
    }

    private void Update()
    {

        if (health > maxHealth) health = maxHealth;

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller()
    {
 
        ringHealthBar.fillAmount = Mathf.Lerp(ringHealthBar.fillAmount, MicInput.MicLoudness * Amplifier, lerpSpeed);


    }
    void ColorChanger()
    {
       
    }

    bool DisplayHealthPoint(float _health, int pointNumber)
    {
        return ((pointNumber * 10) >= _health);
    }

    public void Damage(float damagePoints)
    {
        if (health > 0)
            health -= damagePoints;
    }
    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
            health += healingPoints;
    }
}
