#Tool to create events

stats = ["Health", "Gold"]

title = input("Internal title for event (not displayed name): ")
name = input("Name of event: ")
flavor = input("Flavor text of event: ")
scene = input("Scene name: ")
camera = input ("Scene Index: ")
minigame = ""
while minigame != "y" and minigame != "n":
    minigame = input("Is this a minigame? (Y/N): ").lower()

if minigame == "y":
    nameOfGame = input("Title of minigame to be displayed: ")
    print(f"{title}\n{name}\n{flavor}\n{scene}\n{camera}\nTrue\n{nameOfGame}")
    exit()

numbChoice = int(input("How many choices will the event have?: "))
choices = []
for i in range (0, numbChoice):
    Ctext = input("Text of the choice: ")
    Ceffects = ""
    for stat in stats:
        Ceffect = input("How much " + stat + " will the event give?: ")
        if stat != stats[0]:
            Ceffects += "\n" 
        Ceffects += stat + " " + Ceffect
    numbItems = int(input("How many items will this choice give?: "))
    Citems = ""
    for y in range(0, numbItems):
        Citems += input("Title of item to be given: ")
        if (y < numbItems - 1):
            Citems += "\n"
    numbEvents = int(input("How many events will this choice add to the pool: "))
    Cevents = ""
    for y in range(0, numbEvents):
        Cevents += input("Title of event to add: ")
        if (y < numbEvents - 1):
            Cevents += "\n"
    choices.append([Ctext, Ceffects, numbItems, Citems, numbEvents, Cevents])
print(f"{title}\n{name}\n{flavor}\n{scene}\n{camera}\nFalse\n{numbChoice}")
for choice in choices:
    for element in choice:
        if (element != ""):
            print(element, end="")
            if (choice != choice[len(choice) - 1]):
                print("")