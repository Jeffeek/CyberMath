using System;
using System.Collections.Generic;
using System.Linq;
using CyberMath.Structures.BinaryTreeBase;

namespace CyberMath.Structures.BinaryTree
{
    public class BinaryTree<T> : BinaryTreeBase<T>
        where T : IComparable<T>, IComparable
    {
        public BinaryTree(params T[] values)
        {
            AddRange(values);
        }

        public BinaryTree(IEnumerable<T> values)
        {
            AddRange(values.ToArray());
        }

        public BinaryTree() { }

        public override IBinaryTreeNode<T> Root { get; protected set; }
        
        public override void Add(T item)
        {
            if (Root == null)
            {
                Root = new BinaryTreeNode<T>(item);
                Count = 1;
                return;
            }

            Root.Add(item);
            Count++;
        }

        public override bool Remove(T item)
        {
            var items = Postorder().ToList();
            if (!items.Remove(item)) return false;
            Clear();
            AddRange(items.ToArray());
            return true;
        }
    }
}