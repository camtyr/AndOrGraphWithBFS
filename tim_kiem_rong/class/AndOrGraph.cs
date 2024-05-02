using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace tim_kiem_rong
{
    public class AndOrGraph
    {
        // node gốc
        public Node Root { get; set; }

        // hàm khởi tạo
        public AndOrGraph(Node root)
        {
            this.Root = root;
        }

        // tạo đồ thị và hoặc
        public static AndOrGraph CreateGraphLab1()
        {
            Node A = new Node("A", false, false);

            Node B = new Node("B", true, false);
            Node C = new Node("C", false, false);
            Node D = new Node("D", true, false);

            Node E = new Node("E", false, true);
            Node F = new Node("F", false, false);
            Node G = new Node("G", false, false);
            Node H = new Node("H", false, true);
            Node I = new Node("I", false, false);

            Node J = new Node("J", true, false);
            Node K = new Node("K", false, false);
            Node L = new Node("L", false, true);

            Node M = new Node("M", false, true);
            Node N = new Node("N", false, false);

            Node O = new Node("O", false, true);

            N.AddChild(O);

            J.AddChild(M);
            J.AddChild(N);

            F.AddChild(J);

            G.AddChild(K);

            I.AddChild(L);

            B.AddChild(E);
            B.AddChild(F);

            C.AddChild(G);

            D.AddChild(H);
            D.AddChild(I);

            A.AddChild(B);
            A.AddChild(C);
            A.AddChild(D);

            Console.WriteLine("Lab1\n***********************\n");

            return new AndOrGraph(A);
        }

        public static AndOrGraph CreateGraphLab2()
        {
            Node A = new Node("A", false, false);

            Node B = new Node("B", false, false);
            Node C = new Node("C", false, false);

            Node D = new Node("D", false, false);
            Node E = new Node("E", false, false);
            Node F = new Node("F", true, false);
            Node G = new Node("G", false, false);

            Node H = new Node("H", false, false);
            Node I = new Node("I", true, false);
            Node J = new Node("J", false, false);
            Node K = new Node("K", false, false);
            Node L = new Node("L", false, true);
            Node M = new Node("M", false, false);
            Node N = new Node("N", false, false);
            Node O = new Node("O", false, false);

            Node P = new Node("P", false, false);
            Node Q = new Node("Q", false, true);
            Node R = new Node("R", false, true);
            Node S = new Node("S", false, true);
            Node T = new Node("T", false, false);

            H.AddChild(P);

            I.AddChild(Q);
            I.AddChild(R);

            M.AddChild(S);
            M.AddChild(T);

            D.AddChild(H);
            D.AddChild(I);

            E.AddChild(J);
            E.AddChild(K);

            F.AddChild(L);
            F.AddChild(M);

            G.AddChild(N);
            G.AddChild(O);

            B.AddChild(D);
            B.AddChild(E);

            C.AddChild(F);
            C.AddChild(G);

            A.AddChild(B);
            A.AddChild(C);

            Console.WriteLine("Lab2\n***********************\n");

            return new AndOrGraph(A);
        }

        public static AndOrGraph CreateGraphLab3()
        {
            Node A = new Node("A", true, false);

            Node B = new Node("B", false, false);
            Node C = new Node("C", false, false);

            Node D = new Node("D", false, false);
            Node E = new Node("E", false, false);
            Node F = new Node("F", true, false);
            Node G = new Node("G", false, false);

            Node H = new Node("H", false, false);
            Node I = new Node("I", true, false);
            Node J = new Node("J", false, true);
            Node K = new Node("K", false, false);

            Node L = new Node("L", false, true);
            Node M = new Node("M", false, true);
            Node N = new Node("N", false, true);
            Node O = new Node("O", false, false);

            I.AddChild(L);
            I.AddChild(M);

            K.AddChild(N);
            K.AddChild(O);

            D.AddChild(H);
            D.AddChild(I);

            F.AddChild(J);
            F.AddChild(K);

            B.AddChild(D);
            B.AddChild(E);

            C.AddChild(F);
            C.AddChild(G);

            A.AddChild(B);
            A.AddChild(C);

            Console.WriteLine("Lab3\n***********************\n");

            return new AndOrGraph(A);
        }

        // thủ tục gán nhãn
        public static void LabelingProcedure(Node node, Queue<Node> openSet, Queue<Node> closedSet)
        {
            // nếu node có nhãn không xác định thì thực hiện câu lệnh
            if (node.LabelNode == Label.kxd)
            {
                // nếu không nằm trong tập MO và tập DONG thì gán nhãn không xác định
                if ((openSet.Contains(node) || closedSet.Contains(node)))
                {
                    // nếu là node kết thúc thì gán nhãn giải được
                    if (node.IsEndNode) node.LabelNode = Label.gd;

                    // nếu không là node kết thúc và là node lá thì gán nhãn không giải được
                    else if (node.Childrens.Count == 0) node.LabelNode = Label.kgd;

                    else
                    {
                        bool canSolve = true;
                        bool cantSolve = false;

                        // nếu là node và
                        if (node.IsAndNode)
                        {
                            // duyệt các con của node
                            foreach (var nodeChild in node.Childrens)
                            {
                                LabelingProcedure(nodeChild, openSet, closedSet);

                                /* nếu 1 trong các con của node không phải nhãn giải được
                                   thì đặt biến canSolve = false */
                                if (nodeChild.LabelNode != Label.gd)
                                {
                                    canSolve = false;

                                    /* nếu 1 trong các con của node là nhãn không giải được
                                       thì đặt biến canSolve = true */
                                    if (nodeChild.LabelNode == Label.kgd) cantSolve = true;
                                }
                            }
                        }

                        //nếu là node hoặc
                        else
                        {
                            canSolve = false;
                            cantSolve = true;

                            foreach (var nodeChild in node.Childrens)
                            {
                                LabelingProcedure(nodeChild, openSet, closedSet);

                                /* nếu 1 trong các con của node không phải nhãn không giải được
                                   thì đặt biến cantSolve = false */
                                if (nodeChild.LabelNode != Label.kgd)
                                {
                                    cantSolve = false;

                                    /* nếu 1 trong các con của node là nhãn giải được
                                       thì đặt biến canSolve = true */
                                    if (nodeChild.LabelNode == Label.gd) canSolve = true;
                                }
                            }
                        }

                        if (canSolve) node.LabelNode = Label.gd;
                        else if (cantSolve) node.LabelNode = Label.kgd;
                        else node.LabelNode = Label.kxd;

                        /* nếu node được gán nhãn giải được hoặc nhãn không giải được
                           thì loại bỏ con của node trong tập MO */
                        if (node.LabelNode != Label.kxd)
                        {
                            foreach (Node nodeChild in openSet)
                            {
                                openSet = new Queue<Node>(openSet.Where(node => node != nodeChild));
                            }
                        }    
                    }
                }
            }

            Console.Write(node.Name + " " + node.LabelNode + " ");
        }

        public static void BFS(AndOrGraph graph)
        {
            List<string> log = new List<string>();
            Queue<Node> openSet = new Queue<Node>();
            Queue<Node> closedSet = new Queue<Node>();

            // thêm node gốc vào tập MO
            Node root = graph.Root;
            openSet.Enqueue(root);

            int step = 0;
            log.Add($"{step},,,{root.Name},");

            // khi có phần tử trong tập MO thì thực hiện tiếp
            while (openSet.Count > 0)
            {
                step++;
                // lấy từ tập MO 1 node nằm ở đầu
                Node currentNode = openSet.Dequeue();

                // thêm node hiện tại vào tập DONG
                closedSet.Enqueue(currentNode);
                Console.WriteLine($"Lay ra node {currentNode.Name}");

                // nếu node hiện tại có con thì thực hiện tiếp
                if (currentNode.Childrens.Count > 0)
                {
                    foreach (Node nodeChild in currentNode.Childrens)
                    {
                        openSet.Enqueue(nodeChild);

                        // nếu node con là node kết thúc thì gán nhãn giải được 
                        if (nodeChild.IsEndNode) nodeChild.LabelNode = Label.gd;
                    }

                    LabelingProcedure(root, openSet, closedSet);
                    Console.WriteLine();

                    // nếu node gốc giải được thì thông báo kết thúc chương trình
                    if (root.LabelNode == Label.gd)
                    {
                        log.Add($"{step},{currentNode.Name},{String.Join(" ", new List<Node>(currentNode.Childrens).ConvertAll(node => node.Name))},,");
                        Console.Write("Thanh cong");
                        Console.WriteLine("\n***********************\n");
                        break;
                    }

                    // nếu node gốc không giải được thì thông báo kết thúc chương trình
                    else if (root.LabelNode == Label.kgd)
                    {
                        log.Add($"{step},{currentNode.Name},{String.Join(" ", new List<Node>(currentNode.Childrens).ConvertAll(node => node.Name))},,");
                        Console.Write("Khong thanh cong");
                        Console.WriteLine("\n***********************\n");
                        break;
                    }

                    // nếu node gốc không xác định thì tiếp tục 
                    else
                    {
                        foreach (Node nodeChild in openSet)
                        {
                            // nếu trong tập MO có node với nhãn giải được hoặc không giải được thì loại node đó ra khỏi tập MO
                            if (nodeChild.LabelNode != Label.kxd) openSet = new Queue<Node>(openSet.Where(node => node != nodeChild));
                        }
                    }
                }

                log.Add($"{step},{currentNode.Name},{String.Join(" ", new List<Node>(currentNode.Childrens).ConvertAll(node => node.Name))},{String.Join(" ", new List<Node>(openSet).ConvertAll(node => node.Name))},{String.Join(" ", new List<Node>(closedSet).ConvertAll(node => node.Name))}");

                Console.Write("tap mo: ");
                foreach (Node nodeChild in openSet)
                {
                    Console.Write(nodeChild.Name + " ");
                }

                Console.Write("\ntap dong: ");
                foreach (Node nodeChild in closedSet)
                {
                    Console.Write(nodeChild.Name + " ");
                }
                Console.WriteLine("\n***********************\n");
            }

            Console.WriteLine("\n{0,-10} {1,-10} {2,-15} {3,-20} {4,-20}", "N", "S", "Child(S)", "MO", "DONG");
            foreach (var entry in log)
            {
                var parts = entry.Split(',');
                Console.WriteLine("{0,-10} {1,-10} {2,-15} {3,-20} {4,-20}", parts[0], parts[1], parts[2], parts[3], parts[4]);
            }

            Console.WriteLine();
        }

    }
}
