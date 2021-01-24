<h1>CyberMath</h1>
<h2>A little library with useful data structures and extension methods.</h2>
<hr>
<h2 align="center">Some stats</h2>

<p align="center">
<img src="https://img.shields.io/nuget/v/CyberMath?style=for-the-badge">
<img src="https://img.shields.io/github/last-commit/Jeffeek/CyberMath?style=for-the-badge">
<img src="https://img.shields.io/nuget/dt/CyberMath?style=for-the-badge">
<img src="https://img.shields.io/github/stars/Jeffeek/CyberMath?style=for-the-badge">
<img src="https://img.shields.io/github/issues/Jeffeek/CyberMath?style=for-the-badge">
<img src="https://img.shields.io/github/issues-pr/Jeffeek/CyberMath?style=for-the-badge">
<img src="https://img.shields.io/github/repo-size/Jeffeek/CyberMath?style=for-the-badge">
</p>
<hr>

- **Two-dimensional arrays**
  - Matrix
  - Jugged Matrix
<hr>

- **Binary Trees**
  - Vanilla Tree
  - AVL Tree
  - Red Black Tree 
  
  - **Common interface: IBinaryTree : *ICollection*, IDisposable where T : IComparable&lt;T&gt;, IComparable**
  
<hr>

- **Extension methods**
  - Extension methods for **collections**
    - Swap -> void *(Swaps items in indexed collections)*
    - Shuffle -> void *(Shuffles the items in an indexed collection)*
    - RandomItem -> &lt;T&gt; *(Gets a random item from collection)*
    - Permutations -> IEnumerable&lt;IEnumerable&lt;T&gt;&gt; *(Returns a new collections with all permutations of elements with repeating elements)*
    - PermutationsWithRepeat -> IEnumerable&lt;IEnumerable&lt;T&gt;&gt *(Returns a new collections with all permutations of elements with repeating elements)*
  <hr>

  - Extension methods of **strings**
    - Concat -> string
    - IsPalindrome -> bool *(Checks string for palindromicity)*
    - IsAnagramOf -> bool *(Checks two string for anagramism)*
    - WordsFrequency -> Dictionary&lt;char,int&gt; *(Creates a Dictionary&lt;TKey,TValue&gt; where **TKey** is char and **TValue** is int **(count of TKey in input string)**)*
    - ToInt32 -> int *(Returns string parsed to Int32)*
    - ToInt64 - long *(Returns string parsed to Int64)*
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
  <hr>

    - Extension methods for **Prime Numbers**
      - IsPrime -> bool *(Checks number for primality)*
      - GenerateRandomPrimeNumber -> Int32/Int64 *(Generating one random prime number)*
      - GeneratePrimeNumbers -> IEnumerable&lt;Int32/Int64&gt; *(Generates IEnumerable collection of prime numbers which are less than max)*
    <hr>

  - Extension methods for all **IMatrixBase&lt;T&gt;** 
    - IsMaxInColumn -> bool *(Returns bool value if element at [i, j] is max in IMatrixBase&lt;IComparable&gt; matrix column at index j)*
    - IsMinInRow -> bool *(Returns bool value if element at [i, j] is min in IMatrixBase&lt;IComparable&gt; matrix row at index i)*
    - DiagonalSum -> int/long/souble/decimal/short and Nullable *(**ONLY FOR SQUARE** Calculates sum of all items on main diagonal)*
    - SideDiagonalSum -> int/long/souble/decimal/short and Nullable *(**ONLY FOR SQUARE** Calculates sum of all items on side diagonal)*
    - SumSaddlePoints -> int/long/souble/decimal/short and Nullable *(Calculates sum of all saddle points in matrix)*
    <hr>

    - For primitives
      - Add -> IMatrixBase&lt;T&gt; *(Returns the add IMatrixBase first and IMatrixBase second)*
      - Sub -> IMatrixBase&lt;T&gt; *(Returns the sub IMatrixBase first and IMatrixBase second)*
      - MulOnNumber -> IMatrixBase&lt;T&gt; *(Returns the multiplication IMatrixBase matrix on number)*
      - FillRandomly -> void *(Fills matrix with randomly generated items)*
  <hr>

  - Extension methods for **JuggedMatrix&lt;T&gt;** 
    - CountOnEachRow -> IEnumerable&lt;int&gt; *()*
    - ToMatrix -> -> IMatrix&lt;T&gt; *(Creates a new instance of IMatrix from IJuggedMatrix)*
    - [NextV] CreateVanilla -> T[][] *()*
  <hr>

  - Extension methods for **IMatrix&lt;T&gt;**
    - ToJuggedMatrix -> IJuggedMatrix&lt;T&gt; *(Creates a new instance of IJuggedMatrix from IMatrix)*
    - [NextV] CreateVanilla -> T[,] *()*
    <hr>

    - For primitives
      - Multiplication -> IMatrix&lt;int&gt; *(Returns the mul IMatrix first and IMatrix second)*
      - CalculateDeterminant -> int/long/souble/decimal and Nullable *(Calculates determinant for IMatrix)*
      - CreateInvertibleMatrix -> IMatrix&lt;double&gt; *(Creates inverted matrix from IMatrix)*
      - CalculateMinor -> int/long/souble/decimal and Nullable *(Calculates minor for IMatrix)*
<hr>

- **Equations**
  - Quadratic *(Represents a class for building quadratic equation)*
