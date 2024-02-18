Projet de l'équipe CUBI2

# Git Lock
## Vérifier d'abord si vous avez LFS sur votre ordinateur (en cmd-line): 
``git-lfs --version``

## On peut utiliser la commande en ligne de commande: 
``git lfs lock path/to/nom_de_la_scene.unity`` en remplaçant le chemin et le nom par le bon chemin et le bon nom.

## Il ne faut pas oublier de le unlock lorsqu'on a fini avec, car un autre utilisateur ne pourra pas le faire
La commande pour unlock est:  ``git lfs unlock path/to/file.png``

## Pour voir les fichiers lock:
``git lfs locks``

## git lfs mac
```bash
brew install git-lfs
git lfs install
git lfs pull
git lfs track "*.unity"
git add .gitattributes
git pull origin dev
```