#Tool for process spreadsheet output into items
import os, sys
#Changes the current directory to the directory this python script is in
os.chdir(sys.path[0])


file = open("./Item Creator - Sheet1.tsv", "r")
line = file.readline()
whichEffects = "base"
baseEffects = ""
lvlEffects = ""

while line != "":
    line = line.strip("\n")
    line = line.split('\t')
    if (line[0] == "Title"):
        title = line[1]
    elif (line[0] == "Name"):
        name = line[1]
    elif (line[0] == "Type"):
        typ = line[1]
    elif (line[0] == "Image"):
        image = line[1]
    elif (line[0] == "Damage"):
        if (whichEffects == "base"):
            baseEffects += "Damage " + line[1] + "\n"
        else:
            lvlEffects += "Damage " + line[1] + "\n"
    elif (line[0] == "Duration"):
        if (whichEffects == "base"):
            baseEffects += "Duration " + line[1] + "\n"
        else:
            lvlEffects += "Duration " + line[1] + "\n"
    elif (line[0] == "Defense"):
        if (whichEffects == "base"):
            baseEffects += "Defense " + line[1] + "\n"
        else:
            lvlEffects += "Defense " + line[1] + "\n"
    elif (line[0] == "Uses"):
        uses = line[1]
    elif (line[0] == "Max Level"):
        mxlvl = line[1]
    elif (line[0] == "Level Up Effects:"):
        whichEffects = "Level up"
    elif (line[0] == "Activator"):
        activ = line[1]
    line = file.readline()
print(f"{title}\n{name}\n{typ}\n{image}\n{baseEffects}{mxlvl}\n{lvlEffects}")
if (typ == "Relic"):
    print(activ)
if (typ == "Item"):
    print(uses)