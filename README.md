# CyberMath

<p align="center">
  <img src="https://raw.githubusercontent.com/Jeffeek/CyberMath/master/cyberMath.png" alt="CyberMath Logo" width="200"/>
</p>

<h3 align="center">A .NET library with a collection of useful data structures and extension methods, designed to simplify common mathematical and programming tasks.</h3>

<p align="center">
  <a href="https://github.com/Jeffeek/CyberMath/actions/workflows/dotnet.yml"><img src="https://img.shields.io/github/workflow/status/Jeffeek/CyberMath/.NET?style=for-the-badge&logo=github" alt="Build Status"></a>
  <a href="https://www.nuget.org/packages/CyberMath"><img src="https://img.shields.io/nuget/v/CyberMath?style=for-the-badge&logo=nuget" alt="NuGet Version"></a>
  <a href="https://www.nuget.org/packages/CyberMath"><img src="https://img.shields.io/nuget/dt/CyberMath?style=for-the-badge&logo=nuget" alt="NuGet Downloads"></a>
  <a href="https://github.com/Jeffeek/CyberMath/stargazers"><img src="https://img.shields.io/github/stars/Jeffeek/CyberMath?style=for-the-badge&logo=github" alt="GitHub Stars"></a>
  <a href="https://github.com/Jeffeek/CyberMath/issues"><img src="https://img.shields.io/github/issues/Jeffeek/CyberMath?style=for-the-badge&logo=github" alt="GitHub Issues"></a>
</p>

---

## Features

CyberMath provides a rich set of tools to accelerate development, including:

### Data Structures

- **Matrices**: A comprehensive set of matrix types.
  - `Matrix<T>`: A standard, fixed-size matrix.
  - `DynamicMatrix<T>`: A matrix that allows adding or removing rows and columns.
  - `JuggedMatrix<T>`: A matrix with rows that can have different lengths.
  - `DynamicJuggedMatrix<T>`: A jagged matrix with dynamic row and column manipulation.
- **Binary Trees**: A collection of binary tree implementations.
  - `BinaryTree<T>`: A simple binary search tree.
  - `AVLBinaryTree<T>`: A self-balancing AVL tree.
  - `RedBlackBinaryTree<T>`: A self-balancing Red-Black tree.

### Equations

- `QuadraticEquation`: A class to solve quadratic equations of the form `ax^2 + bx + c = 0`.

### Extension Methods

- **Collections**: A variety of extension methods for collections, including `Swap`, `Shuffle`, `RandomItem`, and `Permutations`.
- **Strings**: A set of useful string extensions, such as `IsPalindrome`, `IsAnagramOf`, `WordsFrequency`, and `ToAlternatingCase`.
- **Random**: Extensions for the `Random` class to generate numbers within a specified range (`NextDouble`, `NextLong`).
- **Int32 and Int64**: A wide range of extensions for 32-bit and 64-bit integers, including:
  - **Number Theory**: `IsPrime`, `IsOdd`, `IsEven`, `GCD`, `LCM`.
  - **Number Manipulation**: `IsPalindrome`, `GetLength`, `GetDigits`.
  - **Conversions**: `ToBinary`, `ToHex`.

### Helpers

- `FixExpressionConverter`: A utility class to convert expressions between infix, postfix, and prefix notations.
- `GenericTypesExtensions`: Provides a `SerializableDeepCopy` method for creating a deep copy of any serializable object.

---

## Installation

You can install CyberMath via NuGet Package Manager:

```shell
PM> Install-Package CyberMath
```

---

## Usage Examples

### Matrix Operations

```csharp
using CyberMath.Structures.Matrices.Matrix;

var matrix = new Matrix<int>(3, 3);
matrix.FillRandomly(0, 10); // Fill with random numbers between 0 and 10

Console.WriteLine("Original Matrix:");
Console.WriteLine(matrix);

var transposed = matrix.Transpose();
Console.WriteLine("Transposed Matrix:");
Console.WriteLine(transposed);
```

### Binary Tree

```csharp
using CyberMath.Structures.BinaryTrees.BinaryTree;

var tree = new BinaryTree<int> { 5, 3, 8, 1, 4, 7, 9 };

Console.WriteLine($"Max value: {tree.Max()}");
Console.WriteLine($"Min value: {tree.Min()}");

Console.WriteLine("In-order traversal:");
foreach (var item in tree)
{
    Console.Write(item + " ");
}
// Output: 1 3 4 5 7 8 9
```

### Integer Extensions

```csharp
using CyberMath.Extensions;

int number = 12321;
Console.WriteLine($"{number} is a palindrome: {number.IsPalindrome()}"); // True

int a = 48, b = 18;
Console.WriteLine($"GCD of {a} and {b} is: {a.GCD(b)}"); // 6
Console.WriteLine($"LCM of {a} and {b} is: {a.LCM(b)}"); // 144
```

---

## Building from Source

To build the project from source, you will need the .NET SDK. 

1.  **Clone the repository:**
    ```shell
    git clone https://github.com/Jeffeek/CyberMath.git
    ```
2.  **Navigate to the project directory:**
    ```shell
    cd CyberMath
    ```
3.  **Build the solution:**
    ```shell
    dotnet build -c Release
    ```

## Running Tests

To run the tests, navigate to the root of the project and run the following command:

```shell
dotnet test
```