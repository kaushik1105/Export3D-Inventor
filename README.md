# Export3D-Inventor
A demo C# add-in for Autodesk Inventor to show the file structure 
This a demo add-in for Autodesk Inventor. Its functionality is to check a file if it is an assembly or part.

The idea of this add-in is to know the structure of add-in. The template used here is Autodesk Inventor C# AddIn.

How it's done:

1) Used Visual Studio Professional 2022 (64-bit)

2) Create a new Project In Visual Studio 

3) Select "Autodesk Inventor C# AddIn" and click Create. (To get the "Autodesk Inventor C# AddIn" Template,go to https://github.com/InventorCode/inventor-addin-templates

4) Go to Project menu and select the properties, In Application Tab select the Target Framework as .NET Framework 4.8.

5) In Debug tab, select start external Program and set file location of Inventor.exe in your computer.

6) Add the references as per the Github Repsitories.

7) Create the File and Folder structure as per the Export3D-Inventor Package.

8) Built and Start Debug.

9) The add-in will be added in Tools section and will be visible when a .asm file will be openned.
