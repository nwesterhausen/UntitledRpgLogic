#!/bin/bash

# Create destination directories if they don't exist
mkdir -p wwwroot/css/bootstrap
mkdir -p wwwroot/js

# Run bun to install dependencies
bun install

# Copy Bootstrap files
cp node_modules/bootstrap/dist/css/bootstrap.min.css wwwroot/css/bootstrap/
cp node_modules/bootstrap/dist/css/bootstrap.min.css.map wwwroot/css/bootstrap/
cp node_modules/bootstrap/dist/js/bootstrap.bundle.min.js wwwroot/js/
cp node_modules/bootstrap/dist/js/bootstrap.bundle.min.js.map wwwroot/js/

# Copy Popper.js file
cp node_modules/@popperjs/core/dist/umd/popper.min.js wwwroot/js/
cp node_modules/@popperjs/core/dist/umd/popper.min.js.map wwwroot/js/

echo "Successfully copied assets to wwwroot."

