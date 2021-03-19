# Unity-PCTO
# IT:
Un gioco fatto con Unity per il PCTO
I controlli sono le freccie sinistra e destra ('A' e 'D' non sono assegnati).
Il gioco è ancora in sviluppo, quindi aspettatevi bug.

Il gioco è bidimensionale a scorrimento laterale
e vede come protagonista un elefantino, alla guida di un veicolo,
che deve affrontare le tortuosità del terreno.

Il terreno è teoricamente e praticamente illimitato
venendo generato proceduralmente e randomicamente.
Tuttavia, anche se il terreno è illimitato, il gioco non lo è.
C'è una limitazione di Unity relativa alle coordinate
che accade quando si percorre una grande distanza.
Unity inizia a riservare più memoria alla
parte intera del double, lasciando meno 
spazio alla parte decimale.
(Almeno questo è quello che ho capito)
Questo risulta in diversi tipi di problemi
nella parte fisica del veicolo, rendendo
quindi il gioco ingiocabile a lungo andare.
In ogni caso, si parla di distanze stratosfericamente alte.

# EN:
A game made with Unity for a school project.
Controls are the left and right arrow keys ('A' and 'D' aren't binded).
The game is still in development, so expect bugs.

The game is 2D and side-scrolling
and it has an elephant driving a vehicle as a protagonist.
This elephant has to overcome the windingness of the terrain.

The terrain is theoretically and practically unlimited
since it's being generated procedurally and randomly.
Though, even if the terrain is unlimited, the game isn't.
There's a Unity coordinate limitation
that occurs when a lot of distance is traveled.
Unity starts to reserve more memory for
the intger part of the double, 
leaving less space for the decimal part.
(At least that's what I understood)
This results in all kinds of problems
in the physics part of the vehicle,
making the game unplayable on the long run.
Anyway, we are talking about stratospherically high distances.
