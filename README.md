<h1>CyberMath</h1>
<img src="https://img.shields.io/github/workflow/status/Jeffeek/CyberMath/.NET?style=for-the-badge">
<h2>A little library with useful data structures and extension methods.</h2>

<h2 align="center">Some stats</h2>

<p align="center">
<img src="https://img.shields.io/nuget/v/CyberMath?style=for-the-badge">
<img src="https://img.shields.io/github/last-commit/Jeffeek/CyberMath?style=for-the-badge">
<img src="https://img.shields.io/nuget/dt/CyberMath?style=for-the-badge">
<img src="https://img.shields.io/github/stars/Jeffeek/CyberMath?style=for-the-badge">
<img src="https://img.shields.io/github/issues/Jeffeek/CyberMath?style=for-the-badge">
<img src="https://img.shields.io/github/repo-size/Jeffeek/CyberMath?style=for-the-badge">
<img src="https://img.shields.io/tokei/lines/github/Jeffeek/CyberMath?style=for-the-badge">
</p>
<hr>

## Nuget Package
>PM> Install-Package CyberMath -Version 1.0.3
<hr>

- **Two-dimensional arrays**
  - **Matrix**
    - IsSquare -> bool *(Represent a bool(true) value if the rows count equals elements count on each row; otherwise false)*
    - [int row, int column] -> T *(Indexer for matrix)*
    - ProcessFunctionOverData -> void *(Do action over every element in matrix)*
    - ElementsInRow -> int *(Returns count of elements in row)*
    - GetColumnsEnumerable -> IEnumerable&lt;IEnumerable&lt;T&gt;&gt; *(Returns enumerable, which is 'walks' on the column neither default)*
    - RowsCount -> int *(Count of rows in matrix)*
    - ColumnsCount -> int *(Count of columns in matrix)*
    - Transpose -> IMatrix&lt;T&gt; Creates a new transposed matrix
    <hr>
    
    - **DynamicMatrix**
      - AddColumn -> void *(Inserting a new column to the end of matrix)*
      - InsertColumn -> void *(Inserting the column into matrix by index)*
      - AddRow -> void *(Inserting a new row to the end of matrix)*
      - InsertRow -> void *(Inserting the row into matrix by index)*
      - RemoveColumn -> void *(Removes a column in matrix at index)*
      - RemoveRow -> void *(Removes a row in matrix at index)*
  <hr>
  
  - **Jugged Matrix**
    - IsSquare -> bool *(Represent a bool(true) value if the rows count equals elements count on each row; otherwise false)*
    - [int row, int column] -> T *(Indexer for matrix)*
    - ProcessFunctionOverData -> void *(Do action over every element in matrix)*
    - ElementsInRow -> int *(Returns count of elements in row)*
    - GetColumnsEnumerable -> IEnumerable&lt;IEnumerable&lt;T&gt;&gt; *(Returns enumerable, which is 'walks' on the column neither default)*
    - RowsCount -> int *(Count of rows in matrix)*
    - SortRows -> IJuggedMatrix&lt;T&gt; *(Sorts rows in matrix by count of elements)*
    - SortRowsByDescending -> IJuggedMatrix&lt;T&gt; *(Sorts rows by descending in matrix by count of elements)*
    <hr>
    
    - Dynamic Jugged Matrix
      - AddColumn -> void *(Inserting a new column to the end of matrix if it's possible)*
      - InsertColumn -> void *(Inserting the column into matrix by index)*
      - AddRow -> void *(Inserting a new row to the end of matrix)*
      - InsertRow -> void *(Inserting the row into matrix by index)*
      - RemoveColumn -> void *(Removes a column in matrix at index)*
      - RemoveRow -> void *(Removes a row in matrix at index)*
<hr>

- **Binary Trees**
   - Root -> IBinaryTreeNode&lt;T&gt; *(Reference to the main node, called Root)*
   - IsEmpty -> bool *(bool result which show the emptiness of IBinaryTree)*
   - TraversalOrderType -> TraversalOrderType *(Traversal strategy for foreach statement)*
   - Depth -> int *(Depth of BinaryTree)*
   - Inorder -> IEnumerable&lt;T&gt; *(Returns an inorder traversal IEnumerable collection)*
   - Preorder -> IEnumerable&lt;T&gt; *(Returns an preorder traversal IEnumerable collection)*
   - Postorder -> IEnumerable&lt;T&gt; *(Returns an postorder traversal IEnumerable collection)*
   - Min -> T *(Returns minimal element in BinaryTree)
   - Max -> T *(Returns maximal element in BinaryTree)
   - AddRange -> void *(Adds IEnumerable collection into BinaryTree)
   - MergeWith -> void *(Merges initial IBinaryTree with another IBinaryTree)*
   <hr>
   
  - **Vanilla Tree**
  - **AVL Tree**
  - **Red Black Tree** 
  
  - **Common interface: IBinaryTree : *ICollection*, IDisposable where T : IComparable&lt;T&gt;, IComparable**
  
<hr>

- **Helpers**
  - FixExpressionConverter **(Class for converting xFix expression into yFix)**
    - Infix -> Postfix => A+B\*C/(E-F) -> ABC\*EF-/+<br/>
    - Infix -> Prefix => A+B\*C/(E-F) -> +A\*B/C-EF<br/>
    - Postfix -> Infix => ABC\*EF-/+ -> (A+((B\*C)/(E-F)))<br/>
    - Postfix -> Prefix => ABC/-AK/L-\* -> \*-A/BC-/AKL<br/>
    - Prefix -> Infix => +A\*B/C-EF -> (A+(B\*(C/(E-F))))<br/>
    - Prefix -> Postfix => \*-A/BC-/AKL -> ABC/-AK/L-\*
  - GenericTypesExtensions **(Just a class to make out life and programming faster and more productive)**
    - SerializableDeepCopy -> T *(Makes a deep copy of item. Type of item should be marked as [Serializable]; otherwise -> EXCEPTION)*
    
<hr>

- **Extension methods**
  - Extension methods for **collections**
    - Swap -> void *(Swaps items in indexed collections)*
    - Shuffle -> void *(Shuffles the items in an indexed collection)*
    - RandomItem -> T *(Gets a random item from collection)*
    - Permutations -> IEnumerable&lt;IEnumerable&lt;T&gt;&gt; *(Returns a new collections with all permutations of elements with repeating elements)*
    - PermutationsWithRepeat -> IEnumerable&lt;IEnumerable&lt;T&gt;&gt *(Returns a new collections with all permutations of elements with repeating elements)*
  <hr>

  - Extension methods for **strings**
    - Concat -> string
    - IsPalindrome -> bool *(Checks string for palindromicity)*
    - IsAnagramOf -> bool *(Checks two string for anagramism)*
    - WordsFrequency -> Dictionary&lt;char,int&gt; *(Creates a Dictionary&lt;TKey,TValue&gt; where **TKey** is char and **TValue** is int **(count of TKey in input string)**)*
    - ToInt32 -> int *(Returns string parsed to Int32)*
    - ToInt64 -> long *(Returns string parsed to Int64)*
    - ToAlternatingCase -> *(Converts input string to alternating case)*
  <hr>

  - Extension methods for **Random**
    - NextDouble -> double *(Generates a double number between min and max)*
    - NextLong -> long *(Generates a long number between min and max)*
  <hr>

  - Extension methods for **Int32** and **Int64**
    - IsPalindrome -> bool *(Checks number for palindromicity)*
    - IsOdd -> bool *(Checks is number odd)*
    - IsEven -> bool *(Checks is number even)*
    - GCD **(greatest common divisor)** -> int/long *(Calculates greatest common divisor between two numbers)*
    - LCM **(lowest common multiple)** -> int/long *(Calculates lowest common multiple between two numbers)*
    - Swap -> void *(Swaps two integers in memory **(by ref)**)*
    - GetLength -> int *(Calculates the length of number)*
    - ToBinary -> string *(Converts number to binary(2) format)*
    - ToHex -> string *(Converts number to HEX(16) format)*
    - GetDigits -> IEnumerable&lt;bytes&gt; *Return all digits in number as a collection*
    <hr>

    - Extension methods for **Prime Numbers**
      - IsPrime -> bool *(Checks number for primality)*
      - GenerateRandomPrimeNumber -> Int32/Int64 *(Generating one random prime number)*
      - GeneratePrimeNumbers -> IEnumerable&lt;Int32/Int64&gt; *(Generates IEnumerable collection of prime numbers which are less than max)*
  <hr>

  - Extension methods for all matrices
    - IsMaxInColumn -> bool *(Returns bool value if element at [i, j] is max in IMatrixBase&lt;IComparable&gt; matrix column at index j)*
    - IsMinInRow -> bool *(Returns bool value if element at [i, j] is min in IMatrixBase&lt;IComparable&gt; matrix row at index i)*
    - DiagonalSum -> int/long/souble/decimal/short and Nullable *(**ONLY FOR SQUARE** Calculates sum of all items on main diagonal)*
    - SideDiagonalSum -> int/long/souble/decimal/short and Nullable *(**ONLY FOR SQUARE** Calculates sum of all items on side diagonal)*
    - SumSaddlePoints -> int/long/souble/decimal/short and Nullable *(Calculates sum of all saddle points in matrix)*
    - CreateMatrixWithoutRow -> IJuggedMatrix/IMatrix *(Creates a new matrix without row at rowIndex)*
    - CreateMatrixWithoutColumn -> IJuggedMatrix/IMatrix *(Creates a new matrix without column at columnIndex)*
    <hr>

    - For primitives
      - Add -> IMatrixBase&lt;T&gt; *(Returns the add IMatrixBase first and IMatrixBase second)*
      - Sub -> IMatrixBase&lt;T&gt; *(Returns the sub IMatrixBase first and IMatrixBase second)*
      - MulOnNumber -> IMatrixBase&lt;T&gt; *(Returns the multiplication IMatrixBase matrix on number)*
      - FillRandomly -> void *(Fills matrix with randomly generated items)*
  <hr>

  - Extension methods for **JuggedMatrix&lt;T&gt;** 
    - CountOnEachRow -> IEnumerable&lt;int&gt; *(Returns an IEnumerable with the count of elements on each row of matrix)*
    - ToMatrix -> IMatrix&lt;T&gt; *(Creates a new instance of IMatrix from IJuggedMatrix)*
    - CreateVanilla -> T[][] *(Creates a vanilla array of arrays on base of JuggedMatrix)*
  <hr>

  - Extension methods for **IMatrix&lt;T&gt;**
    - ToJuggedMatrix -> IJuggedMatrix&lt;T&gt; *(Creates a new instance of IJuggedMatrix from IMatrix)*
    - CreateVanilla -> T[,] *(Creates a vanilla matrix on base of Matrix)*
    <hr>

    - For primitives
      - Multiplication -> IMatrix&lt;T&gt; *(Returns the mul IMatrix first and IMatrix second)*
      - CalculateDeterminant -> int/long/double/decimal and Nullable *(Calculates determinant for IMatrix)*
      - CreateInvertibleMatrix -> IMatrix&lt;double&gt; *(Creates inverted matrix from IMatrix)*
      - CalculateMinor -> int/long/souble/decimal and Nullable *(Calculates minor for IMatrix)*
<hr>

- **Equations**
  - Quadratic *(Represents a class for building quadratic equation)*
