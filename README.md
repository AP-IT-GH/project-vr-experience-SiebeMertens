# Escape Of Death – VR Game met ML-Agents

Escape Of Death is een VR-game geïnspireerd op Dead By Daylight, waarin een AI-agent (de "killer") de speler probeert te vinden en te vangen. De speler moet in een virtuele omgeving op zoek naar een sleutel om te ontsnappen. Deze tutorial beschrijft hoe je een eenvoudige VR-omgeving met ML-Agents opzet, hoe de agent leert, en welke resultaten je mag verwachten.

## Inleiding

In dit project combineren we VR-technologie met machine learning. Je leert hoe een AI-agent in Unity getraind wordt om een doelwit te zoeken en te vangen, en hoe de speler in VR interactie heeft met deze omgeving. Aan het einde van deze tutorial begrijp je hoe observaties, acties en beloningen samenkomen in een werkende game.

## Methoden

### Installatie

- Unity versie: 2021.3 LTS
- ML-Agents versie: 2.0.1
- XR Interaction Toolkit: 2.1.1

### Verloop van de simulatie

De game bestaat uit vier kamers. In één kamer wordt willekeurig een target geplaatst, in een andere kamer start de agent. De agent gebruikt raycasts om het doelwit te detecteren, rekening houdend met muren en obstakels. De speler navigeert in VR en zoekt naar de ontsnappingssleutel.

### Observaties, acties en beloningen

- **Observaties:** Posities van muren, targets, agent en speler via raycasts.
- **Acties:** Bewegen, draaien, target benaderen.
- **Beloningen:** +1 bij het vangen van het doelwit, -0.01 per stap zonder vangst.

### Objecten en gedragingen

- **Agent (killer):** Zoekt actief naar het doelwit, navigeert rond obstakels.
- **Target:** Staat stil of beweegt beperkt.
- **Speler:** Navigeert in VR, zoekt sleutel.
- **Sleutel:** Ontgrendelt de uitgang.

### One-pager (afwijkingen?)



## Resultaten

### Training met Tensorboard

![Tensorboard Fase1 (episode length and cumulative reward)](Assets/documentatie/image-1.png)

De Tensorboard-grafieken tonen een duidelijke stijging in cumulatieve beloning en een daling in episode-lengte naarmate de agent leert. Alle relevante data zijn zichtbaar, zonder smoothing.

### Opvallende waarnemingen

Tijdens het trainen bleek de agent snel te leren navigeren rond muren. Soms bleef de agent echter hangen bij complexe obstakels, wat duidt op mogelijke verbeterpunten in de omgeving.

## Conclusie

We hebben een VR-game ontwikkeld waarin een AI-agent succesvol leert een doelwit te vinden en te vangen. De resultaten tonen aan dat de agent zijn taak efficiënt uitvoert en de speler uitdaagt. De combinatie van VR en reinforcement learning biedt een boeiende leeromgeving. In de toekomst kan de omgeving complexer gemaakt worden, bijvoorbeeld door bewegende targets of meerdere agenten toe te voegen.
