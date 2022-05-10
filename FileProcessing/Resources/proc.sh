#!/bin/bash

objectName="${1%.zip}"
meshroom="$HOME/meshroom/"
recievedFiles="$HOME/files/recieved/$1"
inputFiles="$HOME/files/input/$objectName"
outputFiles="$HOME/files/output/$objectName"

# Unpack recieved zip file
if [ -d "$inputFiles" ]
then
    rm -rf $inputFiles
fi

mkdir $inputFiles
unzip -q $recievedFiles -d $inputFiles

# Launch 3D processing
$meshroom/meshroom_batch --input $inputFiles --output $outputFiles