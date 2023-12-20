#!/bin/bash

mkdir -p output

dotnet run --project Dot2DotGenerator/Dot2DotGenerator.csproj \
  --input sample-data/bee-dots-01-B.json \
  --output output/dots-01-B.svg

svg2png --width 600 output/dots-01-B.svg output/dots-01-B.png

dotnet run --project Dot2DotGenerator/Dot2DotGenerator.csproj \
  --input sample-data/bee-dots-02-E.json \
  --output output/dots-02-E.svg

svg2png --width 600 output/dots-02-E.svg output/dots-02-E.png

dotnet run --project Dot2DotGenerator/Dot2DotGenerator.csproj \
  --input sample-data/bee-dots-03-E.json \
  --output output/dots-03-E.svg

svg2png --width 600 output/dots-03-E.svg output/dots-03-E.png
