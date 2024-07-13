#speaker AlterMann
VAR completable_FillVase = false
VAR completed_FillVase = false
VAR active_FillVase = false
VAR completable_AlterMannTalk = false
VAR completed_AlterMannTalk = false
VAR active_AlterMannTalk = false

Und bist du schon fertig?
* {completable_FillVase} -> foundVase 
* {not completed_AlterMannTalk} -> choices

== choices ==
#addQuest AlterMannTalk
Hey Matthew, du siehst so nachdenklich aus.

* Ich suche grade Raha, hast du sie gesehen?
-> What
* Nicht wirklich, Mutter hat gemeckert wegen Raha.
-> Where

== What ==
Das Wieseljunge? Ich hab sie panisch davonflitzen sehen. Hat sie Ärger gemacht?

* Wohl einen Wasserkrug umgeworfen oder so. Weißt du in welche Richtung sie floh?
-> Location

== Where ==
Haustiere halten ist nicht einfach. Ich hatte selbst früher mal eine Eidechse namens Ganha. Ich durfte damals keine Haustiere halten also hab ich sie heimlich übers Fenster zu mir geholt, wenn Vater jagen und Mutter kochen oder Wäsche machen war. Ich vermiss es manchmal ein Haustier zu haben.
* Warum holst du dir nicht wieder eines?
-> Gespraech

== Location ==
Nein. Aber ich vermute, dass sie sich in Richtung der Wälder aufmachte.
-> Gefallen

== Gespraech ==
Ich bin nicht mehr der Fitteste. Ich glaube Arbeit mit einem Haustier würd mich zu sehr anstrengen und das möcht ich dem Tier nicht antun.
-> Gefallen

== Gefallen ==
Aber sag mir Matthew, könntest du mir einen Gefallen tun während du nach dem Wieseljungen suchst?

* Was gibt's denn?
Kannst du mir einen Krug Wasser am naheliegenden Fluss auffüllen und zu mir bringen bei Gelegenheit? Das würde meine alten Knochen etwas schonen. Nimm dir einfach einen der Krüge, die dort stehen.
-> Help

* Tut mir leid. Ich hab noch was vor.
-> END

== Help ==
#addQuest FillVase
#completeQuest AlterMannTalk
* Natürlich!
Ahh, vielen Dank. Du bist eine große Hilfe.
-> END

== foundVase ==
Dankeschön.
* Kein Problem. Hab ich doch gern gemacht.
#completeQuest FillVase
-> END