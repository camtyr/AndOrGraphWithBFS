using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tim_kiem_rong
{
    //
    public enum Label
    {
        gd, kgd, kxd
    }

    public class Node
    {
        public string Name {  get; set; }
        public List<Node> Childrens {  get; set; }
        public bool IsAndNode {  get; set; }
        public bool IsEndNode { get; set; }
        public Label LabelNode { get; set; } = Label.kxd;

        // hàm khởi tạo
        public Node(string name, bool isAndNode, bool isEndNode)
        {
            Name = name;
            IsAndNode = isAndNode;
            Childrens = new List<Node>();
            IsEndNode = isEndNode;
        }

        // Thêm con cho node
        public void AddChild(Node child)
        {
            Childrens.Add(child);
        }
    }
}
