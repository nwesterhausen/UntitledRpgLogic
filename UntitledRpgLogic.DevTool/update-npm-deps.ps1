# Create destination directories if they don't exist
New-Item -ItemType Directory -Force -Path "wwwroot/css/bootstrap"
New-Item -ItemType Directory -Force -Path "wwwroot/js"

# Run bun to install dependencies
bun install

# Copy Bootstrap files
Copy-Item -Path "node_modules/bootstrap/dist/css/bootstrap.min.css" -Destination "wwwroot/css/bootstrap/"
Copy-Item -Path "node_modules/bootstrap/dist/css/bootstrap.min.css.map" -Destination "wwwroot/css/bootstrap/"
Copy-Item -Path "node_modules/bootstrap/dist/js/bootstrap.bundle.min.js" -Destination "wwwroot/js/"
Copy-Item -Path "node_modules/bootstrap/dist/js/bootstrap.bundle.min.js.map" -Destination "wwwroot/js/"

# Copy Popper.js file
Copy-Item -Path "node_modules/@popperjs/core/dist/umd/popper.min.js" -Destination "wwwroot/js/"
Copy-Item -Path "node_modules/@popperjs/core/dist/umd/popper.min.js.map" -Destination "wwwroot/js/"

Write-Host "Successfully copied assets to wwwroot."

