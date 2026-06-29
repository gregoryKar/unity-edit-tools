 




- intro = speaking
- debugs = show 
- debugs code
- console commands
- console commands code 
- shortcuts tool

- shortcuts unity didnt have
- highlight gameObject for vissibility


- hallo , i am showcasing my custom unity edit tools . they are configured 
as a unity package, so that i can easily donwload it from the package manager through git url .
It works right out of the box in any new project , with no need for special configuration

# dynamic debug

<toggle on off , size change >
- for start i have a tool to help me debug values , without writing a 1000 log messages
and clatering the console.
i can easily open and close it through a custom shortcut of my choice. And it doesnt need
a scene reference, it initialises itself on load.

<show settings>
i have a custom unity menu with all the tools settings

<show toggle extra variables>
- here i can see the variables im tracking, with options to see extra information, like 
 last update time and frame,  and how many times it has been updated in total. lets see 
how to use this from our code

- open C#
- read text to say for debugs from there
1. normals 
2. dynamic debugs


# console commands

<toggle console on off, function , multi param , navigate up down , clc>
then we have the cheat console to test functions ,
again i can toggle it on and off with shortcuts and it initialises itself on load.
it produces suggestions based on text similarity with registered functions .
i can navigate to previous commands or to the suggested ones with the arrow keys.
it supports functions with multiple parameters as well and it
   shows what parameters the function needs
lets see how to use this as well

- open C#
- read text to say for console from there

# custom shortcuts

- another tool in the pack allows me to assign any function with a custom shortcut
for easy and fast testing.

- i havent configured this one to be used with an attribute yet, so i wire it up with
a few lines of code like this.
and just like that i can now execute the function by pressing the shortcut

# editor shortcuts unity didnt have

- in the pack i also have a few editor shortcuts unity ddidnt have by default
- toggle gizmos on and of in the scene and game view

- change tabs in a group that i have assigned to the mouse side buttos for fast navigation

- hierarchy collapse and expand . either all or only selected

# Styled Hierarchy

- and finaly a monobehaviour to change the visual apearance of hierarchy 
items for better vissibility ,
with controll over the icon , backround color and text color 
- you can also add this in runtime to mark a gameobject. for example i do 
this to mark items that an error occured into
to make debuging easier
- i just mark an item in runtume from code like this 

#