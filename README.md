# Émulateur Intel 8080 & Space Invaders

Projet d'émulation du microprocesseur **Intel 8080**, avec pour objectif final de faire tourner la ROM arcade de **Space Invaders** (Taito / Midway, 1978).

Ce projet est avant tout un exercice d'apprentissage : comprendre comment fonctionne un CPU au niveau des opcodes, des registres, des drapeaux et de la mémoire, puis le prouver en faisant tourner un vrai jeu.

> **Statut :** 🚧 en cours de développement

---

## Objectifs

- [ ] Désassembleur 8080 (lecture et affichage des opcodes)
- [ ] Émulation complète du jeu d'instructions (registres, drapeaux, pile, appels, sauts)
- [ ] Validation du CPU avec une ROM de test (`cpudiag`)
- [ ] Émulation de la machine Space Invaders (mémoire, ports d'entrée/sortie, registre à décalage)
- [ ] Gestion des interruptions (deux par frame)
- [ ] Affichage vidéo (écran 1 bit, rotation de 90°)
- [ ] Entrées clavier (pièces, start, déplacement, tir)
- [ ] Son

---

## Le processeur Intel 8080

| Caractéristique | Valeur |
|---|---|
| Registres 8 bits | A, B, C, D, E, H, L |
| Paires de registres 16 bits | BC, DE, HL |
| Stack pointer | SP (16 bits) |
| Program Counter | PC (16 bits) |
| Flags | Zero (Z), Sign (S), Parity (P), Carry (CY), Auxiliary Carry (AC) |
| Espace d'adressage | 64 Ko |
| Ports d'entrée/sortie | 256 en entrée, 256 en sortie |

---

## La machine Space Invaders

Space Invaders n'est pas qu'un CPU : l'émulateur doit aussi reproduire le matériel de la borne d'arcade.

### Cartographie mémoire

| Plage | Contenu |
|---|---|
| `0x0000 – 0x1FFF` | ROM du jeu (8 Ko) |
| `0x2000 – 0x23FF` | RAM de travail (1 Ko) |
| `0x2400 – 0x3FFF` | RAM vidéo (7 Ko, 1 bit par pixel) |
| `0x4000 –` | Miroir de la RAM |

### Écran

- Résolution : **224 × 256** pixels, en 1 bit par pixel
- L'écran est monté à la verticale : la mémoire vidéo doit être **pivotée de 90°** à l'affichage
- Fréquence : 60 Hz

### Interruptions

Deux interruptions par frame, générées par le matériel :

| Moment | Instruction | Adresse |
|---|---|---|
| Milieu de l'écran | `RST 1` | `0x08` |
| Fin de l'écran (VBLANK) | `RST 2` | `0x10` |

### Ports d'entrée/sortie

| Port | Sens | Rôle |
|---|---|---|
| `IN 1` | lecture | Pièce, start, contrôles joueur 1 |
| `IN 2` | lecture | Contrôles joueur 2, options (DIP) |
| `IN 3` | lecture | Résultat du registre à décalage |
| `OUT 2` | écriture | Valeur de décalage du registre |
| `OUT 3` | écriture | Sons (1er groupe) |
| `OUT 4` | écriture | Données du registre à décalage |
| `OUT 5` | écriture | Sons (2e groupe) |
| `OUT 6` | écriture | Watchdog |

Le **shift register** est une particularité de la carte : il permet au jeu de décaler rapidement des sprites. Sans lui, l'émulation ne fonctionne pas correctement.

---

## Architecture du projet

> À adapter selon ton langage et ta structure.

```
.
├── src/
│   ├── cpu          # État du CPU et exécution des instructions
│   ├── disassembler # Désassembleur 8080
│   ├── machine      # Mémoire, ports, interruptions, registre à décalage
│   └── display      # Rendu et entrées clavier
├── roms/            # ROMs (non fournies, voir ci-dessous)
├── tests/           # Tests du CPU
└── README.md
```

---

## Tests

Le CPU est validé avant de lancer le jeu :

1. **Désassemblage** : comparer la sortie du désassembleur avec le code attendu.
2. **`cpudiag`** : ROM de diagnostic de l'Intel 8080. Un message de réussite à l'écran indique que le jeu d'instructions est correct.
3. **Space Invaders** : écran-titre, puis partie jouable.

---

## Ressources

Ce projet s'appuie principalement sur le site **Emulator 101**, qui guide pas à pas la création d'un émulateur 8080 :

- 📘 [Emulator 101](https://web.archive.org/web/20240118230905/http://www.emulator101.com/welcome.html) : tutoriel principal (désassembleur, émulation du CPU, machine Space Invaders)
- 📗 [*Intel 8080 Microcomputer Systems User's Manual*](https://altairclone.com/downloads/manuals/8080%20Programmers%20Manual.pdf) : documentation officielle du processeur
- 📙 [Table des opcodes 8080](https://www.pastraiser.com/cpu/i8080/i8080_opcodes.html)
- 📕 [Documentation](https://computerarcheology.com/Arcade/SpaceInvaders/) sur le matériel de Space Invaders

---

## Licence

Code distribué sous licence **MIT** *(à adapter)*.

Space Invaders est une marque et une œuvre protégées appartenant à leurs ayants droit. Ce projet n'est ni affilié, ni approuvé par Taito, Midway ou Intel.

---

## Crédits

- [Emulator 101](http://emulator101.com/) pour le tutoriel
- Intel pour l'architecture 8080
- Tomohiro Nishikado pour Space Invaders
