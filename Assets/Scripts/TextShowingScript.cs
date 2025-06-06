using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class TurnBack : MonoBehaviour
{
    [Header("Toggle for the GUI on/off")]
    public bool GuiOn = false; // GUI is standaard uitgeschakeld

    [Header("The text to display on trigger")]
    [Tooltip("To edit the look of the text, go to Assets > Create > GUISkin.")]
    public string Text = "Turn Back";

    [Tooltip("Window box size, adjusted in pixels.")]
    public Rect BoxSize = new Rect(0, 0, 200, 100);

    [Tooltip("Custom GUISkin for styling the text.")]
    public GUISkin customSkin;

    private void Start()
    {
        GuiOn = false; // Zorg dat de GUI altijd uitstaat bij het begin
    }

    // Activeer GUI wanneer de speler dichtbij komt
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Controleer of het de speler is
        {
            GuiOn = true; // Zet de GUI aan
            StartCoroutine(HideTextAfterDelay(2f)); // Verberg de GUI na 2 seconden
        }
    }

    // Coroutine om de GUI na een bepaalde tijd uit te schakelen
    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GuiOn = false; // Zet de GUI uit
    }

    // Display the GUI
    private void OnGUI()
    {
        if (GuiOn)
        {
            if (customSkin != null)
            {
                GUI.skin = customSkin;
            }

            // Center the GUI op het scherm
            GUI.BeginGroup(new Rect((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));
            GUI.Label(BoxSize, Text);
            GUI.EndGroup();
        }
    }
}
