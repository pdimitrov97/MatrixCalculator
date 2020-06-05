# Matrix Calculator
This is a simple matrix calculator Windows Forms Application in C#

## Description
The calculator can perform the following:
- Calculate transpose matrix
- Calculate determinant of a matrix
- Calculate inverse matrix

The calculator works with square matrices of minimum size <b>2 X 2</b> up to a specified size.
There is a constant 

```
private const int MAX_MATRIX_TYPE = 10;
```

in <b>CalculatorForm.cs</b> which specifies the maximum size of a square matrix that can be selected. Modifying this number will allow you to increase or decrease the number of options for matrix sizes if you need so.

## How to use it
The program has a GUI.  When you first launch the program you will see this screen:

<img src="https://user-images.githubusercontent.com/15669909/83851383-f3d48180-a71a-11ea-8cff-d85c1ebe23ed.png">

You are then prompted to select the matrix size:

<img src="https://user-images.githubusercontent.com/15669909/83851443-09e24200-a71b-11ea-8d81-7f48693823ea.png">

After selecting the matrix size you are prompted to select what you want to calculate:

<img src="https://user-images.githubusercontent.com/15669909/83851490-1cf51200-a71b-11ea-8611-ff0ba852c732.png">

After selecting what you are going to calculate you have to enter the matrix elements on the left side and press the calculate button to get the result:

<img src="https://user-images.githubusercontent.com/15669909/83851699-6ba2ac00-a71b-11ea-97d9-65fcdd27e1b3.png">

## Requirements:
- .NET Framework 4.5 or greater
