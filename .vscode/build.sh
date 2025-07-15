#!/bin/bash
set -e

export RimWorldVersion="$1"
CONFIGURATION="Debug"
TARGET="$HOME/.var/app/com.valvesoftware.Steam/.local/share/Steam/steamapps/common/RimWorld/Mods/TacticsModeRedux"

# build dlls
dotnet build --configuration $CONFIGURATION Source/TacticsModeRedux/TacticsModeRedux.csproj
dotnet build --configuration $CONFIGURATION Source/TacticsModeRedux.Achtung/TacticsModeRedux.Achtung.csproj

# remove mod folder
rm -rf "$TARGET"

# copy mod files
mkdir -p "$TARGET/$RimWorldVersion"
cp -r $RimWorldVersion/* "$TARGET/$RimWorldVersion/"
cp -r Common "$TARGET/Common"

# copy interop mod files
mkdir -p "$TARGET/${RimWorldVersion}_Achtung"
ls ${RimWorldVersion}_Achtung
cp -r ${RimWorldVersion}_Achtung/* "$TARGET/${RimWorldVersion}_Achtung/"

mkdir -p "$TARGET/About"
cp About/About.xml "$TARGET/About/"
cp About/Preview.png "$TARGET/About/"
cp About/ModIcon.png "$TARGET/About/"
cp About/PublishedFileId.txt "$TARGET/About/"

cp CHANGELOG.md "$TARGET/"
# cp LICENSE "$TARGET/"
# cp LICENSE.Apache-2.0 "$TARGET/"
# cp LICENSE.MIT "$TARGET/"
# cp README.md "$TARGET/"
cp LoadFolders.xml "$TARGET/"