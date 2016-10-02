using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coding.Tests
{
    /// <summary>
    /// Node structure.
    /// </summary>
    /// <typeparam name="TType">type of node.</typeparam>
    internal sealed class LinkedListNode<TType> where TType : class
    {
        public TType Content { get; set; }
        public LinkedListNode<TType> Next { get; set; }
        public LinkedListNode<TType> Previous { get; set; }
    }

    /// <summary>
    /// Linked list.
    /// </summary>
    /// <typeparam name="TType">type of list.</typeparam>
    internal sealed class LinkedList<TType> where TType : class
    {
        private uint _size = 0;
        private LinkedListNode<TType> _headNode = null;
        private LinkedListNode<TType> _lastNode = null;

        public LinkedList()
        {
            _size = 0;
        }

        /// <summary>
        /// Size list.
        /// </summary>
        /// <returns></returns>
        public uint Count()
        {
            return _size;
        }

        /// <summary>
        /// Clear the linked list.
        /// </summary>
        public void Clear()
        {
            GC.Collect();
            _size = 0;
            _lastNode = _headNode = null;
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Adds the new node at the start position.
        /// </summary>
        /// <param name="value">value to be added.</param>
        /// <returns>node added.</returns>
        public LinkedListNode<TType> AddFirst(TType value)
        {
            var node = new LinkedListNode<TType>()
            {
                Content = value,
                Next = _headNode ?? _lastNode
            };

            if (_headNode != null)
            {
                _headNode.Previous = node;
            }
            else
            {
                _lastNode = node;
            }

            _headNode = node;

            _size++;
            return node;
        }

        /// <summary>
        /// Adds the new node at the last position.
        /// </summary>
        /// <param name="value">value to be added.</param>
        /// <returns>node added.</returns>
        public LinkedListNode<TType> AddLast(TType value)
        {
            var node = new LinkedListNode<TType>()
            {
                Content = value,
                Previous = _lastNode ?? _headNode
            };

            if (_lastNode != null)
            {
                _lastNode.Next = node;
            }

            if (_headNode == null)
            {
                _headNode = node;
            }

            _lastNode = node;

            _size++;

            return node;
        }

        /// <summary>
        /// Adds the new node before the existent node.
        /// </summary>
        /// <param name="node">existent node.</param>
        /// <param name="value">value to be added.</param>
        /// <returns>node added.</returns>
        public LinkedListNode<TType> AddBefore(LinkedListNode<TType> node, TType value)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var internalNode = new LinkedListNode<TType>()
            {
                Content = value,
                Next = node,
                Previous = node.Previous
            };

            node.Previous = internalNode;

            if (internalNode.Previous != null)
            {
                internalNode.Previous.Next = internalNode;
            }

            _size++;

            return internalNode;
        }

        /// <summary>
        /// Adds the new node after the existent node.
        /// </summary>
        /// <param name="node">existent node.</param>
        /// <param name="value">value to be added.</param>
        /// <returns>node added.</returns>
        public LinkedListNode<TType> AddAfter(LinkedListNode<TType> node, TType value)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var internalNode = new LinkedListNode<TType>()
            {
                Content = value,
                Previous = node,
                Next = node.Next
            };

            node.Next = internalNode;

            if (internalNode.Next != null)
            {
                internalNode.Next.Previous = internalNode;
            }

            _size++;

            return internalNode;
        }

        /// <summary>
        /// Finds the first node thats contains the value.
        /// </summary>
        /// <param name="value">value to be searched.</param>
        /// <returns>node found.</returns>
        public LinkedListNode<TType> Find(TType value)
        {
            LinkedListNode<TType> node = _headNode;

            if (_headNode != null)
            {
                while (value != node.Content && (node = node.Next) != null) ;
            }

            return node;
        }

        /// <summary>
        /// Removes the first node.
        /// </summary>
        public void RemoveFirst()
        {
            if (_headNode != null)
            {
                if (_headNode.Next != null)
                {
                    _headNode.Next.Previous = null;
                }
                _headNode = _headNode.Next;

                _size--;
            }
        }

        /// <summary>
        /// Removes the last node.
        /// </summary>
        public void RemoveLast()
        {
            if (_lastNode != null)
            {
                if (_lastNode.Previous != null)
                {
                    _lastNode.Previous.Next = null;
                }
                _lastNode = _lastNode.Previous;

                _size--;
            }
        }

        /// <summary>
        /// Removes the first node that contains the value.
        /// </summary>
        /// <param name="value">value to be removed.</param>
        public void Remove(TType value)
        {
            var node = Find(value);

            if (node != null)
            {
                if (node.Equals(_headNode))
                {
                    RemoveFirst();
                }
                else if (node.Equals(_lastNode))
                {
                    RemoveLast();
                }
                else
                {
                    node.Previous.Next = node.Next;
                    node.Next.Previous = node.Previous;
                    _size--;
                }
            }
        }

        /// <summary>
        /// Converts the content in array.
        /// </summary>
        /// <returns>array of content.</returns>
        public TType[] ToArray()
        {
            var array = new TType[Count()];
            var currentNode = _headNode;

            for (int i = 0; i < Count(); i++)
            {
                array[i] = currentNode.Content;
                currentNode = currentNode.Next;
            }

            return array;
        }
    }

    /// <summary>
    /// Tests to structures.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/1/2016 12:49:35 PM</Date>
    [TestClass]
    public class ListAlgorithmTests
    {
        /// <summary>
        /// List algorithm - linked list.
        /// </summary>
        [TestMethod]
        public void LinkedListTest()
        {
            var linkedList = new LinkedList<string>();
            var nodeA = linkedList.AddFirst("A");
            var nodeZ = linkedList.AddLast("Z");

            var nodeX = linkedList.AddBefore(nodeZ, "X");

            var nodeJ = linkedList.AddAfter(nodeA, "J");
            var nodeO = linkedList.AddAfter(nodeJ, "O");
            var nodeS = linkedList.AddAfter(nodeO, "S");
            var nodeE = linkedList.AddAfter(nodeS, "E");

            var nodeM = linkedList.AddAfter(nodeE, "M");
            var nodeA1 = linkedList.AddAfter(nodeM, "A");
            var nodeU = linkedList.AddAfter(nodeA1, "U");
            var nodeR = linkedList.AddAfter(nodeU, "R");
            var nodeO1 = linkedList.AddAfter(nodeR, "O");

            linkedList.Remove("X");
            linkedList.RemoveFirst();
            linkedList.RemoveLast();

            System.Diagnostics.Debug.WriteLine("LinkedList: " + string.Join(", ", linkedList.ToArray()));
        }
    }
}
