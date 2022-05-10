#!/usr/bash

export recievedZipfile=$1
export objectName=${recievedZipfile%.zip}
export meshroom=$home/meshroom/
export recievedFiles=$home/files/recieved
export inputFiles=$home/files/input
export outputFiles=$home/files/output

# Unpack recieved zip file
mkdir "$inputFiles/$objectName"
uzip -q "$recievedFiles/$recievedZipfile" -d "$inputFiles/$objectName"

# Launch 3D processing
./$meshroom/meshroom_batch --input "$inputFiles/$objectName" --output "$outputFiles/$objectName"