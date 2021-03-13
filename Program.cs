using System;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Node test = new Node(10);
            Console.WriteLine(test.Add(5));
            Console.WriteLine(test.Add(3));
            Console.WriteLine(test.Add(12));
            Console.WriteLine(test.Add(11));

            test.Print();

            Console.WriteLine(test.FindNode(5));
            Console.WriteLine(test.FindNode(10));

            Console.WriteLine("Count: " + test.Count());

            test.FindNode(12).Add(15);

            test.FindNode(15).num = 35;

            test.Print();

            Console.WriteLine("Count: " + test.Count());

            Console.WriteLine(test.Remove(12));

            test.Print();

            Console.WriteLine("Count: " + test.Count());

        }
    }

    public enum Link
    {
        RootLink,     // 0
        RNodeLink,    // 1
        LNodeLink     // 2
    }

    class Node
    {
        public int num { get; set; }
        public Node RNode, LNode;
        public Link Link;

        public Node(int num, Link Link = 0)
        {
            this.num = num;
            this.Link = Link;
        }

        public bool Add(int num)
        {
            if (num >= this.num && RNode == null)
            {
                RNode = new Node(num, Link.RNodeLink);
                return true;
            }
            else if (RNode != null)
            {
                return false;
            }
            else if (LNode == null)
            {
                LNode = new Node(num, Link.LNodeLink);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Print(int level = 0)
        {
            string text = "--";

            for (int i = 0; i < level; i++)
            {
                text += "--";
            }

            text += " " + num;

            Console.WriteLine(text);

            level++;

            if (LNode != null)
            {
                LNode.Print(level);
            }
            if (RNode != null)
            {
                RNode.Print(level);
            }
        }

        public Node FindNode(int num)
        {
            if (num == this.num)
            {
                return this;
            }

            if (LNode != null)
            {
                if (LNode.FindNode(num) != null)
                {
                    return LNode.FindNode(num);
                }
            }
            if (RNode != null)
            {
                if (RNode.FindNode(num) != null)
                {
                    return RNode.FindNode(num);
                }
            }

            return null;
        }

        public Node FindParentNode(int num)
        {

            if (LNode != null)
            {
                if (num == this.LNode.num)
                {
                    return this;
                }

                if (LNode.FindNode(num) != null)
                {
                    return LNode.FindNode(num);
                }
            }
            if (RNode != null)
            {
                if (num == this.RNode.num)
                {
                    return this;
                }

                if (RNode.FindNode(num) != null)
                {
                    return RNode.FindNode(num);
                }
            }

            return null;
        }

        public bool Remove(int num)
        {
            Node node = FindNode(num);

            if (node != null)
            {
                if (node.RNode == null)
                {
                    if (node.Link == Link.LNodeLink)
                    {
                        FindParentNode(num).LNode = node.LNode;
                    }
                    else
                    {
                        FindParentNode(num).RNode = node.LNode;
                    }
                    return true;
                }
                else if (node.LNode == null)
                {
                    if (node.Link == Link.LNodeLink)
                    {
                        FindParentNode(num).LNode = node.RNode;
                    }
                    else
                    {
                        FindParentNode(num).RNode = node.RNode;
                    }
                    return true;
                }
                else if (node.RNode.LNode != null)
                {
                    Node current = node.RNode.LNode;

                    while (current.LNode != null)
                    {
                        if (node.Link == Link.LNodeLink)
                        {
                            FindParentNode(num).LNode = current.LNode;
                        }
                        else
                        {
                            FindParentNode(num).RNode = current.LNode;
                        }
                        current = current.LNode.LNode;
                    }

                    return true;
                }
                else
                {
                    node = null;
                    Console.WriteLine("node == null");
                    return true;
                } 
            }

            return false;
        }

        public int Count()
        {
            int count = 1;
            if (LNode != null)
            {
                count += LNode.Count();
            }
            if (RNode != null)
            {
                count += RNode.Count();
            }
            return count;
        }

    }
}
