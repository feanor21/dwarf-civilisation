# dwarf-civilisation
A game inspired by dwarf fortress on unity

Devellop a game on unity, inspired by dwarf forteress.

En Français pour le moment.

Description des classes :

La classe GameController gére le jeu. En particulier, elle gére les interactions entre objets. 
Les actions faites par les autres objets sont appelées depuis la classe GameController.

Le GameController contient des fonctions macro. Il gére les interactions ave la souris, et garde le compte des objets sur le terrain.

La classe MouseFollower.

MouseFollower est le pointeur de la souris. C'est un objet qui suit le pointeur de la souris, et permet de reconnaitre les collisions avec les autres objets.
Elle permet aussi de reconnaitre les click.

La classe ClickerMover

Clickermover implémente les fonctions permettant de déplacer un objet à la souris. il récupére l'emplacement originel de l'objet, l'emplacement cible, et il déplace l'objet.

La classe DwarfController

DwarfController définit le comportement du nain. Il implément pour le moment:
*manger
*dormir
*la possibilité d'avoir un métier (uniquement fermier pour le moment)
*la mort du nain


eatscript et sleepscript sont appelés lorsque le nain va manger ou dormir.


La classe FarmController définit le comportement de la ferme. Pour le moment, lorsqu'un nain est dans la ferme, il devient fermier et produit de la nourriture.


La classe Skills

La classe SKills défini le focntionnement des skills des personnages. Contient des méthodes permettant de faire gaggner de l'expérience au personnage, et de faire levelup les compétences.

Ajout d'un fichier .gitignore : éditez-le pour ignorer certains fichiers lors du git push

Known issues :
- Problèmes avec la gestions de la souris et du sol lorsque que la caméra est à une hauteur y différente de 50







