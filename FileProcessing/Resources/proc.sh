#!/bin/bash

objectName="${1%.zip}"
meshroom="$HOME/meshroom/"
recievedFiles="$HOME/files/recieved/$1"
inputFiles="$HOME/files/input/"
outputFiles="$HOME/files/output/$1"

echo $objectName
echo $meshroom
echo $recievedFiles
echo $inputFiles
echo $outputFiles

# Unpack recieved zip file
if [ -d "$inputFiles" ]
then
    rm -rf $inputFiles
fi

mkdir $inputFiles
unzip -q $recievedFiles -d $inputFiles

# Launch 3D processing
$meshroom/meshroom_batch --input "$inputFiles$objectName" --output $outputFiles