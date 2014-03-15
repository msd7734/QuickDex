QuickDex
========
A lightweight desktop Pokedex application in .NET

About
=====
The goal of this project is to provide an application that allows a quick lookup of Pokemon data from various sources. Enter the number or name of a Pokemon and instantly view locally stored data or get linked to a third party site of your choosing, such as Serebii or Bulbapedia. This application consumes the public PokeAPI, found here: http://pokeapi.co/

State of Implementation
=======================
This project is currently in a beta releaase. Basic functionality is fully implemented. This includes:
 - All UI behavior (this is a System Tray application)
 - Ability to search Pokemon by both name and national dex number on Bulbapedia and Serebii

Planned features include:
 - Search Pokemon abilities, moves, types, etc.
 - Search and spelling suggestions
 - Customized settings (hotkeys and functionality)
 - Search locally stored Pokemon information

FAQ
===
Q: How do I do a search?

A: Find the Pokeball icon in your System Tray, right click, select "Show". Or, use the shortcut Windows Key+Q at any time. Note to Windows 8 users: WIN+Q opens the Metro search. QuickDex will override this functionality while it's running. Custom hotkeys are a planned feature which will fix this limitation. 


Q: I found a bug, what do I do?

A: Report it on Github and I'll look into it.


Q: Does this support name searches using the Japanese/French/Spanish/etc. Pokemon names?

A: Currently QuickDex only supports searching via English names. National Dex #'s, however, are universal and can also be used to search.


Q: How do I search for different Nidoran genders by name?

A: That's a very specific question, but I'm glad you asked! Nidoran(f) and Nidoran(m), as well as using the Unicode Male/Female symbols will work.


Q: Is this program safe? Will it give me a virus/hack my computer/steal my bank account number/release my shiny Celebi/etc?

A: It won't do any of that! It's completely safe. But don't take my word for it, Github has all the source code for your perusal.


Q: This is useless. Why wouldn't someone just go to Bulbapedia or Serebii or somewhere else and search on the site directly?

A: True, you could do that. But you have to open up a browser and navigate there yourself. Serebii in particular has lots of extensive, well-formatted information, but it has poor navigation. Wouldn't it be nice to just hit WIN+Q inside your emulator or on your desktop, type in a name , hit enter and get taken straight to the page you want to see? QuickDex allows you an efficient workflow for all your Pokemon searching needs. In addition, in the future you'll be able to query a local cache of Pokemon info without needing an internet connection.


Download
========
You can build the source inside Visual Studio, or you can download the .zip file of the beta 1.0 release here: http://www.mediafire.com/download/5k65vc88b4dh78i/QuickDex_b1.0.zip
