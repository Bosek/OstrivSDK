# **Unofficial OstrivSDK**
This repo contains two projects - OstrivSDK library and OstrivConverter binary.

## **OstrivSDK**
Right now lacks in-code docs, but the API is pretty simple and self-explanatory. You can load `data.data`, `.minimap` and `.level_save` files, export them, import them and/or save them. This should make it possible to implement some basic level editor for example.

## **OstrivConverter**
This is a CLI tool that uses SDK to do all above mentioned things without needing to code.  

`ostrivconverter --help`  
### Examples:  
`ostrivconverter data --export -s data.data -d ostrivdata`  
to export data from `data.data` file into `ostrivdata` folder  

`ostrivconverter data --import -s ostrivdata -d data.data`  
to import data from `ostrivdata` folder to `data.data` file  

`ostrivconverter levelsave --export -s level01.level_save -d level01.yaml`  
to export data from `level01.level_save` file to `level01.yaml` file  

`ostrivconverter levelsave --import -s level01.yaml -d level01.level_save`  
to import data from `level01.yaml` file to `level01.level_data` file  

`ostrivconverter minimap --export -s level01.minimap -d level01.bmp`  
to export data from `level01.minimap` file to `level01.bmp`  

`ostrivconverter minimap --import -s level01.bmp -d level01.minimap`  
to import data from `level01.bmp` file to `level01.minimap` (bmp file has to be 1024x1024px)  