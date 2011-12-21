#!/bin/sh
# Script used to build the website, it performs the following tasks :
#      - Generate HTML web pages from the PHP pages
#      - Optimize the different *.png files used for the website
#	   - Optimize the CSS used by the website

if [ -d "./output" ]
then
  echo Output directory already exists
  read -p "    => Remove previous content generated (y/n)?"
  if [ "$REPLY" == "y" ]
  then 
    rm -f -r ./output/
    mkdir ./output
  fi
else
  mkdir ./output
fi

# Generation of HTML files from the PHP
echo Generating HTML files
php -f ./input/index.php > ./output/index.html
php -f ./input/about.php > ./output/about.html
php -f ./input/npgdump.php > ./output/npgdump.html
php -f ./input/npgrestore.php > ./output/npgrestore.html
php -f ./input/products.php > ./output/products.html
php -f ./input/project.php > ./output/project.html
php -f ./input/roadmap.php > ./output/roadmap.html
php -f ./input/versioning.php > ./output/versioning.html
 
# Picture management
mkdir ./output/images
if [ "$1" = "--NoImgOpt" ]
then
    echo Copy Image files
    cp --force --recursive ./input/images ./output
else        
    echo Optimizing PNG files
    ./tools/optipng/optipng.exe -quiet -o7 -dir ./output/images ./input/images/*.png
    echo Optimizing GIF files
    ./tools/optipng/optipng.exe -quiet -o7 -dir ./output/images ./input/images/*.gif
fi

# Javascript libraries management
echo Copy Javascript libraries
cp --force --recursive ./input/js ./output/js

# CSS copy
echo Copy CSS files
./tools/ajaxminifier/AjaxMin.exe ./input/style/style.css -out ./output/style/style.css

echo ===== Website builded =====
echo 

# Put online
read -p "    => Put the website generated online (y/n)?"
  if [ "$REPLY" == "y" ]
  then
    echo Removing previous HTML files
    ssh gildas@pgfoundry.org "rm /home/pgfoundry.org/groups/npgtools/htdocs/*.*"
    echo Removing previous images directory
    ssh gildas@pgfoundry.org "rm -rf /home/pgfoundry.org/groups/npgtools/htdocs/images/"
    echo Removing previous javascript directory
    ssh gildas@pgfoundry.org "rm -rf /home/pgfoundry.org/groups/npgtools/htdocs/js/"
    echo Removing previous style directory
    ssh gildas@pgfoundry.org "rm -rf /home/pgfoundry.org/groups/npgtools/htdocs/style/"  
    echo Sending website
    scp -r ./output/ gildas@pgfoundry.org:/home/pgfoundry.org/groups/npgtools/htdocs/.  
    echo ===== Website sent online =====
  fi