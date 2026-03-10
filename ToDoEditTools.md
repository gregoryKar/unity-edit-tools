- TODO - PROBLEMS - THOUGHTS

[https://assetstore.unity.com/packages/tools/utilities/quantum-console-211046]
    <!--> το πακετο του αλλου για commands 

> οτι τελειωει το βαζεις σε done για να τα εχεις παρουσιασιμα 
-   <!--> η βαζεις και ενα ai να σου κανει περιγραφη δυνατοτητες ...
    


- shortcuts ???





> MAIN 
- scene change issues ??
- refrash command η και ui κουμπι για να παρει τα νεα [must]
    <!-->ισως και επιλογη για refrash interval ακριβο ..
    στα settings αυτο
    του το λες και Min πχ 1 sec 
    πχ εχεις φτιαξει νεα αντικειμεντα με attributes
    που για οικονομια τα κοιταει μονο στην αρχη,,,
- REMOVE POSSIBLITY OF EDITOR CALL <later test>
    <!--> use #if , no new class .. only stass.create 
    all pass the if not editor return function test
- example showcase script [must]
    <!-->για τα πακετα σου MONO που το κοτσαρεις στη σκηνη 
    ανοιγεις και βλεπεις τι κανει για να μαθεις να το δουλευεις 
    maybe spawn it by default in package download

> LATER
- physiacal world debugg <poly kalo>
    <!-- >με offset και επιλογη να κοιταει τον παικτη 
    (αυτο το transform)
    abstract base και παιδι για ui και μη 


> DYNAMIC DEBUG     
- DYnamic debug stuff NOTES
    <!-->just text no back
    not only show this for now ..
    no input 
    buttons later...IF ever
    nickname : apear name 
        deafult = code
    extra info no special collor ..
    color ?? affect only tittle ?? YES
    - all use nickname 
    is code by default unless changed

> CONSOSE 
- MALAKA MONOBEHAVIOUR ATTRIBUTE -> command prefix 
    <!-->βαζει πανω απο το mono attribute command prefix 
    και απλα μετα μεσα εχεις εντολη που επειδη και μονο ειναι μεσα σε αυτο
    παει στην κατηγορια του
    πχ στον παικτη απο πανω στο mono εχεις player  [MonoCategoryAttribute(nickanme : "player")]
    μετα στο function shoot [functionAttribute("nickname:"shoot")] !!!!!
    αν δεν εχει category ??
- ADDITIONAL [must]
    <!-->
    Builder Patten Condition(a).Condition(b).Once().Register()...
    Condition
    Once
    SetFaverite()
    SetUseNullParams -> cast even without input 
        it will handle it ..
    Register ??





- να βλεπεις τις τελευαιτες εντολες που εκανες [must]
    <!-- >οπως σε terminals 
    και πχ με alt βελακια κανεις navigate σε αυτες

- Autocomplete [must]
    <!-->γκρι απο κατω τι προτεινει και tab do >> 

- parameter functions [must]
    <!-- like with bool , string , int ktl ,,,

- ΕΙΔΙΚΑ SUGGESTIONS [must]
    <!-- πχ γραφεις εντολη για tag η εντολη να κανει signal 
    οτι τωρα περιμενεις tag.. οποτε κοιτας για tag για πιθανες
    λεξεις να προτεινει ...
    οτι τωρα περιμενεις true false 
    και πολαπλες προτασεις κατω .. οπως στην ααρχη μεχρι γραψεις 
    εντολη σου λεει εντολες μετα οι προτασεις ειναι για το επομενο
    πχ πιθανα tags / true false , νουμερα ..
        σηαμεινει συστημα που δινεις suggestion σε κατηγορια
    πχ boolean -> true false 
    tag -> οποτε φτιαχνεις νεο δωστου
    και απο command creation specify parameterTypeOption
    SetFirstParameterType(Type.boolean)
    SetFirstParameterType(Type.tag)
    SetFirstParameterType(Type.number)
    και να μην εχει suggestions ισως το αξιοποιει για να κανει 
    το χρωμα... πχ οτι κανεις λαθος ..


- OTHER STUFF [nice]
    <!-->print tags or command and others in this terminal 
    not printing like in unity debug , maybe both .. plus in unity 
    different lines .. vissible well
   
- TAG COMMANDS [must]
    <!-- > show only this tag / enable all disable all
   


> LATER 
- settings default interval for variables
- settings color pallete <functionality ready>
    <!--> your colors + 3 custom for user
    then in debug variable enum uses these ..
    vale min transparency 
    fro enum.red paei kai to parnei ..
   
    option to reset them all
    separate from general reset
- later
    <!--> icons later ..πχ warning , start 
    , person , happy , angry , simple 
    color box 
    - ADD dynamic icon απο πρασινο το κανει κοκκινο 
    απο τιποτα το κανει warning

    maybe 1 line name and icon
    and then another text for the info easier to have smaller size and offset to 
    left or right  ,, cause the template you had with icon and [ ] you like but in one 
    line [ ] icon + stuff too much , and not good seperation 

    the toggle change color from the color palete not full red/green

    extra info show tag .. βθ

    - ADD TAP ACTION 
    <!-- οταν πατας ποιο ομως ?? ισως το icon 
    και να εχει και outline ... 

    - δυνατοτητα αλλαξει το μαυρο backround
    - settings option for font size 

> CAN LIVE WITHOUT
- panel for actions [good-but-work...]
    <!--> more user friendly instead of console thing just 
    a unity window with buttons with the action name..
    ability to add favourites , and show only favourites
    like if you have 50 you must find a way to manage them
    like now i see these 7 ...
    ισως ενα panel με κουμπι για mode και πατας και βγαινουν τα
    κουμπια .. ισως ακομα το text απλα πιο κατω 
    ισως απλα να χωρανε και τα δυο ετσι απλα ...
- errors καπου ρυθμιση - OPTIONAL -
    <!-->για το αν θα ειναι errors / warnings / suppress
- range from Transform parameter <kalo but not very easy>
    <!-->  shortcuts - debugg item
    practicaly is another GetEnabled .. 
    hot to combine
    πχ για να φαινεται debug / shortcut to work 
    hmm intresting !!! , ισως απλα δυο transforms 
    που απλα κοιτας αποσταση και πχ βαζει παικτη 
    και αντικειμενο η οτι αλλο θελει.. 
- στις ρυθμισεις error handling [later]
    <!-->επιλογη για error warning Η ignore για τα editor calls
    η απλα στο editor ignore και αντε κανενα warning 
    αντε βαλε option supress
    μεγεθος και αλλα settings δημιουργησε στα settings
- οργανωσε και το read me ...
- GOOGLE FONTS
- UI REFERENCES 
- panel με κουμπια για τα εργαλεια σου .. 
    <!-->πατας ανοιγει το πανελο και πατας για να ανοιξουν η κλεισουν
    τα αλλα , να ειναι μικρο οποτε σε παιρνει μονιμως ανοιχτο οσο παιζεις ..
    κουμπια console , debugger , granazi ρυθμισεις 



> SOMETHING FISHY _______ ????
- ENCORPORATE MOUSE INPUT !!!
    <!--> like how ?? 
- make snippets for dynamic debugger say 
    <!-->.  snippets start to seem less usefull
    since you create through do().do()...
    no huge class with 17 default parameters

    new class with all . shortcut . action . 
    plus snipets... can you add snipets to a packkage 
    also snipets for shortcut .. 
- thought of nothing gameobject just draw... 


> DONE
- if not tag parameters given still has tag : the defalt tag ...
- debug items listen for tag change
- to work with both input systems .....
- in case of making debug with same code what do you do
- WATCH VARIABLE ATTRIBUTE
- add the collapse shortcut .. amazing .. 
    <!-->already both hierarch and inspector
    TAB SWITCHING DONE ALSO !!!
- debugger open settings 
- edit tools if no canvas just make one... [must]
- να προσαρμωζεται στην αλλαγη οθονης .. [must]
- too many items dynamic debug handling 
- all moved to dont destroy on load ??
- φερε και το απλο για hierarchy color και icon ...
    <!-->το icon αν δουλευει ..
- command settings που το παει settings [simple-must]
    <!-->παραθυρο αλλιως και κουμπι με γραναζι ..