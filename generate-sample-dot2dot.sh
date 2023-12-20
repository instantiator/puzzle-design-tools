#!/bin/bash

dotnet run --project Dot2DotGenerator/Dot2DotGenerator.csproj \
  --input sample-data/bee-dots.json \
  --output output/dots.svg
