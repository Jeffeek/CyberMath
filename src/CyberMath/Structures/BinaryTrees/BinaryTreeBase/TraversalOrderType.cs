namespace CyberMath.Structures.BinaryTrees.BinaryTreeBase
{
    /// <summary>
    ///     Enum to represent traversal strategy in <see cref="IBinaryTree{T}" />
    /// </summary>
    public enum TraversalOrderType
	{
        /// <summary>
        ///     Preorder way in <see cref="IBinaryTree{T}" />
        /// </summary>
        Preorder,

        /// <summary>
        ///     Inorder way in <see cref="IBinaryTree{T}" />
        /// </summary>
        Inorder,

        /// <summary>
        ///     Postorder way in <see cref="IBinaryTree{T}" />
        /// </summary>
        Postorder
	}
}